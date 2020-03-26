//-----------------------------------------------------------------------
// <copyright file="Program.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 10:35
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;


namespace XAF_CustomAppearanceViewController.Win
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
			WindowsFormsSettings.LoadApplicationSettings();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			DevExpress.ExpressApp.BaseObjectSpace.ThrowExceptionForNotRegisteredEntityType = true;
			EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;

			if(Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
			{
				Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
			}
			Tracing.Initialize();

			XAF_CustomAppearanceViewControllerWindowsFormsApplication winApplication = new XAF_CustomAppearanceViewControllerWindowsFormsApplication();

			if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
			{
				winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			}
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
			if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
			{
				winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
			}
#endif
			try
			{
				winApplication.Setup();
				winApplication.Start();
			}
			catch(Exception e)
			{
				winApplication.StopSplash();
				winApplication.HandleException(e);
			}
		}
	}
}
