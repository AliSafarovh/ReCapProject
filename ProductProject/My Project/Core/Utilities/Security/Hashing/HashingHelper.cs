using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) //string tipinde password alacaq ve onu byte[] (out ile) seklinde xarice oturecek
        {

            using (var hmac=new System.Security.Cryptography.HMACSHA512()) //// HMACSHA512 alqoritmindən istifadə edərək şifrələmə obyektini yaradırıq
            {
                passwordSalt = hmac.Key;  //deyer kimi password yox, onun salt deyerini verecem 
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//o deyeri hash et.    
            }
        }
        
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)// Verilən şifrəni hash və salt dəyərləri ilə müqayisə edərək doğrulayan metod
        {
         
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))   // Yenə HMACSHA512 obyektini yaradıb, bu dəfə mövcud salt ilə şifrəni doğrulayırıq
            {
               
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Yenidən şifrəni hash edirik (eyni salt istifadə olunmaqla)

              
                for (int i = 0; i < computedHash.Length; i++)  // Hesablanmış hash dəyərini verilmiş hash dəyəri ilə müqayisə edirik
                {
                   
                    if (computedHash[i] != passwordHash[i]) // Əgər bir dəyər fərqli olarsa, şifrə yanlışdır
                    {
                        return false; // Doğrulama uğursuz oldu
                    }
                }

                return true; // Bütün dəyərlər eynidirsə, doğrulama uğurludur

            }
        }
    }
}
