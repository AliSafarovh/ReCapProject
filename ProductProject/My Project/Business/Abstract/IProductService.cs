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
       Task <IDataResult<List<Product>>> GetAllAsync();  //evvelki List tipi bize sadece datani verirdise,IdataResult tipi Result sinfinden miras alaraq hem datani verir hemde metodun icrasi haqqinda mesaj.
        Task <IDataResult<List<Product>>> GetByCategoryIdAsync(int id);
        Task <IDataResult<List<Product>>> GetByUnitPriceAsync(decimal min, decimal max);
        Task <IDataResult<List<ProductDetailDto>>> GetProductDetailsAsync();
        Task <IResult> AddAsync(Product product); //Result tipi bize metodun icrasi haqqinda mesaj gonderir
        Task <IDataResult<Product>> GetByIdAsync(int productId);
        Task <IResult> UpdateAsync(Product product);
        Task <IResult> DeleteAsync(Product product);
        //IResult AddTransactionalTest(Product product); 
    }
}
