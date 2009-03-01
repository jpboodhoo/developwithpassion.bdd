properties{#misc
  $framework_dir = "$env:windir\microsoft.net\framework\v2.0.50727"
}  

properties{#directories
  $base_dir = new-object System.IO.DirectoryInfo $pwd
  $base_dir = $base_dir.Parent.FullName
  
  $build_dir = "$base_dir\build"
  $build_tools_dir = "$build_dir\tools"
  $build_artifacts_dir = "$build_dir\artifacts"

  $config_dir = "$build_dir\config"
	$coverage_dir = "$build_artifacts_dir\coverage"
	$coverage_file_name = "$coverage_dir\ncoverage.xml"

	$deploy_dir = "$build_dir\deploy"

	$product_dir = "$base_dir\product"

	$sql_dir = "$build_dir\sql"
	$sql_ddl_dir = "$sql_dir\ddl" 
	$sql_data_dir = "$sql_dir\data" 
  $third_party_dir = "$base_dir\thirdparty"
  $third_party_tools_dir = "$third_party_dir\tools"
  $third_party_lib_dir = "$third_party_dir\lib"
}

properties {#load in the build utilities file
  . $build_dir\tools\psake\build_utilities.ps1
}

properties {#load in the file that contains the name for the project
  . $build_dir\project_name.ps1
}

properties{#filesets
  $all_template_files = get_file_names(get-childitem -path $build_dir -recurse -filter "*.template")
  $third_party_libraries = get_file_names(get-childitem -path $third_party_lib_dir -recurse -filter *.dll)
  $third_party_tools = get_file_names(get-childitem -path $third_party_tools_dir -recurse -filter "*.dll")
  $third_party_exes = get_file_names(get-childitem -path $third_party_tools_dir -recurse -filter "*.exe")
  $bdd_doc_resources = get_file_names(get-childitem -path $third_party_tools_dir\bdddoc -recurse  -include @("*.css","*.jpg"))
  $all_third_party_dependencies = $third_party_tools  + $third_party_libraries + $third_party_exes + $bdd_doc_resources
  $all_sql_ddl_template_files = get_file_names(get-childitem -path $sql_ddl_dir -recurse -filter *.sql.template)
  $all_sql_data_template_files = get_file_names(get-childitem -path $sql_data_dir -recurse -filter *.sql.template)
  $all_sql_template_files = $all_sql_ddl_template_files , $all_sql_data_template_files
}

properties{ #files
	$studio_app_config = "$product_dir\$project_name\bin\debug\$project_name.dll.config" 
	$log4net_config = "$config_dir\log4net.config.xml" 
	$now = [System.DateTime]::Now
	$project_lib = "$project_name.dll"
	$project_test_lib = "$project_name.test.dll"
	$db_timestamp = "$sql_dir\db.timestamp"
	$nant_properties_file = "$build_dir\local_properties.xml"
}
	
properties{#logging
	$log_dir =  "$build_dir\logs"
	$log_file_name = "${log_dir}\log.txt"
	$log_level = "DEBUG"
}


properties{#transient folders for build process
  $transient_folders = new-object -typename System.Collections.ArrayList
  $transient_folders.add($build_artifacts_dir)
  $transient_folders.add($coverage_dir)
  $transient_folders.add($deploy_dir)
}

properties{#machine dependent external properties
  . $build_dir\local_properties.ps1
  $app_config = $local_settings.app_config_template.Replace(".template","");
  $app_config = "$config_dir\$app_config"
}

properties{#other build files
. .\studio_build.ps1
. .\web_build.ps1
. .\assembly_build.ps1
}

task default -depends init

task build_db -depends init{
  $files_changed = files_have_changed $all_sql_template_files $db_timestamp

  if ($files_changed -eq $true)
  {
    process_sql_files $script:all_sql_ddl_files  $local_settings.osql_exe "-E"
  }
  else
  {
    "DB is upto date"
  }
  touch $db_timestamp
}

task load_data -depends build_db {
  process_sql_files $script:all_sql_data_files $local_settings.osql_exe "-E"
}


task init -depends clean{
  $transient_folders | foreach-object{ make_folder $_ } 
  expand_all_template_files $all_template_files $local_settings
  $script:all_sql_ddl_files = get_file_names(get-childitem -path $sql_ddl_dir -recurse -filter *.sql)
  $script:all_sql_data_files = get_file_names(get-childitem -path $sql_data_dir -recurse -filter *.sql)
}

task clean{
  $transient_folders | foreach-object{ remove-item $_ -recurse -ErrorAction SilentlyContinue}
}

task compile -depends init{
 $result = MSBuild.exe "$base_dir\solution.sln" /t:Rebuild /p:Configuration=Debug
  $script:product_outputs = get_file_names(get-childitem -path $product_dir -recurse -filter *.dll)
  $script:product_debug_outputs = get_file_names(get-childitem -path $product_dir -recurse -filter *.pdb)

 $result
}

task prep_for_distribution -depends compile{
  $all_third_party_dependencies | foreach-object {copy-item -path $_ -destination $build_artifacts_dir}
  $script:product_outputs | foreach-object {copy-item -path $_ -destination $build_artifacts_dir}
}

task setup_test -depends prep_for_distribution{
    $script:product_outputs | foreach-object {copy-item -path $_ -destination $build_artifacts_dir}
    $script:product_debug_outputs | foreach-object {copy-item -path $_ -destination $build_artifacts_dir}
}

task test -depends setup_test{
    $xunit = "$third_party_tools_dir\mbunit\MbUnit.Cons.exe"
    $result = .$xunit $build_artifacts_dir\$project_test_lib /rt:"$($local_settings.xunit_report_type)" /rnf:"$($local_settings.xunit_report_file_name)" /rf:"$($local_settings.xunit_report_file_dir)" /sr

    $result
}



task run_ncover -depends setup_test{
  $xunit_app_console_args = "$build_artifacts_dir\$project_test_lib $($local_settings.xunit_console_args)" 

  ."$build_tools_dir\ncover\ncover.console.exe" $build_artifacts_dir\MBUnit.Cons.exe $build_artifacts_dir\$project_test_lib //reg //x "$coverage_file_name"  //a "$project_name"
  ."$build_tools_dir\ncover.explorer\NCoverExplorer.Console.exe" "$coverage_file_name" /s:"$coverage_dir\merged_report.xml"
  ."$build_tools_dir\ncover.explorer\NCoverExplorer.exe" "$coverage_dir\merged_report.xml"
 
}

task run_test_report -depends test{
   $result = ."$build_artifacts_dir\bdddoc.console.exe" "$build_artifacts_dir\$project_test_lib" "ObservationAttribute" "$build_artifacts_dir\SpecReport.html" "$($local_settings.xunit_report_file_dir)\$($local_settings.xunit_report_file_name_with_extension)"
}
