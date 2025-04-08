using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) //yeni SuccesResult bize sadece mesaj oturecek.(bool ise
                                                                   // :base(true) vasitesile Result da yoxlanilacaq)
        {
        }
        public SuccessResult() : base(true) //2ci metodu cagirmaq cun(sadece true qaytaracaq) (mesaj qaytarmayacaq)
        {

        }
    }
}
