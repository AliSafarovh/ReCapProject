using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>
             {
              new Product { ProductId = 1, CategoryId = 1, ProductName = "Tv", UnitPrice = 1550, UnitsInStock = 1500 },
              new Product { ProductId = 2, CategoryId = 1, ProductName = "Smartphone", UnitPrice = 1550, UnitsInStock = 1500 },
              new Product { ProductId = 3, CategoryId = 3, ProductName = "Yataq", UnitPrice = 1550, UnitsInStock = 1500 },
              new Product { ProductId = 4, CategoryId = 2, ProductName = "Tava", UnitPrice = 1550, UnitsInStock = 1500 }
             };
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetbyCategoryId(int categoryId)
        {
            return _products.Where(p=>p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        List<ProductDetailDto> IProductDal.GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
