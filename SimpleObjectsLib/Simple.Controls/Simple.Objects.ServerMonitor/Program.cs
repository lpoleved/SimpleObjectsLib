using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Simple.Collections;
using Simple.Modeling;
using Simple.Network;
using Simple.Objects.MonitorProtocol;
using Simple.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Ribbon;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Simple.Objects.Controls;

namespace Simple.Objects.ServerMonitor
{
    public static class Program
    {
        ////internal const string SettingLastServer = "Server";
        ////internal const string SettingLastUsername = "LastUsername";
        ////internal const string SettingServerPort = "ServerPort";
        private static HashList<ServerObjectModelInfo> serverObjectPropertyInfoByTableId = new HashList<ServerObjectModelInfo>();
        internal const int DefaultMonitorPort = 2021;
		public const string DefaultServer = "localhost";
		public const string DefaultUsername = "System Admin"; // "Monitor Admin";
		public const string DefaultPassword = "manager";

		//public static SimpleObjectModelDiscovery? ModelDiscovery;
		public static SimpleObjectMonitorClient MonitorClient = new SimpleObjectMonitorClient(); // = SimpleObjectMonitorClient.Empty;
        public static MonitorAppContext AppContext => MonitorAppContext.Instance;

        public static RibbonForm? MonitorForm = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        public static void Main()
        {
			// Get .NET version info
			//Assembly asm = Assembly.GetExecutingAssembly();
			Assembly? asm = Assembly.GetAssembly(typeof(FormMainOld2));

			Console.WriteLine("Compiled on .NET Version: {0}", asm?.ImageRuntimeVersion.ToString());
			Console.WriteLine("Running on .NET Version: {0}", Environment.Version.ToString());

			AppContext.Version = Assembly.GetEntryAssembly()?.GetName().Version;

			AppContext.UserSettings.DefaultRibonSkinName = "WXI"; // "Office 2010 Blue"; // "Office 2013 Dark Gray";;
			AppContext.UserSettings.DefaultRibbonStyle = (int)RibbonControlStyle.Default;
			AppContext.UserSettings.DefaultRibbonControlColorScheme = (int)RibbonControlColorScheme.Orange;

			AppContext.UserSettings.DefaultServer = DefaultServer;
			AppContext.UserSettings.DefaultPort = DefaultMonitorPort;
			AppContext.UserSettings.DefaultUsername = DefaultUsername;

			DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(MonitorAppContext.Instance.UserSettings.GetRibbonSkinName());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


			SplashScreenManager.ShowForm(parentForm: null, typeof(FormSplashScreen), true, true);

			//// Enforce assemblies to included in current domain in order to be discovered by SimpleObjectModelDiscovery engine in SimpleObjectsMonitorClient class.
			//foreach (AssemblyName assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
			//    Assembly.Load(assemblyName);

			//Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			//ModelDiscovery = new SimpleObjectModelDiscovery(assemblies);
			//MonitorClient = new SimpleObjectMonitorClient(); // ModelDiscovery);

			//if (Program.ConnectToServerAndAuthorize(Program.AppClient))



			//MonitorForm = new FormMain();
			//MonitorForm = new FormMainNew();
			MonitorForm = new FormMain();

			Application.Run(MonitorForm);

			//// Open Login form
			//try
			//{
			//	var client = new SimpleObjectsMonitorClient(AppDomain.CurrentDomain.GetAssemblies().ToList());
			//	bool connected = false;
			//	bool authorized = false;
			//	string server = "localhost";
			//	string username = "System Admin";
			//	FormLogin formLogin = new FormLogin("Test", server, username);
			//	DialogResult loginDialogResult = formLogin.ShowDialog();

			//	while (!connected || !authorized)
			//	{
			//		if (loginDialogResult == DialogResult.OK)
			//		{
			//			IPEndPoint endPoint = IpHelper.ParseIpEndPoint(formLogin.Server, 2021);

			//			connected = (client.IsConnected) ? true : client.Connect(endPoint);

			//			if (!connected)
			//			{
			//				XtraMessageBox.Show("Could not connect to remote server " + formLogin.Server, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//				loginDialogResult = formLogin.ShowDialog();
			//			}
			//			else
			//			{
			//				//ClientAppContext.Instance.UserSettings.SetValue(SettingLastServer, formLogin.Server);
			//				//ClientAppContext.Instance.UserSettings.Save();

			//				var test = client.GetProtocolVersion();
			//				var authorizedResult = client.Authorize(formLogin.Username, formLogin.Password);
			//				authorized = authorizedResult.ResultValue;

			//				if (!authorized)
			//				{
			//					XtraMessageBox.Show("Wrong username or password.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//					loginDialogResult = formLogin.ShowDialog();
			//				}
			//				else
			//				{
			//					//ClientAppContext.Instance.UserSettings.SetValue(SettingLastUsername, formLogin.Username);
			//					//ClientAppContext.Instance.UserSettings.Save();
			//				}
			//			}
			//		}
			//		else
			//		{
			//			return;
			//		}
			//	}

			//	formLogin.Close();
			//}
			//catch (Exception ex)
			//{
			//	XtraMessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			//	return;
			//}


		}

		public static async ValueTask<bool> TryConnectToServerAndAuthorize(SimpleObjectMonitorClient appClient)
        {
            bool connected = false;
            bool authorized = false;
            bool escape = false;
            int retry = 0;
            int numOfRetry = 3;
            string server = null; ;
            int port = 0;
            string username = null;
            FormLogin formLogin = null;

            try
            {
                if (appClient.IsConnected)
                    await appClient.CloseAsync();

                AppContext.UserSettings.DefaultServer = DefaultServer;
                AppContext.UserSettings.DefaultPort = DefaultMonitorPort;
                AppContext.UserSettings.DefaultUsername = DefaultUsername;

                server = AppContext.UserSettings.Server;
                port = AppContext.UserSettings.Port;
                username = AppContext.UserSettings.LastUsername;

                while ((!connected || !authorized) && retry++ < numOfRetry)
                {
#if !DEBUG
                    IPEndPoint endPoint = IpHelper.ParseIpEndPoint(server, port);
                    
                    connected = (appClient.IsConnected) ? true : await appClient.ConnectAsync(endPoint);

                    //               Version? version = null;

                    //if (connected)
                    //                   version = await appClient.GetProtocolVersion();

                    if (connected)
                    {
                        var response = await appClient.AuthenticateSession(username, Program.DefaultPassword);

						authorized = response.ResponseSucceeded && response.IsAuthenticated;
                    }

                    escape = !(connected && authorized);
#else
					formLogin = new FormLogin(MonitorAppContext.Instance.AppName, server, username);
					formLogin.Focus();
					DialogResult loginDialogResult = formLogin.ShowDialog();

					if (loginDialogResult == DialogResult.OK)
					{
						server = formLogin.Server;
						username = formLogin.Username;

						IPEndPoint endPoint = IpHelper.ParseIpEndPoint(formLogin.Server, DefaultMonitorPort);

						connected = (appClient.IsConnected) ? true : await appClient.ConnectAsync(endPoint);

						if (connected)
						{
							var response = await appClient.AuthenticateSession(formLogin.Username, formLogin.Password);

                            authorized = response.ResponseSucceeded && response.IsAuthenticated;
						}
					}
					else
					{
						escape = true;
					}

					formLogin.Close();
                   
					if (escape)
                       break;
#endif

                   if (!connected)
                    {
                        string serverInfo = formLogin?.Server ?? server;

						XtraMessageBox.Show("Could not connect to remote server " + serverInfo, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!authorized)
                    {
                        XtraMessageBox.Show("Wrong username or password.", "Authorization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

					if (escape)
						break;

					Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (formLogin != null)
                    formLogin.Close();
            }
            finally
            {
                if (connected)
                {
                    AppContext.UserSettings.Server = server;
                    AppContext.UserSettings.Port = port;
                }

                if (authorized)
                    AppContext.UserSettings.LastUsername = username;

                if (connected || authorized)
                    AppContext.UserSettings.Save();
            }

            return connected && authorized;
        }
    }
}
