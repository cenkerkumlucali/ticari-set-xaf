using DevExpress.ExpressApp.DC;

namespace TicariSet.Module.EnumObjects
{
    public enum AdresTipi 
    {
        [XafDisplayName("Ev Adresi")]
        Ev = 0,
        [XafDisplayName("İş Adresi")]
        Is = 1,
        [XafDisplayName("Fatura Adresi")]
        Fatura = 2
    }
}