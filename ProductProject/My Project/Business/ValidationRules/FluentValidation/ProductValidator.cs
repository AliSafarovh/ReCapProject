using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.ProductName).NotEmpty(); //Rulofro ile sert veririk
            RuleFor(p=>p.ProductName).MinimumLength(2);
            RuleFor(p=>p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA)
                .WithMessage("Daxil Etdiyiniz Mehsul 'A' hefiri ile baslamalidir");
        }

        private bool StartWithA(string arg) 
        {
            return arg.StartsWith('A');
        }
    }
}
    