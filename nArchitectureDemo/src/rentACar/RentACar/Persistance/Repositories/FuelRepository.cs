using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entites;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class FuelRepository : EfRepositoryBase<Fuel, Guid, BaseDbContext>, IFuelRepository
    {
        public FuelRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
