using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic
{
    public class Sort
    {//Sort sinifi isə məlumatları müvafiq sahəyə görə sıralamağı təmin edir(Mes;qiymetleri artan ya azalan sirayla duz)
        public string Field { get; set; }
        public string Dir { get; set; }

        public Sort()
        {
            Field = string.Empty;
            Dir = string.Empty;
        }

        public Sort(string field, string dir)
        {
            Field = field;
            Dir = dir;
        }
    }
}
