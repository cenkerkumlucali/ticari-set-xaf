using DevExpress.ExpressApp.DC;

namespace TicariSet.Module.EnumObjects
{
    public enum IletisimTipleri
    {
        [XafDisplayName("Telefon-1")]
        Cep1 = 0,
        [XafDisplayName("Telefon-2")]
        Cep2 = 1,
        [XafDisplayName("Telefon-3")]
        Cep3 = 2,
        [XafDisplayName("Gsm")]
        Gsm = 3,
        [XafDisplayName("E-posta")]
        Eposta = 4,
        Fax = 5,
        [XafDisplayName("Web Adresi")]
        WebAdresi = 6
    }
}