using ETicaretAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction
{
    public interface IProductService
    {
    List<Product> GetALl();
    }
}
