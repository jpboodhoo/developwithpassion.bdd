properties{#directories
  $deploy_dir_bin = "$deploy_dir\bin"
  $deploy_dir_images = "$deploy_dir\images"
	$web_ui_dir = "$product_dir\web.ui" 
}

properties{#filesets
}

properties{ #files
}
	

properties{#asp app mapping settings
  $executable="$framework_dir\aspnet_isapi.dll"
  $extension = ".oo"
  $verbs = "GET,POST"
}


properties{#machine dependent external properties
	$run_url = "http://$env:computername/$($local_settings.virtual_directory_name)/$($local_settings.startup_page)"
}


task copy_web_project{
  remove-item $deploy_dir -recurse -ErrorAction SilentlyContinue
  copy-item $web_ui_dir $deploy_dir -recurse

  get-childitem $deploy_dir -include *.cs -recurse | foreach-object{remove-item $_ -force}

  kill_subversion_files $deploy_dir

  remove-item $deploy_dir\properties -recurse

  remove-item $deploy_dir\obj -recurse

  remove-item $deploy_dir\web.ui.csproj

  remove-item $deploy_dir\web.ui.csproj.user
}

task make_iis_dir{
  make_iis_dir "$($local_settings.virtual_directory_name)" "$deploy_dir"
  add_iis_mapping "$($local_settings.virtual_directory_name)" $false $extension $verbs $executable
}

task deploy -depends compile,copy_web_project,make_iis_dir{
    copy-item $log4net_config $deploy_dir
    copy-item $app_config $deploy_dir\web.config

   $result = .$build_tools_dir\winrar\rar.exe a $deploy_dir\dist.rar $deploy_dir

   $result
}

task run -depends deploy{
  .$local_settings.browser_exe $run_url
}
