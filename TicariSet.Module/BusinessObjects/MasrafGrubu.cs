using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("Action_CloneMerge_Clone_Object")]
    [DefaultProperty("Tanim")]
    [RuleCombinationOfPropertiesIsUnique("MasrafGrubuRule",DefaultContexts.Save,"Tanim",messageTemplate:"Girilen masraf grubu zaten mevcuttur.")]
    public class MasrafGrubu : XPObject
    {
        public MasrafGrubu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        string tanim;
        string kod;

        [VisibleInListView(false),VisibleInDetailView(false)]
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
        [Association("MasrafGrubu-Masraf")]
        public XPCollection<Masraflar> Masraf => GetCollection<Masraflar>(nameof(Masraf));

        protected override void OnSaving()
        {
            if (!(Session is NestedUnitOfWork
                  && (Session.DataLayer != null)
                  && Session.IsNewObject(this))
                && string.IsNullOrEmpty(Kod))
            {
                int deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "MasrafGrubuServerPrefix");
                Kod = string.Format("MG{0:D7}", deger);
            }


            base.OnSaving();
        }

    }
}