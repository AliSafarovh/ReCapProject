using Core.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface ICarRepository : IAsyncRepository<Car, Guid>, IRepository<Car, Guid>
    {

    }

}
