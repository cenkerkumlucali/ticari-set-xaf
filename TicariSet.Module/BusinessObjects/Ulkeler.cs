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
    [DefaultProperty("Ad")]
    [CreatableItem(false)]
    [ImageName("BO_Country_v92")]
    public class Ulkeler : XPObject
    {
        public Ulkeler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string ad;

        [Size(32)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        [Association("Ulkeler-CariAdres")]
        public XPCollection<CariAdresBilgileri> CariAdresBilgileri =>
            GetCollection<CariAdresBilgileri>(nameof(CariAdresBilgileri));
    }
}