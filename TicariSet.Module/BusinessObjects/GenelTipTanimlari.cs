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
using System.Reflection;
using System.Text;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Utils;
using DevExpress.Xpo.Metadata.Helpers;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("PrintAllPages")]
    [DefaultProperty("Ad")]

    public class GenelTipTanimlari : XPObject
    {
        public GenelTipTanimlari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        DurumType durum;
        string ad;

        [XafDisplayName("Tanim")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }
       

        Type _ObjectName;
        [ValueConverter(typeof(TypeStringConverter))]
        [EditorAlias("TypeConverterEditor")]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [RuleRequiredField("RRF-GnTip.ObjectName", DefaultContexts.Save, SkipNullOrEmptyValues = false)]
        [Size(64)]
        [XafDisplayName("Nesne Tipi")]
        public Type ObjectName
        {
            get { return _ObjectName; }
            set { SetPropertyValue(nameof(ObjectName), ref _ObjectName, value); }
        }

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
    }
}