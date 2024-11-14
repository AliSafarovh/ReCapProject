using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void  Validate(IValidator validator,object entity) //ProductValidator   , entity - metodun product-i.  
           
        {
            var context = new ValidationContext<object>(entity); //context - product

            var result = validator.Validate(context); //ve hemin contexti validatorda yoxla
            if (!result.IsValid)//eger dogrulama serti odenmese
            {
                throw new ValidationException(result.Errors); //xeta mesajini gonder
            }
        }
    }
}
