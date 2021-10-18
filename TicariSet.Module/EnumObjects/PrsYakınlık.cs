using DevExpress.ExpressApp.DC;

namespace TicariSet.Module.EnumObjects
{
    public enum PrsYakınlık
    {
        [XafDisplayName("Eş")]
        Es,
        [XafDisplayName("Çocuk")]
        Cocuk,
        Anne,
        Baba,
        [XafDisplayName("Kız Kardeş")]
        KızKardes,
        [XafDisplayName("Erkek Kardeş")]
        ErkekKardes
    }
}