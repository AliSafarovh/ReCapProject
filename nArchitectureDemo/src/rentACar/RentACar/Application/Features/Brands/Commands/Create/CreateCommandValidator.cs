using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateCommandValidator:AbstractValidator<CreateBrandCommand> //Create isleyen zaman validatoru islet
    {
        public CreateCommandValidator()
        {
           RuleFor(c=>c.Name).NotEmpty().MinimumLength(2);        
        }
    }
}
