//-----------------------------------------------------------------------
// <copyright file="WinApplication.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 10:36
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win.Utils;


namespace XAF_CustomAppearanceViewController.Win
{
	// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Win.WinApplication._members
	public partial class XAF_CustomAppearanceViewControllerWindowsFormsApplication : WinApplication
	{
		public XAF_CustomAppearanceViewControllerWindowsFormsApplication()
		{
			InitializeComponent();
			InitializeDefaults();
		}


		protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
		{
			args.ObjectSpaceProviders.Add(new XPObjectSpaceProvider(InMemoryDataStoreProvider.ConnectionString, null, false));
			args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
		}
		void XAF_CustomAppearanceViewControllerWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e)
		{
			string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
			if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1)
			{
				e.Languages.Add(userLanguageName);
			}
		}
		void XAF_CustomAppearanceViewControllerWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
		{
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
			if(System.Diagnostics.Debugger.IsAttached)
			{
				e.Updater.Update();
				e.Handled = true;
			}
			else
			{
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

				if(e.CompatibilityError != null && e.CompatibilityError.Exception != null)
				{
					message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
				}
				throw new InvalidOperationException(message);
			}
#endif
		}


		#region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
		static XAF_CustomAppearanceViewControllerWindowsFormsApplication()
		{
			DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
			DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
			DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
		}
		void InitializeDefaults()
		{
			LinkNewObjectToParentImmediately = true;
			OptimizedControllersCreation = true;
			UseLightStyle = true;
			SplashScreen = new DXSplashScreen(typeof(XafSplashScreen), new DefaultOverlayFormOptions());
			ExecuteStartupLogicBeforeClosingLogonWindow = true;
		}
		#endregion
	}
}
