using Core.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface IFuelRepository : IAsyncRepository<Fuel, Guid>, IRepository<Fuel, Guid>
    {

    }

}
