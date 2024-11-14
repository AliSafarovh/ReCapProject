using Core.Persistence.Repositories;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public   interface IBrandRepository:IAsyncRepository<Brand,Guid>,IRepository<Brand,Guid>
    {
     
    }

}
