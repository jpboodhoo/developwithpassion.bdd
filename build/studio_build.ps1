task from_ide -depends init{
  copy-item $app_config $studio_web_config
  copy-item $log4net_config "$web_ui_dir\log4net.xml"
  copy-item $app_config $studio_app_config
  copy-item $log4net_config "$product_dir\$project_name\bin\debug\log4net.xml"
}
