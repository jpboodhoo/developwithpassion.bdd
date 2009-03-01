$local_settings = @{
  	app_config_template = "app.config.xp.template" ;
    #app_config_template = "app.config.vista.template" ;

  	osql_connectionstring = "-E";
  	path_to_runtime_log4net_config = "$build_artifacts_dir\log4net.config.xml";
  	initial_catalog = "$project_name";
  	database_provider = "System.Data.SqlClient" ;
  	database_path = "C:\databases" ;
  	asp_net_worker_process = "aspnet_wp.exe";
  	startup_page = "Default.aspx";
  	browser_exe = "C:\program files\mozilla firefox\firefox.exe";
  	log_file_name = "log";
  	log_level = "DEBUG";
  	xunit_report_file_dir = "$build_artifacts_dir" ;
  	xunit_report_file_name = "test_report";
  	xunit_report_type = "text";
  	xunit_show_test_report = $true;
  	debug = "TRUE";
  }
$local_settings.xunit_report_file_name_with_extension = "$($local_settings.xunit_report_file_name).$($local_settings.xunit_report_type)"
$local_settings.sql_tools_path = "$env:SystemDrive\program files\microsoft sql server\100\tools\binn" ;
$local_settings.osql_exe = "$($local_settings.sql_tools_path)\osql.exe"
$local_settings.config_connectionstring = "data source=(local);Integrated Security=SSPI;Initial Catalog=$($local_settings.initial_catalog)"
#$local_settings.db_account_sql = $($local_settings.asp_net_account)] WITH PASSWORD=N'$($local_settings.asp_net_account)', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF")
$local_settings.asp_net_account = "$($env:computername)\ASPNet";
$local_settings.db_account_sql = "$($local_settings.asp_net_account)', N'$($local_settings.asp_net_account)'"
