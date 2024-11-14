using Core.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface IModelRepository : IAsyncRepository<Model, Guid>, IRepository<Model, Guid>
    {

    }

}
