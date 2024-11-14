using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult <List<Product>> GetAll();  //evvelki List tipi bize sadece datani verirdise,
        //IdataResult tipi Result sinfinden miras alaraq hem datani verir hemde metodun icrasi haqqinda mesaj.
        //Burada <List<Product>> <T> tipidir.
         IDataResult <List<Product>> GetByCategoryId(int id);
         IDataResult <List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult <List<ProductDetailDto>> GetProductDetails();
        IResult Add(Product product); //Result tipi bize metodun icrasi haqqinda mesaj gonderir
         IDataResult <Product> GetById(int productId);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
