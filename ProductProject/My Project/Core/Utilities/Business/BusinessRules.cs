using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        //params(istediyin qeder IResult tipinde metod elave et(Array kimi))
        public static IResult Run(params IResult[] logics) 
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;  // Metod ən birinci burada işləyir
                }
            }
            return null;
        }
    }
}
