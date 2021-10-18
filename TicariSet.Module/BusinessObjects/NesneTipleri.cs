using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Tanim")]

    public class NesneTipleri : XPObject
    { 
        public NesneTipleri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        int kod;
        string tanim;
        
        public int Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }
    }
}