using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [DefaultProperty("RenkTanimId.Ad")]
    [ImageName("EditColors")]
    #region Persistent
    [Persistent("StokKartRenk")]
    #endregion
    public class StKartRenk : BaseObject
    { 
        public StKartRenk(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Stoklar stokId;
        private RenkTanimlari renkId;

        [XafDisplayName("Stok")]
        [Association("Stoklar-Renkler")]
        public Stoklar StokId
        {
            get => stokId;
            set => SetPropertyValue(nameof(StokId), ref stokId, value);
        }
        [XafDisplayName("Renk")]
        [Association("Renkler-StKartRenk")]
        public RenkTanimlari RenkTanimId
        {
            get => renkId;
            set => SetPropertyValue(nameof(RenkTanimId), ref renkId, value);
        }
    }
}