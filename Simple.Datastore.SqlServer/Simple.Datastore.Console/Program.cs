using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Simple.Datastore.SqlServer;

namespace Simple.Datastore.Console
{
	internal class Program
	{
		static void Main(string[] args)
		{
			if (args != null && args.Length > 0)
			{
				// install sql-express SqlExpressInstallationFilePath SqlServerInstanceName Password [Features]
				// example: install sql-express c:\Software\SQLEXPR32_x86_ENU.exe SQLNETMANAGER SaPassword1234 SQL, TOOLS
				if (args[0] == "install")
				{
					if (args.Length >= 5 && args[1] == "sql-server")
					{
						string sqlServerInstallationFilePath = args[2];
						string sqlServerInstanceName = args[3];
						string password = args[4];
						string features = "SQL";

						if (args.Length > 5) // Collect features in single string 
						{
							features = "";

							for (int i = 6; i < args.Length; i++)
								features += args[i];
						}

						string info = SqlServerHelper.InstallSqlServer(sqlServerInstallationFilePath, sqlServerInstanceName, features, password);

						System.Console.WriteLine(info);
					}
					else
					{
						System.Console.WriteLine("install command is not correct.");
						System.Console.WriteLine("usage: install sql-server SqlExpressInstallationFilePath SqlServerInstanceName sqlServerSaPassword [Features]");
						System.Console.WriteLine("example: install sql-server \"C:\\Software\\SQLEXPR32_x86_ENU.exe\" MYSQLEXPRESS sa SQL, TOOLS");
					}
				}
				// restore sql-database SqlServerInstanceName DatabaseName BackupFilePath [DataFilePath]
				// example: restore sql-database .\SQLEXPRESS NetManagerDatastore c:\Temp\NetManagerDatastore.bak [c:\Program Files\NET.Manager Studio\Datastore]
				else if (args[0] == "restore")
				{
					if ((args.Length == 5 || args.Length == 6) && args[1] == "sql-database")
					{
						try
						{
							string sqlServerInstanceName = args[2];
							string databaseName = args[3];
							string backupFilePath = args[4];
							string dataFilePath = (args.Length == 6) ? args[5] : String.Empty;

							System.Console.WriteLine($"\nTrying to restore database {databaseName} on {sqlServerInstanceName}...");
							//System.Console.WriteLine("\r\nsqlServerInstanceName=" + sqlServerInstanceName);
							//System.Console.WriteLine("databaseName=" + databaseName);
							//System.Console.WriteLine("ackupFilePath=" + backupFilePath);
							//System.Console.WriteLine("dataFilePath=" + dataFilePath);

							SqlServerHelper.RestoreSqlDatabase(sqlServerInstanceName, databaseName, backupFilePath, dataFilePath, replaceDatabase: false); // replaceDatabase is false -> No drop database if already exists 
							System.Console.WriteLine("Restore completed.");
							Thread.Sleep(5000);
							
							//System.Console.WriteLine("Press any key...");
							//System.Console.ReadKey();
						}
						catch (Exception ex)
						{
							System.Console.WriteLine(ex.GetFullErrorMessage());
							Thread.Sleep(5000);
						}
					}
					else
					{
						System.Console.WriteLine("restore command is not correct.");
						System.Console.WriteLine("usage: restore sql-database SqlServerInstanceName DatabaseName BackupFilePath [DataFilePath]");
						System.Console.WriteLine("example: restore sql-database .\\MYSQLEXPRESS MyDatabase \"C:\\Temp\\MyDatabase.bak\" [\"C:\\Program Files\\MyApp\"]");
					}
				}
				// backup sql-database SqlServerInstanceName DatabaseName BackupFilePath [Username Password]
				// example: restore sql-database .\SQLNETMANAGER NetManagerDatastore c:\Temp\NetManagerDatastore.bak [c:\Program Files\NET.Manager\Datastore]
				else if (args[0] == "backup")
				{
					if ((args.Length == 5 || args.Length == 7) && args[1] == "sql-database")
					{
						try
						{
							string sqlServerInstanceName = args[2];
							string databaseName = args[3];
							string backupFilePath = args[4];
							string username = (args.Length == 7) ? args[5] : String.Empty;
							string password = (args.Length == 7) ? args[6] : String.Empty;

							System.Console.WriteLine(String.Format("\nTrying to backup database {0} --> {1}...", databaseName, backupFilePath));
							SqlServerHelper.BackupSqlDatabase(sqlServerInstanceName, databaseName, backupFilePath, username, password);
							System.Console.WriteLine("Backup completed.");
						}
						catch (Exception ex)
						{
							System.Console.WriteLine(ex.GetFullErrorMessage());
						}
					}
					else
					{
						System.Console.WriteLine("backup command is not correct.");
						System.Console.WriteLine("usage: backupp sql-database SqlServerInstanceName DatabaseName BackupFilePath [Username Password]");
						System.Console.WriteLine("example: restore sql-database .\\MYSQLEXPRESS MyDatabase \"C:\\Temp\\MyDatabase.bak\" \"C:\\Program Files\\MyApp\" [myUsername, myPassword]");
					}
				}
				else
				{
					System.Console.WriteLine("unknown command");
				}
			}
		}
	}
}
