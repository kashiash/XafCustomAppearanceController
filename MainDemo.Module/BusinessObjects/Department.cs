using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using JK.XAF.Module.BusinessObjects;
using System.Drawing;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty(nameof(Department.Title))]
    public class Department : BaseObject, IColor
    {
        private string title;
        private string office;
        public Department(Session session)
            : base(session) {
        }
        public string Title {
            get {
                return title;
            }
            set {
                SetPropertyValue(nameof(Title), ref title, value);
            }
        }
        public string Office {
            get {
                return office;
            }
            set {
                SetPropertyValue(nameof(Office), ref office, value);
            }
        }
        [Association("Department-Contacts")]
        public XPCollection<Contact> Contacts {
            get {
                return GetCollection<Contact>(nameof(Contacts));
            }
        }
        [Association("Departments-Positions")]
        public XPCollection<Position> Positions {
            get {
                return GetCollection<Position>(nameof(Positions));
            }
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


        [Index(4)]
        public FontStyle FontStyle
        {
            //get => fontStyle;
            //set => SetPropertyValue(nameof(FontStyle), ref fontStyle, value);
            get
            {
                return GetPropertyValue<FontStyle>(nameof(FontStyle));
            }
            set
            {
                SetPropertyValue<FontStyle>(nameof(FontStyle), value);
            }
        }
    }
}
