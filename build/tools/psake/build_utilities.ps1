  function combine_paths($path1,$path2)
  {
    return [System.IO.Path]::Combine($path1,$path2)
  }

  function touch($file_path)
{
  new-item -path $file_path -force -itemtype File
}

  function drop_folder($path)
  {
    if (test-path $path)
    {
      remove-item $path -force -recurse 
    }
  }

  function make_folder($path)
  {
    new-item -path $path -type directory | out-null
  }

  function get_filename($full_path){
    return [System.IO.Path]::GetFileName($full_path)
  }

  function process_sql_files($files,$sql_tool,$connection_string)
  {
    $files | foreach-object{ .$sql_tool $connection_string -i "$_" }
  }

  function copy_and_replace_tokens($template_file_path,$settings)
  {
    $contents = [System.IO.File]::ReadAllText($template_file_path)
    $settings.keys | foreach-object{
      $contents = $contents.Replace("@${_}@",$settings[$_])
    }
    $new_file = strip_template_extension $template_file_path
    [System.IO.File]::WriteAllText($new_file,$contents)
  }

  function files_have_changed($files_to_check,[System.String]$timestamp_file_path){
    
    $timestamp_exists = test-path $timestamp_file_path
    if($timestamp_exists -eq $false){return $true}

    $reference_file = get-item -path $timestamp_file_path
    $last_write_time = $reference_file.LastWriteTime
    $reference_file = $null

    foreach($file in $files_to_check)
    {
      $actual_file = get-item -path $file
      if($actual_file.LastWriteTime -gt $last_write_time)
      {
        $actual_file = $null
        return $true
      }
    }
      return $false
  }

  function get_file_names($files)
  {
    $file_names = new-object -typename System.Collections.ArrayList
    foreach ($file in $files)
    {
      [void]$file_names.Add($file.FullName)
    }
    return $file_names.ToArray()
  }

  function strip_template_extension($path){
    if ($path.EndsWith(".template"))
    { 
      return $path.Replace(".template","")
    }
    return $path
  }

  function expand_all_template_files($files,$settings)
  {
    $files | foreach-object{ copy_and_replace_tokens $_ $settings}
  }

  function kill_exe($name){
    taskkill "/f /im $name /fi 'STATUS eq RUNNING'"
  }

  function make_iis_dir($application_name,$path){
    $directory_entry = new-object System.DirectoryServices.DirectoryEntry -argumentList "IIS://localhost/W3SVC/1/Root"
    $child_directories = $directory_entry.psbase.Children
    $child_directories | where {$_.psbase.path.contains($application_name)} |foreach-object {
      $child_directories.remove($_)
    }

    $virtual_directory = $child_directories.Add($application_name,"IISWebVirtualDir")
    $virtual_directory.psbase.CommitChanges()
    $virtual_directory.psbase.Properties["Path"][0]= $path
    $virtual_directory.AppFriendlyName = $application_name
    $virtual_directory.psbase.CommitChanges()
    $virtual_directory.psbase.Invoke("AppCreate",$false)
  }

  function add_iis_mapping($application_name,$check_file_exists,$extension,$verbs,$executable){
    $mapping = "$extension,$executable,"

    if ($check_file_exists)
    {
      $mapping = $mapping + "5"
    }
    else
    {
      $mapping = $mapping + "1"
    }
    $mapping = $mapping + ",$verbs"
    $directory_entry = new-object System.DirectoryServices.DirectoryEntry -argumentList "IIS://localhost/W3SVC/1/Root/$application_name"

    if ($directory_entry -ne $null)
    {
      $directory_entry.psbase.RefreshCache()
      $directory_entry.ScriptMaps.Add($mapping)
      $directory_entry.psbase.CommitChanges()
      $directory_entry.psbase.Close()
    }
  }
