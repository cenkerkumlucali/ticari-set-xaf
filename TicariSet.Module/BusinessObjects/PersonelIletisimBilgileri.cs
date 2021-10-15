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
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Phone")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    public class PersonelIletisimBilgileri : XPObject
    {
        public PersonelIletisimBilgileri(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private PersonelKart personelId;
        private string iletisimBilgileri;
        private IletisimTipleri iletisimTipi;
        private string aciklama;

        [Size(64)]
        [RuleRequiredField]
        public string IletisimBilgileri
        {
            get => iletisimBilgileri;
            set => SetPropertyValue(nameof(IletisimBilgileri), ref iletisimBilgileri, value);
        }
        [RuleRequiredField]
        public IletisimTipleri IletisimTipi
        {
            get => iletisimTipi;
            set => SetPropertyValue(nameof(IletisimTipi), ref iletisimTipi, value);
        }

        [Size(128)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }
        [Association("Personel-IletisimBilgileri")]
        [XafDisplayName("Personel")]
        public PersonelKart PersonelId
        {
            get => personelId;
            set => SetPropertyValue(nameof(PersonelId), ref personelId, value);
        }
    }
}