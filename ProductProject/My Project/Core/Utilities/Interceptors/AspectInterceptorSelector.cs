using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;



namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList(); //classin atributunlarini oxu 
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //metodun atributlarini oxu ve
                                                                             //onlari bir listeye qoy
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
            //Butun metodlari avtomatik Loglama yapir.
            //Helelik bize lazm deyl

            return classAttributes.OrderBy(x => x.Priority).ToArray();//Prioritory metodu ile
                                                               //atributlarin islenme ardicilligini qur.
        }
    }
}
