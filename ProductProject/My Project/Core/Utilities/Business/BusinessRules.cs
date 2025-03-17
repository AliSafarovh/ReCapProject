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
        // params(istediyin qeder Task<IResult> tipinde metod elave et)
        public static async Task<IResult> Run(params Task<IResult>[] logics)
        {
            foreach (var logic in logics)
            {
                var result = await logic; // Her bir asinxron metodu icra edirik
                if (!result.Success)
                {
                    return result; // İlk səhvi tapanda geri qaytarırıq
                }
            }
            return null; // Bütün qaydalar uğurlu keçərsə, null qaytarır
        }
    }
}
