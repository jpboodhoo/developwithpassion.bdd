properties{#directories
  $deploy_dir_bin = "$deploy_dir\bin"
  $deploy_dir_images = "$deploy_dir\images"
}


task deploy_assembly -depends prep_for_distribution{
#$result =  .$build_tools_dir\ilmerge\ilmerge.exe /out:$deploy_dir\$project_name.dll $build_artifacts_dir\$project_lib $build_artifacts_dir\FirstAssemblyToMerge
  get_file_names(get-childitem -path $build_artifacts_dir -filter $project_name*dll) | foreach-object{copy-item -path $_ $deploy_dir}

  $result
}

