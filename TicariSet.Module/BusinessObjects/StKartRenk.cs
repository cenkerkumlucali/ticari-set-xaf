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
    [DefaultProperty("RenkTanimId.Ad")]
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

        [Association("Stoklar-Renkler")]
        public Stoklar StokId
        {
            get => stokId;
            set => SetPropertyValue(nameof(StokId), ref stokId, value);
        }

        [Association("Renkler-StKartRenk")]
        public RenkTanimlari RenkTanimId
        {
            get => renkId;
            set => SetPropertyValue(nameof(RenkTanimId), ref renkId, value);
        }
    }
}