using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        //tehlukesizlik acari yaratmaq ucun
        public static SecurityKey CreateSecurityKey(string securitykey)//Bu açar, JWT-ni imzalamaq və doğrulamaq üçün istifadə ediləcəkdir.
        {
            // Verilən 'securitykey' stringini UTF-8 kodlaması ilə byte massivinə çeviririk.
            // Bu byte massivini istifadə edərək, SymmetricSecurityKey obyekti yaradırıq.
            // SymmetricSecurityKey simmetrik şifreleme üçün istifadə olunur, yəni həm şifreleme, həm də deşifreleme üçün eyni açar istifadə edilir.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey));
        }
    }
}
