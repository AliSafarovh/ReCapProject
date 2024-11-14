using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;
        ICategoryService _categoryService;
    
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof (ProductValidator))] 
        //Atribut-Add metodunu run etmemisden evvel Atributu ise sal 
        //Meselen: ProductValidator atributu ile dogralama et sonra Add metodunu run et
        public IResult Add(Product product)
        {
           
            //Validation kodlarini business de deyl Aspect de calistiraq

            IResult result = BusinessRules.Run(CheckIfProductofCategoryCorrect(product.CategoryId)
                ,CheckOfProductName(product.ProductName),CheckIfCategoryLimitedExceded( ));//IResult tipindeki metodlari calistir.
            if (result != null) 
            { return result; }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);      
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        public IDataResult <List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 15)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult <List<Product>> GetByCategoryId(int id)
        {
            return new SuccessDataResult <List<Product>>(_productDal.GetAll(p=>p.CategoryId==id),Messages.ProductListed);
        }

        public IDataResult <Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId),Messages.ProductListed);
        }

        public IDataResult <List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max),Messages.ProductListed);
        }

        public IDataResult <List<ProductDetailDto>> GetProductDetails()
        {
           return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductListed);  
        }

        public IResult Update (Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

     private IResult CheckIfProductofCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result <=10 )
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckOfProductName(string productName)
        {
            var result = _productDal.GetAll(pn => pn.ProductName == productName);
            if(result != null)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitedExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitedExceded);
            } 
            return new SuccessResult();
        }
    }
}
