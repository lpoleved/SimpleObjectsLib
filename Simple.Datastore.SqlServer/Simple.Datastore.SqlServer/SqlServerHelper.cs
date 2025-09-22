using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
//using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using Microsoft.Win32;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Smo.RegisteredServers;
using Simple;
using Microsoft.Identity.Client;

namespace Simple.Datastore.SqlServer
{
    public class SqlServerHelper
    {
        public static IEnumerable<string> GetRegisteredServerNames()
        {
            List<string> result = new List<string>();
            string serverName;

            //// SMO Enum Servers
            //DataTable dt = SmoApplication.EnumAvailableSqlServers(localOnly: true);
            
            //if (dt.Rows.Count > 0)
            //{
            //    // Load server names into combo box
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        serverName = (String)dr["Name"];

            //        //only add if it doesn't exist
            //        if (!result.Contains(serverName))
            //            result.Add(serverName);
            //    }
            //}

            //// System.Data.Sql
            //DataTable sqlInstances = SqlDataSourceEnumerator.Instance.GetDataSources();

            //foreach (DataRow row in sqlInstances.Rows)
            //{
            //    serverName = row["ServerName"].ToString();

            //    if (row["InstanceName"] != null && row["InstanceName"].ToString() != string.Empty)
            //        serverName += string.Format(@"\{0}", row["InstanceName"]);

            //    //only add if it doesn't exist
            //    if (!result.Contains(serverName))
            //        result.Add(serverName);
            //}

            //// Registered Servers
            //RegisteredServerCollection rsvrs = SqlServerRegistrations.RegisteredServers; //SmoApplication.SqlServerRegistrations.EnumRegisteredServers();

            //foreach (RegisteredServer rs in rsvrs)
            //{
            //    String name = "";

            //    serverName = rs.ServerInstance.Replace(".", System.Environment.MachineName)
            //                            .Replace("(local)", System.Environment.MachineName)
            //                            .Replace("localhost", System.Environment.MachineName);
            //    //only add if it doesn't exist
            //    if (!result.Contains(serverName))
            //        result.Add(serverName);
            //}

            //Registry for local
            RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");

            if (registryKey != null)
            {
                String[]? instances = (String[])registryKey.GetValue("InstalledInstances");

                if (instances != null && instances.Length > 0)
                {
                    foreach (String element in instances)
                    {
                        //only add if it doesn't exist
                        if (element == "MSSQLSERVER")
                            serverName = System.Environment.MachineName;
                        else
                            serverName = System.Environment.MachineName + @"\" + element;

                        if (!result.Contains(serverName))
                            result.Add(serverName);
                    }
                }
            }

            var localDbInstances = GetLocalDbInstances();

            result.AddRange(localDbInstances);

			return result;
        }

		public static IEnumerable<string> GetLocalDbInstances()
		{
			List<string> result = new List<string>();
            string output = ShellHelper.ExecuteCommand("sqllocaldb info");
            string[] instances = null;


			if (output == null || output.Trim().Length == 0)
                return result;

            if (output.Contains("not recognized"))
            {                                       // Try find sqllocaldb.exe if LocalDB 2012 is installed
                output = ShellHelper.ExecuteCommand("\"C:\\Program Files\\Microsoft SQL Server\\110\\Tools\\Binn\\SqlLocalDB.exe info");

				if (output == null || output.Trim().Length == 0 || output.Contains("not recognized"))
					return result;
			}

			instances = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None); // Split output result into lines

			foreach (var item in instances)
				if (item.Trim().Length > 0)
					result.Add("(LocalDB)\\" + item.ToUpper());

            return result;


			//var proc2 = System.Diagnostics.Process.Start("ipconfig");

			////proc2.WaitForExit();
			
   //         string result1 = proc2.StandardOutput.ReadToEnd();


			//// Start the child process.
			//Process p = new Process();
			
   //         // Redirect the output stream of the child process.
			//p.StartInfo.UseShellExecute = false;
			//p.StartInfo.RedirectStandardOutput = true;
			//p.StartInfo.FileName = "cmd.exe";
			//p.StartInfo.Arguments = "sqllocaldb";
			//p.StartInfo.CreateNoWindow = true;
			//p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			//p.Start();
            
			
   //         // Do not wait for the child process to exit before reading to the end of its redirected stream.
			//p.WaitForExit();
			
   //         // Read the output stream first and then wait.
			//string sOutput = p.StandardOutput.ReadToEnd();

   //         //p.WaitForExit();

			//sOutput = p.StandardOutput.ReadToEnd();

			////If LocalDb is not installed then it will return that 'sqllocaldb' is not recognized as an internal or external command operable program or batch file.
			////if (sOutput == null || sOutput.Trim().Length == 0 || sOutput.Contains("not recognized"))
			////	return result;

			//string[] instances = sOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			
   //         foreach (var item in instances)
			//	if (item.Trim().Length > 0)
			//		result.Add(item);

			//ProcessStartInfo processStartInfo = new ProcessStartInfo();

   //         //processStartInfo.FileName = @"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\sqllocaldb.exe"; // "sqllocaldb"; // @"C:\Windows\system32\cmd.exe";
   //         processStartInfo.FileName = @"C:\Windows\system32\cmd.exe";
			//processStartInfo.Arguments = "sqllocaldb info"; // "/c date /t";
			////processStartInfo.CreateNoWindow = true;
			//processStartInfo.UseShellExecute = false;
			//    processStartInfo.RedirectStandardOutput = false;

			//Process process = new Process();
			
   //         process.StartInfo = processStartInfo;
			//process.Start();
			////process.WaitForExit();

			//string output = process.StandardOutput.ReadToEnd();

			//Console.WriteLine("Current date (received from CMD):");
			//Console.Write(output);


			//var  process1 = System.Diagnostics.Process.Start("CMD.exe", "sqllocaldb info");
			
   //         output = process1.StandardOutput.ReadToEnd();





			//// Prepare the process to run
			//ProcessStartInfo start = new ProcessStartInfo();
			//// Enter in the command line arguments, everything you would enter after the executable name itself
			//start.Arguments = "sqllocaldb info";
			//// Enter the executable to run, including the complete path
			//start.FileName = "cmd.exe";
			//// Do you want to show a console window?
			//start.WindowStyle = ProcessWindowStyle.Hidden;
			//start.CreateNoWindow = true;
			//int exitCode;


			//// Run the external process & wait for it to finish
			//using (Process proc3 = Process.Start(start))
			//{
			//	//proc.Start();
			//	//proc.WaitForExit();
			//	sOutput = proc3.StandardOutput.ReadToEnd();

			//	// Retrieve the app's exit code
			//	exitCode = proc3.ExitCode;
			//}

			//return result;
		}

       	public static IEnumerable<string> GetDatabaseNames(string sqlConnectionString)
        {
            List<string> result = new List<string>();
 
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                sqlConn.Open();
                DataTable tblDatabases = sqlConn.GetSchema("Databases");
                sqlConn.Close();

                foreach (DataRow row in tblDatabases.Rows)
                    result.Add(row["database_name"].ToString());
            }

            return result;
        }

        public static string InstallSqlServer(string sqlServerInstallationFilePath, string sqlServerInstanceName, string features, string sqlServerSaPassword = "sa")
		{
            StringBuilder info = new StringBuilder();
            
            if (File.Exists(sqlServerInstallationFilePath))
            {
                try
                {
                    info.AppendLine("\nInstalling SQL Server Express...");

                    //Process process = Process.Start(installFile, "/q /ACTION=Install /BROWSERSVCSTARTUPTYPE=Automatic /SQLSVCSTARTUPTYPE=Automatic /FEATURES=\"SQLEngine,Tools, ADV_SSMS, AS,RS,IS\ /INSTANCENAME=SQLExpress2008NETManager " +
                    //                           "/SQLSVCACCOUNT=\"NT AUTHORITY\\Network Service\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" /AGTSVCACCOUNT=\"NT AUTHORITY\\Network Service\" " +
                    //                           "/IACCEPTSQLSERVERLICENSETERMS /ADDCURRENTUSERASSQLADMIN=TRUE /INDICATEPROGRESS");

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    
                    startInfo.FileName = sqlServerInstallationFilePath;
                    startInfo.Arguments = "/QS /INSTANCENAME=" + sqlServerInstanceName + " /ACTION=Install /SECURITYMODE=SQL /SAPWD=" + sqlServerSaPassword + " /FEATURES=" + features + " /IACCEPTSQLSERVERLICENSETERMS /SkipRules=VSShellInstalledRule RebootRequiredCheck " +
                                          "/SQLSVCACCOUNT=\"NT AUTHORITY\\Network Service\" /SQLSYSADMINACCOUNTS=\"BUILTIN\\ADMINISTRATORS\" /AGTSVCACCOUNT=\"NT AUTHORITY\\Network Service\"";
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;

                    using (Process process = Process.Start(startInfo))
                    {
                        //
                        // Read in all the text from the process with the StreamReader.
                        //
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            info.Append(result);
                        }

                        process.WaitForExit();
                    }

                    info.AppendLine("Installation completed.");
                }
                catch (Exception ex)
                {
                    info.AppendLine(ex.GetFullErrorMessage());
                }
            }
            else
            {
                info.AppendLine("Specified installation file " + sqlServerInstallationFilePath + " does not exists");
            }

            return info.ToString();
        }

        public static void BackupSqlDatabase(string serverName, string databaseName, string destinationPath)
		{
			BackupSqlDatabase(serverName, databaseName, destinationPath, String.Empty, String.Empty);
		}

		//public static void BackupSqlDatabase(string serverName, string databaseName, string destinationPath, string serverUsername, string serverPassword)
		//{
		//	BackupSqlDatabase(serverName, databaseName, destinationPath, false, serverUsername, serverPassword);
		//}

		public static void BackupSqlDatabase(string serverName, string databaseName, string destinationPath, string serverUsername, string serverPassword)
		{
			SqlBackupDatabase backupDatabase = new SqlBackupDatabase();
			
            backupDatabase.Backup(serverName, databaseName, destinationPath, serverUsername, serverPassword);
		}

        public static void RestoreSqlDatabase(string serverName, string databaseName, string filePath, bool replaceDatabase)
        {
            RestoreSqlDatabase(serverName, databaseName, filePath, String.Empty, replaceDatabase);
        }

        public static void RestoreSqlDatabase(string serverName, string databaseName, string filePath, string dataFilePath, bool replaceDatabase)
        {
            SqlRestoreDatabase restoreDatabase = new SqlRestoreDatabase();
            
            restoreDatabase.Restore(serverName, databaseName, filePath, dataFilePath, replaceDatabase);
        }

        public class SqlRestoreDatabase
        {
            public void Restore(string serverName, string databaseName, string filePath, bool replaceDatabase)
            {
                this.Restore(serverName, databaseName, filePath, String.Empty, replaceDatabase);
            }

            public void Restore(string serverName, string databaseName, string filePath, string serverUsername, string serverPassword, bool replaceDatabase)
            {
                this.Restore(serverName, databaseName, filePath, String.Empty, serverUsername, serverPassword, replaceDatabase);
            }

            public void Restore(string serverName, string databaseName, string filePath, string dataFilePath, bool replaceDatabase)
            {
                this.Restore(serverName, databaseName, filePath, dataFilePath, String.Empty, String.Empty, replaceDatabase);
            }

            //public void Restore(string serverName, string databaseName, string filePath, string dataFilePath, string serverUsername, string serverPassword, bool replaceDatabase)
            //{
            //    this.Restore(serverName, databaseName, filePath, dataFilePath, true, serverUsername, serverPassword, replaceDatabase);
            //}

            public void Restore(string serverName, string databaseName, string filePath, string dataFilePath, string serverUsername, string serverPassword, bool replaceDatabase)
            {
                string connectionString = SqlServerHelper.GetConnectionString(serverName, serverUsername, serverPassword);
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                ServerConnection serverConnection = new ServerConnection(sqlConnection);
                Server sqlServer = new Server(serverConnection);

                this.Restore(sqlServer, databaseName, filePath, dataFilePath, replaceDatabase);
            }

            public void Restore(Server sqlServer, string filePath, string databaseName, bool replaceDatabase)
            {
                this.Restore(sqlServer, databaseName, String.Empty, replaceDatabase);
            }

            public void Restore(Server sqlServer, string databaseName, string filePath, string dataFilePath, bool replaceDatabase)
            {
                Database? database; // = sqlServer.Databases[databaseName];

                if (replaceDatabase == false && sqlServer.Databases.Contains(databaseName)) // Prevent overwriting existing database if replaceDatabase is set to false. This should be done restore.ReplaceDatabase not doing its job!!!
                    throw new Exception("The database " + databaseName + " already exists.");

                Restore restore = new Restore();
                BackupDeviceItem deviceItem = new BackupDeviceItem(filePath, DeviceType.File);
                
                restore.Devices.Add(deviceItem);
                restore.Database = databaseName;
                restore.Action = RestoreActionType.Database;
                restore.ReplaceDatabase = replaceDatabase; // If true, the database is overwritten!!!

                if (!String.IsNullOrEmpty(dataFilePath))
                {
                    DataTable dataTable = restore.ReadFileList(sqlServer);
                    DataRow[] dataFileList = dataTable.Select();
                    String dataFileLocation = String.Format("{0}\\{1}.mdf", dataFilePath, databaseName);
                    String logFileLocation = String.Format("{0}\\{1}.ldf", dataFilePath, databaseName);
                    RelocateFile rf = new RelocateFile(databaseName, dataFileLocation);
                    
                    restore.RelocateFiles.Add(new RelocateFile(dataFileList[0]["LogicalName"].ToString(), dataFileLocation));
                    restore.RelocateFiles.Add(new RelocateFile(dataFileList[1]["LogicalName"].ToString(), logFileLocation));
                }

                restore.Complete += new ServerMessageEventHandler(sqlRestore_Complete);
                restore.PercentCompleteNotification = 10;
                restore.PercentComplete += new PercentCompleteEventHandler(sqlRestore_PercentComplete);
                restore.SqlRestore(sqlServer);

                database = sqlServer.Databases[databaseName];
                
                if (database != null)
                    database.SetOnline();

                sqlServer.Refresh();
            }

            public event EventHandler<PercentCompleteEventArgs> PercentComplete;

            void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
            {
                if (PercentComplete != null)
                    PercentComplete(sender, e);
            }

            public event EventHandler<ServerMessageEventArgs> Complete;

            void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
            {
                if (Complete != null)
                    Complete(sender, e);
            }
        }

        public class SqlBackupDatabase
        {
            public void Backup(string serverName, string databaseName, string destinationPath)
            {
                this.Backup(serverName, databaseName, destinationPath, String.Empty, String.Empty);
            }

            //public void Backup(string serverName, string databaseName, string destinationPath, string serverUsername, string serverPassword)
            //{
            //    this.Backup(serverName, databaseName, destinationPath, false, serverUsername, serverPassword);
            //}

            public void Backup(string serverName, string databaseName, string destinationPath, string serverUsername, string serverPassword)
            {
                string connectionString = SqlServerHelper.GetConnectionString(serverName, serverUsername, serverPassword);
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                ServerConnection serverConnection = new ServerConnection(sqlConnection);
                Server sqlServer = new Server(serverConnection);

                this.Backup(sqlServer, databaseName, destinationPath);
            }
            
            public void Backup(Server sqlServer, string databaseName, string destinationPath)
            {
                Backup sqlBackup = new Backup();

                sqlBackup.Action = BackupActionType.Database;
                sqlBackup.BackupSetDescription = "ArchiveDataBase:" + DateTime.Now.ToShortDateString();
                sqlBackup.BackupSetName = "Archive";

                sqlBackup.Database = databaseName;

                BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath, DeviceType.File);
                //ServerConnection connection = new ServerConnection(serverName, userName, password);
                //Server sqlServer = new Server(connection);

                Database db = sqlServer.Databases[databaseName];

                sqlBackup.Initialize = true;
                sqlBackup.Checksum = true;
                sqlBackup.ContinueAfterError = true;

                sqlBackup.Devices.Add(deviceItem);
                sqlBackup.Incremental = false;

                sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
                sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

                sqlBackup.FormatMedia = false;

                sqlBackup.Complete += new ServerMessageEventHandler(sqlSqlBackup_Complete);
                sqlBackup.PercentCompleteNotification = 10;
                sqlBackup.PercentComplete += new PercentCompleteEventHandler(sqlBackup_PercentComplete);

                sqlBackup.SqlBackup(sqlServer);
            }

            public event EventHandler<PercentCompleteEventArgs> PercentComplete;

            void sqlBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
            {
                if (this.PercentComplete != null)
                    this.PercentComplete(sender, e);
            }

            public event EventHandler<ServerMessageEventArgs> Complete;

            void sqlSqlBackup_Complete(object sender, ServerMessageEventArgs e)
            {
                if (this.Complete != null)
                    this.Complete(sender, e);
            }
        }

        public static string GetConnectionString(string serverName)
        {
            return GetConnectionString(serverName, username: String.Empty, password: String.Empty);
        }

        public static string GetConnectionString(string serverName, string username, string password)
        {
            return GetConnectionString(serverName, databaseName: String.Empty, username, password);
        }

        public static string GetConnectionString(string serverName, string databaseName)
        {
            return GetConnectionString(serverName, databaseName, username: String.Empty, password: String.Empty);
        }

        public static string GetConnectionString(string serverName, string databaseName, string username, string password)
        {
            string connectionString;

            if (String.IsNullOrEmpty(username)) // Windows Authentication
            {
				connectionString = string.Format("Server={0}; Integrated Security=SSPI; TrustServerCertificate=True;", serverName);
				//connectionString = string.Format("Server={0}; Integrated Security=SSPI; Trusted_Connection=True;", serverName); // Encrypt=False
                //connectionString = string.Format("Server={0}; Integrated Security=SSPI; TrustServerCertificate=True;", serverName); // Encrypt=False
				//connectionString = string.Format("Data Source={0}; Integrated Security=True; Trust Server Certificate=True;", serverName);
			}
			else
            {
                connectionString = string.Format("Server={0}; User ID={1}; Password={2};", serverName, username, password);
            }

            if (!String.IsNullOrEmpty(databaseName))
                connectionString += string.Format(" Database={0};", databaseName);

            return connectionString;
        }
    }
}
