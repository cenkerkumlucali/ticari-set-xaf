using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Toplam")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class EvrakBeden : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public EvrakBeden(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        int miktar;
        string beden;
        EvrakSatir evrakSatirId;
        int toplam;

        //rowcellclick
        //focusedcolumnchanged

        public EvrakSatir EvrakSatirId
        {
            get => evrakSatirId;
            set => SetPropertyValue(nameof(EvrakSatirId), ref evrakSatirId, value);
        }

        [Size(16)]
        public string Beden
        {
            get => beden;
            set => SetPropertyValue(nameof(Beden), ref beden, value);
        }
        
        public int Miktar
        {
            get => miktar;
            set
            {
                if (SetPropertyValue(nameof(Miktar), ref miktar, value)) ;
            }
        }


    }
}