using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entites;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ModelRepository : EfRepositoryBase<Model, Guid, BaseDbContext>, IModelRepository
    {
        public ModelRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
