using DevExpress.ExpressApp.DC;

namespace TicariSet.Module.EnumObjects
{
    public enum PersonelKimlikVerilisNedeni
    {
        Kayıp,
        [XafDisplayName("Değiştirme")]
        Degistirme,
        Yenileme,
        [XafDisplayName("Kayıp")]
        Kayip
    }
}