using Core.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface ITransmissionRepository : IAsyncRepository<Transmission, Guid>, IRepository<Transmission, Guid>
    {

    }

}
