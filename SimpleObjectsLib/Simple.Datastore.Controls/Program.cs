using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Simple.Datastore.SqlServer;

namespace Simple.Datastore.Controls
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                // install sql-express SqlExpressInstallationFilePath SqlServerInstanceName
                // example: install sql-express c:\Software\SQLEXPR32_x86_ENU.exe SQLNETMANAGER
                if (args.Length == 4 && args[0] == "install" && args[1] == "sql-express")
                {
                    string sqlExpressInstallationFilePath = args[2];
                    string sqlServerInstanceName = args[3];

                    if (File.Exists(sqlExpressInstallationFilePath))
                    {
                        try
                        {
                            Console.WriteLine("\nInstalling SQL Express 2008...");

                            //Process process = Process.Start(installFile, "/q /ACTION=Install /BROWSERSVCSTARTUPTYPE=Automatic /SQLSVCSTARTUPTYPE=Automatic /FEATURES=SQL /INSTANCENAME=SQLExpress2008NETManager " +
                            //                           "/SQLSVCACCOUNT=\"NT AUTHORITY\\Network Service\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" /AGTSVCACCOUNT=\"NT AUTHORITY\\Network Service\" " +
                            //                           "/IACCEPTSQLSERVERLICENSETERMS /ADDCURRENTUSERASSQLADMIN=TRUE /INDICATEPROGRESS");


                            Process process = Process.Start(sqlExpressInstallationFilePath, "/QS /INSTANCENAME=" + sqlServerInstanceName + " /ACTION=Install /FEATURES=\"SQL\" /IACCEPTSQLSERVERLICENSETERMS /SkipRules=VSShellInstalledRule RebootRequiredCheck " +
                                                            "/SQLSVCACCOUNT=\"NT AUTHORITY\\Network Service\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" /AGTSVCACCOUNT=\"NT AUTHORITY\\Network Service\"");
                            process.WaitForExit();

                            Console.WriteLine("Installation completed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.GetFullErrorMessage());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Specified installation file " + sqlExpressInstallationFilePath + " dos not exists");
                    }
                }
                // restore sql-database SqlServerInstanceName DatabaseName BackupFilePath [DataFilePath]
                // example: restore sql-database .\SQLNETMANAGER NetManagerDatastore c:\Temp\NetManagerDatastore_Empty.bak [c:\Program Files\DemoApp\Datastore]
                else if ((args.Length == 5 || args.Length == 6) && args[0] == "restore" && args[1] == "sql-database")
                {
                    try
                    {
                        string sqlServerInstanceName = args[2];
                        string databaseName = args[3];
                        string backupFilePath = args[4];
                        string dataFilePath = (args.Length == 6) ? args[5] : String.Empty;

                        Console.WriteLine("\nRestoring database...");
                        SqlServerHelper.RestoreSqlDatabase(sqlServerInstanceName, databaseName, backupFilePath, dataFilePath, replaceDatabase: false);
                        Console.WriteLine("Restore completed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.GetFullErrorMessage());
                    }
                }

                return 0;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BackupDatabaseForm());

            return 0;
        }
    }
}
