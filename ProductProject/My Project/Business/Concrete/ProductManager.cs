using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Apects.Autofac.Caching;
using Core.Apects.Autofac.Performance;
using Core.Apects.Autofac.Transaction;
using Core.Apects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Threading.Tasks;

namespace Business.Concrete
{   
    [PerformanceAspect(2)]
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
        [ValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect("IProductService")]
        [TransactionScopeAspect]
        public async Task<IResult> AddAsync(Product product)
        {
            // Validation kodlarini business de deyl Aspect de calistiraq

            #region BusinessCode LimitedError
            //// Business code (1 categoride en cox 10 mehsul olsun)
            //var productCount = await _productDal.GetAllAsync(p => p.CategoryId == product.CategoryId);
            //if (productCount.Count >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}
            #endregion

            // IResult tipindeki metodlari calistir.
            var result = await BusinessRules.Run(
              CheckIfProductofCategoryCorrect(product.CategoryId),
              CheckOfProductName(product.ProductName),
              CheckIfCategoryLimitedExceded()
 );

            if (result != null)
            {
                return result; // Əgər bir qayda pozularsa, səhv mesajı qaytarırıq
            }

            await _productDal.AddAsync(product);
            return new SuccessResult(Messages.ProductAdded);
        }


        public async Task <IResult> DeleteAsync(Product product)
        {
           await _productDal.DeleteAsync(product);
            return  new SuccessResult(Messages.ProductDeleted);
        }
        [CacheAspect]
        public async Task<IDataResult<List<Product>>> GetAllAsync()
        {
            var products = await _productDal.GetAllAsync();  
            return new SuccessDataResult<List<Product>>(products, Messages.ProductListed);
        }

        
        [CacheAspect]
        public async Task<IDataResult<List<Product>>> GetByCategoryIdAsync(int id)
        {
            var products = await _productDal.GetAllAsync(p => p.CategoryId == id);  // Asinxron olaraq məhsulları götürürük
            return new SuccessDataResult<List<Product>>(products, Messages.ProductListed);
        }

        public async Task<IDataResult<Product>> GetByIdAsync(int productId)
        {
            var product = await _productDal.GetAsync(p => p.ProductId == productId);  // Asinxron olaraq məhsul əldə edilir
            return new SuccessDataResult<Product>(product, Messages.ProductListed);
        }


        public async Task<IDataResult<List<Product>>> GetByUnitPriceAsync(decimal min, decimal max)
        {
            var products = await _productDal.GetAllAsync(p => p.UnitPrice >= min && p.UnitPrice <= max);  // Asinxron qiymət diapazonuna uyğun məhsulları alırıq
            return new SuccessDataResult<List<Product>>(products, Messages.ProductListed);
        }

        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetailsAsync()
        {
            var productDetails = await _productDal.GetProductDetailsAsync();  
            return new SuccessDataResult<List<ProductDetailDto>>(productDetails, Messages.ProductListed);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> UpdateAsync(Product product)
        {
           await  _productDal.UpdateAsync(product);
            return new SuccessResult(Messages.ProductUpdated);
        }


        #region Business Code

        // Eyni kateqoriyada maksimum 10 məhsul ola bilər.
        private async Task<IResult> CheckIfProductofCategoryCorrect(int categoryId)
        {
            var result = await _productDal.GetAllAsync(p => p.CategoryId == categoryId);
            if (result.Count >= 10) // Count async olmadığı üçün await etməyə ehtiyac yoxdur.
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        // Məhsul adının unikal olub-olmadığını yoxlayır.
        private async Task<IResult> CheckOfProductName(string productName)
        {
            var result = await _productDal.GetAsync(pn => pn.ProductName == productName);
            if (result != null) // Əgər məhsul tapılarsa, ErrorResult qaytar.
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            return new SuccessResult();
        }

        // Kateqoriya limiti 15-dən çox ola bilməz.
        private async Task<IResult> CheckIfCategoryLimitedExceded()
        {
            var result = await _categoryService.GetAllAsync();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitedExceded);
            }
            return new SuccessResult();
        }

        //private  IResult CheckOfMaintanceTime()
        //{
        //    if (DateTime.Now.Hour == 15)
        //    {
        //        return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        //    }
        //}
        #endregion

    }
}
