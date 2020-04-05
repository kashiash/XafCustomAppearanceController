﻿using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;
using JK.XAF.Module.Module.BusinessObjects;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Contact : Person, IMapsMarker, INumerowanieDok
    {

        private string webPageAddress;
        private Contact manager;
        private string nickName;
        private string spouseName;
        private TitleOfCourtesy titleOfCourtesy;
        private string notes;
        private DateTime? anniversary;
        public Contact(Session session) :
            base(session)
        {
        }
        public string WebPageAddress
        {
            get
            {
                return webPageAddress;
            }
            set
            {
                SetPropertyValue(nameof(WebPageAddress), ref webPageAddress, value);
            }
        }
        [DataSourceProperty("Department.Contacts", DataSourcePropertyIsNullMode.SelectAll)]
        [DataSourceCriteria("Position.Title = 'Manager' AND Oid != '@This.Oid'")]
        public Contact Manager
        {
            get
            {
                return manager;
            }
            set
            {
                SetPropertyValue(nameof(Manager), ref manager, value);
            }
        }
        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                SetPropertyValue(nameof(NickName), ref nickName, value);
            }
        }
        public string SpouseName
        {
            get
            {
                return spouseName;
            }
            set
            {
                SetPropertyValue(nameof(SpouseName), ref spouseName, value);
            }
        }
        public TitleOfCourtesy TitleOfCourtesy
        {
            get
            {
                return titleOfCourtesy;
            }
            set
            {
                SetPropertyValue(nameof(TitleOfCourtesy), ref titleOfCourtesy, value);
            }
        }
        public DateTime? Anniversary
        {
            get
            {
                return anniversary;
            }
            set
            {
                SetPropertyValue(nameof(Anniversary), ref anniversary, value);
            }
        }
        [Size(4096)]
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                SetPropertyValue(nameof(Notes), ref notes, value);
            }
        }
        private Department department;
        [Association("Department-Contacts"), ImmediatePostData]
        public Department Department
        {
            get
            {
                return department;
            }
            set
            {
                SetPropertyValue(nameof(Department), ref department, value);
                if (!IsLoading)
                {
                    Position = null;
                    if (Manager != null && Manager.Department != value)
                    {
                        Manager = null;
                    }
                }
            }
        }
        private Position position;
        public Position Position
        {
            get
            {
                return position;
            }
            set
            {
                SetPropertyValue(nameof(Position), ref position, value);
            }
        }
        [Association("Contact-DemoTask")]
        public XPCollection<DemoTask> Tasks
        {
            get
            {
                return GetCollection<DemoTask>(nameof(Tasks));
            }
        }
        private XPCollection<AuditDataItemPersistent> changeHistory;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                if (changeHistory == null)
                {
                    changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return changeHistory;
            }
        }

        private Location location;
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), VisibleInListView(false)]
        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                SetPropertyValue(nameof(Location), ref location, value);
            }
        }
        [VisibleInListView(false), VisibleInDetailView(false)]
        [PersistentAlias("FullName")]
        [System.ComponentModel.Browsable(false)]
        public string Title
        {
            get { return Convert.ToString(EvaluateAlias(nameof(Title))); }
        }
        [VisibleInListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Location.Latitude")]
        public double Latitude
        {
            get { return Convert.ToDouble(EvaluateAlias(nameof(Latitude))); }
        }
        [VisibleInListView(false), VisibleInDetailView(false)]
        [PersistentAlias("Location.Longitude")]
        public double Longitude
        {
            get { return Convert.ToDouble(EvaluateAlias(nameof(Longitude))); }
        }

        int nrKolejny;

        string numer;


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Numer
        {
            get
            {
                return numer;
            }
            set
            {
                SetPropertyValue(nameof(Numer), ref numer, value);
            }
        }

        
        public int NrKolejny
        {
            get
            {
                return nrKolejny;
            }
            set
            {
                SetPropertyValue(nameof(NrKolejny), ref nrKolejny, value);
            }
        }

        public override void AfterConstruction() {
            base.AfterConstruction();

            location = new Location(Session);
            location.Contact = this;
        }


        [Action(PredefinedCategory.RecordEdit,AutoCommit =true,Caption ="Przenumeruj")]
        public void Przenumeruj()
        {

            Numer = ".";
        }

        protected override void OnSaving()
        {


            //if (!(Session is NestedUnitOfWork)
            //    && (Session.DataLayer != null)
            //    && Session.IsNewObject(this)
            //    && (Session.ObjectLayer is SimpleObjectLayer)
            //&& string.IsNullOrEmpty(Numer))
            //{
            if (Numer == ".")
            {
                DocumentNumberGeneratorHelper.Generate(Session.DataLayer, this, "");
            }

            base.OnSaving();
        }


    }
    public enum TitleOfCourtesy {
        Dr,
        Miss,
        Mr,
        Mrs,
        Ms
    };
}
