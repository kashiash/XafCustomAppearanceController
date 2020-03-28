//-----------------------------------------------------------------------
// <copyright file="Risk.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 11:17
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Drawing;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using JK.XAF.Module.BusinessObjects;

namespace XAF_CustomAppearanceViewController.Module.BusinessObjects
{
	[DefaultClassOptions]
	public class Risk : BaseObject, IColor
	{
		public Risk(Session session) : base(session)
		{
		}


		[Index(2)]
		[ValueConverter(typeof(ColorValueConverter))]

		public Color BackColor
		{
			get
			{
				return GetPropertyValue<Color>(nameof(BackColor));
			}
			set
			{
				SetPropertyValue<Color>(nameof(BackColor), value);
			}
		}

		[Index(3)]
		[ValueConverter(typeof(ColorValueConverter))]
		public Color ForeColor
		{
			get
			{
				return GetPropertyValue<Color>(nameof(ForeColor));
			}
			set
			{
				SetPropertyValue<Color>(nameof(ForeColor), value);
			}
		}

		FontStyle? fontStyle;
		[Index(4)]
		public FontStyle? FontStyle
		{
			get => fontStyle;
			set => SetPropertyValue(nameof(FontStyle), ref fontStyle, value);
		}



		public String Description
		{
			get
			{
				return GetPropertyValue<String>(nameof(Description));
			}
			set
			{
				SetPropertyValue<String>(nameof(Description), value);
			}
		}



		public String Fullname
		{
			get
			{
				return string.Format("{0} ({1})", Name, Level);
			}
		}

		[Index(1)]
		[RuleUniqueValue("RiskRanking-Ranking-UniqueLevel", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
		public int Level
		{
			get
			{
				return GetPropertyValue<int>(nameof(Level));
			}
			set
			{
				SetPropertyValue<int>(nameof(Level), value);
			}
		}

		[Index(0)]
		public String Name
		{
			get
			{
				return GetPropertyValue<String>(nameof(Name));
			}
			set
			{
				SetPropertyValue<String>(nameof(Name), value);
			}
		}

		//public FontStyle? FontStyle { get; set; }



		public override void AfterConstruction()
		{
			base.AfterConstruction();
			// Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
		}
	}
}