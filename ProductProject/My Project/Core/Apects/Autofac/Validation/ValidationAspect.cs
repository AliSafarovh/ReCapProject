using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Apects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;  
        public ValidationAspect(Type validatorType) //Mene bir Aspect tipi ver
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //Validator oldugu yoxlanilir
            {
                throw new System.Exception("Bu bir Dogrulama sinifi deyil");
            }

            _validatorType = validatorType; //ve Aspect hazirdir
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // ProductValidator  
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // Product
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // product
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
