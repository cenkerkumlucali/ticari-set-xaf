using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace TicariSet.Module.BusinessObjects
{
    [DefaultClassOptions]
    [FileAttachment(nameof(File))]
    [Persistent("EkDosya")]
    [ImageName("BO_FileAttachment")]
    public class EkDosyalar : BaseObject
    { 
        public EkDosyalar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
        Stoklar stokId;
        Cariler cariId;
        string aciklama;
        private FileData file;

        [FileTypeFilter("Document files", 1, "*.txt", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.bmp", "*.png", "*.gif", "*.jpg")]
        [RuleUniqueValue("RUV-EkDosyalar.01", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [XafDisplayName("Dosya")]
        public FileData File
        {
            get => file;
            set => SetPropertyValue(nameof(File), ref file, value);
        }

        [Size(32)]
        public string Aciklama
        {
            get => aciklama;
            set => SetPropertyValue(nameof(Aciklama), ref aciklama, value);
        }

        [Association("Cariler-EkDosyalar")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [XafDisplayName("Cari")]
        public Cariler CariId
        {
            get => cariId;
            set => SetPropertyValue(nameof(CariId), ref cariId, value);
        }

        [Association("Stoklar-EkDosyalar")]
        [XafDisplayName("Stok")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public Stoklar StokId
        {
            get => stokId;
            set => SetPropertyValue(nameof(StokId), ref stokId, value);
        }
    }
}