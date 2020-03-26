//-----------------------------------------------------------------------
// <copyright file="Risk.cs">
//		Author: Ronald van der Does
// 		Copyright 2015-2020(c) Zero Boundaries Technology.
//		Modified 2020/02/04 11:17
//		All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Drawing;

namespace XAF_CustomAppearanceViewController.Module.BusinessObjects
{
    public interface IColor
    {
        Color BackColor { get; set; }
        Color ForeColor { get; set; }
    }
}