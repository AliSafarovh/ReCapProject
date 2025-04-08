using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //Atributu istifade ederek class ve metoddaki atriburlari oxu, 1den cox ise sal(hem Db hem project ucun),
    //Inherited icinde de istifade olunsun
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }  //Bu metod hansi atributun ilk isleyeceyine qerar verir

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
