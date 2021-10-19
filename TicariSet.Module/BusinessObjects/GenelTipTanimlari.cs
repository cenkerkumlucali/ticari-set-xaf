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
        Type _ObjectName;

        [XafDisplayName("Tanim")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField("RRF-Ad.01",DefaultContexts.Save,SkipNullOrEmptyValues = false)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }

        [Size(64)]
        [XafDisplayName("Nesne Tipi")]
        [ValueConverter(typeof(TypeStringConverter))]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [EditorAlias("TypeConverterEditor")]
        [RuleRequiredField("RRF-GnTip.ObjectName", DefaultContexts.Save, SkipNullOrEmptyValues = false)]
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