using DevExpress.ExpressApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace JK.XAF.Module.Interfaces.IModelNodes
{
    public interface IModelAllowFilteringCriterion : IModelNode
    {
        [Category("CustomSettings")]
        [DefaultValue(true)]
        bool PokazujFiltryNaWydruku { get; set; }

        [Category("CustomSettings")]
        [DefaultValue(true)]
        bool WlaczFiltry { get; set; }
    }
}
