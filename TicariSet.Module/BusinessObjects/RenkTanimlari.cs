using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Persistent.Validation;
using DevExpress.Utils.Design;
using DevExpress.XtraEditors;
using TicariSet.Module.EnumObjects;


namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultBindingPropertyEx("Color")]
    [ImageName("RedWhiteBlueColorScale")]
    [DefaultProperty("Ad")]
    [RuleCombinationOfPropertiesIsUnique("RenkTanimlariAdRule",DefaultContexts.Save,"Ad",messageTemplate:"Girilen renk zaten mevcuttur.")]
    public class RenkTanimlari : BaseObject
    { 
        public RenkTanimlari(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string kod;
        private string ad;
        private Color rgbKod;
        private DurumType durum;
        private byte[] resim;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(32)]
        public string Ad
        {
            get => ad;
            set => SetPropertyValue(nameof(Ad), ref ad, value);
        }
        [ValueConverter(typeof(ColorConverter))]
        public Color RgbKod
        {
            get => rgbKod;
            set => SetPropertyValue(nameof(RgbKod), ref rgbKod, value);
        }

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }

        [ImageEditor]
        public byte[] Resim
        {
            get => resim;
            set => SetPropertyValue(nameof(Resim), ref resim, value);
        }

        [Association("Renkler-StKartRenk")]
        public XPCollection<StKartRenk> StKartRenk => GetCollection<StKartRenk>(nameof(StKartRenk));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                  && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "RenkServerPrefix");
                Kod = string.Format("RNK{0:D3}", deger);
            }
            base.OnSaving();
        }
    }

}