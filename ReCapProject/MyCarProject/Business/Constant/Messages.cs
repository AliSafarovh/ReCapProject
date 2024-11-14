using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Mehsul Yuklendi:";
        public static string ProductNameInvalid = "Gecersiz Ad";
        public static string MaintenanceTime = "Sistem Bakimda";
        public static string ProductListed = "Mehsullarin Siyahisi";
        public static string ProductDeleted = "Silinme Tamamlandi";
        public static string ProductUpdated = "Deyisdirilme Ugurla Tamamlandi";
        public static string ImageAdded = "Sekil Yuklendi";
        public static string ImageDeleted = "Sekil Silindi";
        public static string ImageUpdated = "Sekil Ugurla Deyisdirildi";
        public static string ImageLimitInvalid = "5 den cox sekil elave ede bilmezsiniz";
        internal static string ImageNotFound = "Sekil Tapilmadi";
    }
}