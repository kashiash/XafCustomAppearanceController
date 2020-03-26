//-----------------------------------------------------------------------
// <copyright file="DomainObject1.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 11:46
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using XAF_CustomAppearanceViewController.Module.BusinessObjects.Interface;


namespace XAF_CustomAppearanceViewController.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class DomainObject1 : BaseObject, IRisk
	{
		public DomainObject1(Session session) : base(session)
		{
		}


		public Risk CategoryA
		{
			get
			{
				return GetPropertyValue<Risk>(nameof(CategoryA));
			}
			set
			{
				SetPropertyValue<Risk>(nameof(CategoryA), value);
			}
		}

		public Risk CategoryB
		{
			get
			{
				return GetPropertyValue<Risk>(nameof(CategoryB));
			}
			set
			{
				SetPropertyValue<Risk>(nameof(CategoryB), value);
			}
		}

		public String ProjectTitle
		{
			get
			{
				return GetPropertyValue<String>(nameof(ProjectTitle));
			}
			set
			{
				SetPropertyValue<String>(nameof(ProjectTitle), value);
			}
		}


		public override void AfterConstruction()
		{
			base.AfterConstruction();
			// Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
		}
	}
}