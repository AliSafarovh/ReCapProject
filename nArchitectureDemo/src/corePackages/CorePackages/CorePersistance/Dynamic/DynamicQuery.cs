using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic
{
    public class DynamicQuery
    {
        public IEnumerable<Sort>? Sort { get; set; }
        public Filter? Filter { get; set; }

        public DynamicQuery()
        {

        }

        public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
        {
            Filter = filter;
            Sort = sort;
        }
        //Misal
        //        // Məsələn, məhsul siyahısını qiymətə görə azalan, adı isə artan sıralamaq istəyiriksə
        //        var sortCriteria = new List<Sort>
        //{
        //    new Sort("Price", "desc"), // Qiymətə görə azalan
        //    new Sort("Name", "asc")    // Adına görə artan
        //};

        //        // Məhsulları yalnız qiyməti 50-dən aşağı olanlarla filtr edək
        //        var filterCriteria = new Filter("Price", "<") { Value = "50" };

        //        // DynamicQuery obyekti yaradaraq sıralama və filtr kriteriyalarını təyin edirik
        //        var dynamicQuery = new DynamicQuery(sortCriteria, filterCriteria);

    }
}
