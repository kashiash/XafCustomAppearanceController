//-----------------------------------------------------------------------
// <copyright file="AppearanceController_RiskRating.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 11:46
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using XAF_CustomAppearanceViewController.Module.BusinessObjects;
using XAF_CustomAppearanceViewController.Module.BusinessObjects.Interface;


namespace XAF_CustomAppearanceViewController.Module.Win
{
	// For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
	public partial class AppearanceColorController : ViewController
	{
		AppearanceController appearanceController;


		public AppearanceColorController()
		{
			InitializeComponent();
			// Target required Views (via the TargetXXX properties) and create their Actions.
		}


		protected override void OnActivated()
		{
			base.OnActivated();
			// Perform various tasks depending on the target View.
			appearanceController = Frame.GetController<AppearanceController>();

			if(appearanceController != null)
			{
				appearanceController.CustomApplyAppearance += appearanceController_CustomApplyAppearance;
			}
		}
		protected override void OnDeactivated()
		{
			// Unsubscribe from previously subscribed events and release other references and resources.
			if(appearanceController != null)
			{
				appearanceController.CustomApplyAppearance -= appearanceController_CustomApplyAppearance;
			}

			base.OnDeactivated();
		}
		protected override void OnViewControlsCreated()
		{
			base.OnViewControlsCreated();
			// Access and customize the target View control.
		}
		void appearanceController_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
		{
			if ((e.ContextObjects == null) || (e.ContextObjects.Length != 1))
				return;
			PropertyEditor prop = e.Item as PropertyEditor;
			if (prop != null)
			{
				if (prop.PropertyValue != null && prop.PropertyValue is IColor)
				{
					IAppearanceFormat formattedItem = e.Item as IAppearanceFormat;
					formattedItem.BackColor = ((IColor)prop.PropertyValue).BackColor;
					formattedItem.FontColor = ((IColor)prop.PropertyValue).ForeColor;
				}
				else
				{
					IAppearanceFormat formattedItem = e.Item as IAppearanceFormat;
					formattedItem.ResetBackColor();
					formattedItem.ResetFontColor();
				}
			}

			GridViewRowCellStyleEventArgsAppearanceAdapter gridViewRowCellStyleAdapter = e.Item as GridViewRowCellStyleEventArgsAppearanceAdapter;
			if (gridViewRowCellStyleAdapter != null)
			{
				if (gridViewRowCellStyleAdapter.Args.CellValue != null && gridViewRowCellStyleAdapter.Args.CellValue is IColor)
				{
					e.AppearanceObject.BackColor = ((IColor)gridViewRowCellStyleAdapter.Args.CellValue).BackColor;
					e.AppearanceObject.FontColor = ((IColor)gridViewRowCellStyleAdapter.Args.CellValue).ForeColor;
				}
			}
		}

	}
}
