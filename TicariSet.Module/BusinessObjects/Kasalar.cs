using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using TicariSet.Module.EnumObjects;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Business_Dollar")]
    [DefaultProperty("Tanim")]
    [ListViewFilter("Tüm Liste", "")]
    [ListViewFilter("Aktif Kasalar", "[Durum] == false", true)]
    [ListViewFilter("Pasif Kasalar", "[Durum] == true")]
    [RuleIsReferenced("RIR-Kasalar.StokHareketler", DefaultContexts.Delete,typeof(KasaHareket), "KasaID", InvertResult = true, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, MessageTemplateMustBeReferenced = "{TargetObject} nesne referans alınmamalıdır.")]

    public class Kasalar : XPObject
    { 
        public Kasalar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Durum = 0;
        }

        DurumType durum;
        string aciklama;
        string tanim;
        string kod;

        [VisibleInDetailView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Kod
        {
            get => kod;
            set => SetPropertyValue(nameof(Kod), ref kod, value);
        }

        [Size(32)]
        [RuleRequiredField]
        public string Tanim
        {
            get => tanim;
            set => SetPropertyValue(nameof(Tanim), ref tanim, value);
        }

        [Size(128)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        public DurumType Durum
        {
            get => durum;
            set => SetPropertyValue(nameof(Durum), ref durum, value);
        }
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Tahsilat => KasaHareketleri.Where(c => c.Hareket == KasaHareketType.Tahsilat).Sum(c => c.Tutar);
       
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Odeme => KasaHareketleri.Where(c => c.Hareket == KasaHareketType.Odeme).Sum(c => c.Tutar);
       
        [ModelDefault("DisplayFormat", "c2")]
        [ModelDefault("EditFormat", "c2")]
        public double Bakiye => Tahsilat - Odeme;

        [Association("Kasalar-KasaHareketleri")]
        public XPCollection<KasaHareket> KasaHareketleri => GetCollection<KasaHareket>(nameof(KasaHareketleri));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "KasalarServerPrefix");
                Kod = string.Format("{0:D2}", deger);
            }
            base.OnSaving();
        }
    }
}