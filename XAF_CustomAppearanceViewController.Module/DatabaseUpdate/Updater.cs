//-----------------------------------------------------------------------
// <copyright file="Updater.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 11:06
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Updating;
using XAF_CustomAppearanceViewController.Module.BusinessObjects;


namespace XAF_CustomAppearanceViewController.Module.DatabaseUpdate
{
	// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
	public class Updater : ModuleUpdater
	{
		public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
				base(objectSpace, currentDBVersion)
		{
		}


		public override void UpdateDatabaseAfterUpdateSchema()
		{
			base.UpdateDatabaseAfterUpdateSchema();
			//string name = "MyName";
			//DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
			//if(theObject == null) {
			//    theObject = ObjectSpace.CreateObject<DomainObject1>();
			//    theObject.Name = name;
			//}

			Risk R10 = InsertRisk("Level: Green  10", 10, Color.Green);
			Risk R20 = InsertRisk("Level: Yellow 20", 20, Color.Yellow);
			Risk R30 = InsertRisk("Level: Orange 30", 30, Color.Orange);
			Risk R40 = InsertRisk("Level: Red    40", 40, Color.Red);

			InsertDomainObject1("Project 1", R10,R20);
			InsertDomainObject1("Project 2", R30,R10);
			InsertDomainObject1("Project 3", R20,R40);
			InsertDomainObject1("Project 4", R40,R20);
			InsertDomainObject1("Project 5", R40,R10);
			InsertDomainObject1("Project 6", R20,R30);
			InsertDomainObject1("Project 7", R30,R10);


			ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
		}
		public override void UpdateDatabaseBeforeUpdateSchema()
		{
			base.UpdateDatabaseBeforeUpdateSchema();
			//if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
			//    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
			//}
		}
		void InsertDomainObject1(String Description, Risk riskA, Risk riskB)
		{
			DomainObject1 domainObject = ObjectSpace.FindObject<DomainObject1>(new BinaryOperator("ProjectTitle", Description));

			if(domainObject == null)
			{
				domainObject = ObjectSpace.CreateObject<DomainObject1>();
				domainObject.ProjectTitle = Description;
				domainObject.CategoryA = riskA;
				domainObject.CategoryB = riskB;
			}


		}
		Risk InsertRisk(String Name, int Level, Color color)
		{
			Risk risk = ObjectSpace.FindObject<Risk>(new BinaryOperator(nameof(Name), Name));

			if(risk == null)
			{
				risk = ObjectSpace.CreateObject<Risk>();
				risk.Name = Name;
				risk.Level = Level;
				risk.BackColor = color;
			}

			return risk;
		}
	}
}
