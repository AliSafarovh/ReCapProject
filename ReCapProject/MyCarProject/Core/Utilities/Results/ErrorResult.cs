using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message) //eger sert odenilmirse bu metod cagirilir
                                                                  //ve Error mesaji verilir
        {
        }
        public ErrorResult() : base(false) //sert odenilmedikde bu metod cagrilacaq ve sadece false qaytaracaq.
                                           //(mesaj vermeyecek)
        {

        }
    }
}
