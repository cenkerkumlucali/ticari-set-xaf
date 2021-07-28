using DevExpress.ExpressApp.DC;

namespace TicariSet.Module.EnumObjects
{
    public enum KasaHareketType
    {
        [XafDisplayName("Kasa Ödeme")]
        Odeme,
        [XafDisplayName("Kasa Tahsilat")]
        Tahsilat,
        [XafDisplayName("Banka Ödeme")]
        BankaOdeme,
        [XafDisplayName("Banka Tahsilat")]
        BankaTahsilat
    }
}