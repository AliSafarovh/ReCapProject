using Business.Concrete;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProductManager productManager = new ProductManager(new EfProductDal());
            //var result = productManager.GetProductDetails();
            //if (result.Success == true)
            //{
            //    foreach (var product in result.Data)
            //    {
            //        Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            //    }
            //}
            //else 
            //{
            //    Console.WriteLine(result.Message);
            //}
            
            #region Validators

            //Cross Custing Concerns
            //Validation, Log , CAche, Transaction, Authoritation ve s. 

            //Metod ise salinir.Atribut devreye girir.Evvelce atributun hansi tip oldugunu yoxlanilir.
            //Atribur ValidationAspect icinde yoxlanilirki bu IValidation daxilinde gelen Validation metodudurmu? eger deyilse Error.Message versin,
            //eger Validator Atributudursa  _validatorType = validatorType etsin.
            //  protected override void OnBefore(IInvocation invocation) Add metoduna gir . (invocation-metod)
            //1.Validatorun hansi tipe aidoldugunu tap:
            // var validator = (IValidator)Activator.CreateInstance(_validatorType); // ProductValidator
            //2.tapilan ProductValidator neyi yoxlayacaq? (Add olunan Product product-i)
            // var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // Product
            // var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // product
            //3.yoxlanilacaq mehsul askar olduqdan sonra Atributu ise salir.
            //foreach (var entity in entities)   (Product product)
            // {
            //   ValidationTool.Validate(validator, entity); 
            //}

            //2-ci merhele (ValidationTool)
            // public static void  Validate(IValidator validator,object entity) //ProductValidator   , entity - metodun product-i.
            // (Hansi Validatorun hansi entity ni yoxlayacagini mueyyen edir)
            //var context = new ValidationContext<object>(entity); //context - product (Validatordan kecmesi ucun xeyali product yaradir)
            // var result = validator.Validate(context); //ve hemin contexti validatorda yoxla
            // if (!result.IsValid)//eger dogrulama serti odenmese
            //{
            //     throw new ValidationException(result.Errors); //xeta mesajini gonder
            //}

            //Eger Validation dan kecdise try ile continue et ve sonda finally mesaji yolla
        //    var isSuccess = true;
        //    OnBefore(invocation);   Metodu ise sal
        //    try
        //    {
        //        invocation.Proceed();    metod isledi
        //    }
        //    catch (Exception e)
        //    {
        //        isSuccess = false;
        //        OnException(invocation, e);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (isSuccess)
        //        {
        //            OnSuccess(invocation);  succsees ver
        //        }
        //    }
        //    OnAfter(invocation);
        //}
        #endregion




        }
    }
}
