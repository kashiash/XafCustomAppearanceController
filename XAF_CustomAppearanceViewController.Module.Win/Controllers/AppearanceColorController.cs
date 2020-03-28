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
using JK.XAF.Module.BusinessObjects;

namespace JK.XAF.Module.Win
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public  class AppearanceColorController : ViewController
    {
        AppearanceController appearanceController;


        public AppearanceColorController()
        {

        }


        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            appearanceController = Frame.GetController<AppearanceController>();

            if (appearanceController != null)
            {
                appearanceController.CustomApplyAppearance += appearanceController_CustomApplyAppearance;
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            if (appearanceController != null)
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

            if (View is DetailView)
            {
                PropertyEditor prop = e.Item as PropertyEditor;
                if (prop != null)
                {
                    IAppearanceFormat formattedItem = e.Item as IAppearanceFormat;
                    if (formattedItem != null)
                    {
                        if (prop.PropertyValue != null && prop.PropertyValue is IColor)
                        {
                            formattedItem.BackColor = ((IColor)prop.PropertyValue).BackColor;
                            formattedItem.FontColor = ((IColor)prop.PropertyValue).ForeColor;
                            formattedItem.FontStyle = ((IColor)prop.PropertyValue).FontStyle ?? 0;
                        }

                        else
                        {
                            formattedItem.ResetBackColor();
                            formattedItem.ResetFontColor();
                            formattedItem.ResetFontStyle();
                        }
                    }
                }
            }


            if ((View is ListView))
            {
                GridViewRowCellStyleEventArgsAppearanceAdapter gridViewRowCellStyleAdapter = e.Item as GridViewRowCellStyleEventArgsAppearanceAdapter;
                if (gridViewRowCellStyleAdapter != null)
                {
                    if (gridViewRowCellStyleAdapter.Args.CellValue != null && gridViewRowCellStyleAdapter.Args.CellValue is IColor)
                    {
                        e.AppearanceObject.BackColor = ((IColor)gridViewRowCellStyleAdapter.Args.CellValue).BackColor;
                        e.AppearanceObject.FontColor = ((IColor)gridViewRowCellStyleAdapter.Args.CellValue).ForeColor;
                        e.AppearanceObject.FontStyle = ((IColor)gridViewRowCellStyleAdapter.Args.CellValue).FontStyle;

                    }
                }
            }
        }

    }
}
