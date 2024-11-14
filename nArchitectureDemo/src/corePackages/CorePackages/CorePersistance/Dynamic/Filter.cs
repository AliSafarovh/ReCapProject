using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic
{
    public class Filter
    {//Filter sinifi, məlumatları müəyyən meyarlara uyğun daraltmağa xidmət edir,
        public string Field { get; set; } //Filtrləmə üçün hansı sahədə əməliyyat aparılacağını göstərir. Məsələn, Name, Age və s. kimi məlumat sahələrini təyin edir.
        public string? Value { get; set; }//Field sahəsindəki dəyərlə müqayisə ediləcək dəyəri göstərir. Məsələn, əgər Field Namedirsə, Value "Ali" ola bilər.
        public string Operator { get; set; }//Filtrləmədə istifadə ediləcək əməliyyatı müəyyən edir. Məsələn, =, >, <, >= kimi müqayisə operatorları ola bilər.
        public string? Logic { get; set; }//Bir neçə filtr arasında əlaqəni təyin etmək üçün istifadə olunur. Məsələn, AND, OR kimi məntiq operatorları ilə bir neçə filtrin necə birləşəcəyini göstərir.

        public IEnumerable<Filter>? Filters { get; set; } //bir neçə şərti bir yerə toplamaq üçün istifadə olunur.

        public Filter()
        {
            Field = string.Empty;
            Operator = string.Empty;
        }

        public Filter(string field, string @operator)
        {
            Field = field;
            Operator = @operator;
        }

        //Meselen:
    //    var filter = new Filter
    //    {
    //        Logic = "OR", // Şərtləri birləşdirmək üçün 'OR' istifadə edirik
    //        Filters = new List<Filter>
    //{
    //    new Filter
    //    {
    //        Field = "Name", // Sahə adını göstəririk
    //        Operator = "=", // Müqayisə operatorunu göstəririk
    //        Value = "Ali"   // Ali adını axtarırıq
    //    },
    //    new Filter
    //    {
    //        Field = "Age",  // Sahə adını göstəririk
    //        Operator = ">", // Müqayisə operatorunu göstəririk
    //        Value = "25"    // 25-dən böyük yaşları axtarırıq
    //    }
    //}
    //    };

    }
}
