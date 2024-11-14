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
        public static string ProductNameInvalid = "Bu adda Mehsul artiq movcuddur";
        public static string MaintenanceTime="Sistem Bakimda";
        public static string ProductListed="Mehsullarin Siyahisi";
        public static string ProductDeleted = "Mehsul Silindi";
        public static string ProductUpdated = "Mehsul Deyisdirilidi";
        public static string ProductCountOfCategoryError = "Daxil Etdiyiniz mehsulun en Kategorisinde en az 10 Mehsul olmalidir";
        public static string CategoryLimitedExceded = "kategori Limiti Asildi";
        public static string AuthorizationDenied = "Selahiyetiniz Yoxdur";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";
    }
}
