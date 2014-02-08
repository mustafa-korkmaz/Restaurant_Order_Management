using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram
{
    public class Enums
    {
        public enum Currency { TL = 0, USD, EUR, GPB };
        public enum ProductStatus { StoktaYok = 0, Stokta, TeminEdilemez, OnSiparisleGetirilir };
        public enum Unit { adet=0,g };
        public enum WarehouseStatus { KullanimDisi = 0, Kullanimda };
        public enum WarehouseType { Benim = 0, Ortak, Tedarikcimin};
        public enum Decision { Hayir = 0, Evet, Bilmiyorum };
        public enum UserRole { Admin = 0,Kasa, Garson };
        public enum RepositoryItemType { ComboBox = 0, ListBox }
        public enum Type { _null = 0, _string, _int };
        public enum PaymentType { Nakit = 0, Banka, Veresiye, Hediye };
        public enum SupplierPaymentType { Odeme = 0, Iade, Borc,Ilave };
        public enum TableStatus { Acik = 1, Kapali, Uygun, Dolu,Rezerve, Alindi,Kapandi };
        public enum OrderType { Restoran = 1, Paket};
        public enum PrinterType { Mutfak = 0, Kasa };
    }
}
