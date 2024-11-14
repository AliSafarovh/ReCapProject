using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entites;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class CarRepository : EfRepositoryBase<Car, Guid, BaseDbContext>, ICarRepository
    {
        public CarRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
