using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.SystemModule;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Birimler", "[Durum] == false", true)]
    [ListViewFilter("Pasif Birimler", "[Durum] == true")]

    public class Birimler : XPObject
    {
        public Birimler(Session session)
           : base(session)
        {
        }
        public override void AfterConstruction()
        {

            base.AfterConstruction();
            Durum = DurumType.Aktif;
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }


        bool varsayilan;
        DurumType durum;
        string tanim;
        string kod;

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
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

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }

        public bool Varsayilan
        {
            get => varsayilan;
            set => SetPropertyValue(nameof(Varsayilan), ref varsayilan, value);
        }
        protected override void OnSaving()
        {

            if (Varsayilan)
            {
                Birimler birim = Session.FindObject<Birimler>(new BinaryOperator(nameof(birim.Varsayilan), true));
                if (birim != null)
                {
                    birim.varsayilan = false;
                    birim.Save();
                }
            }
            base.OnSaving();
        }
    }
}