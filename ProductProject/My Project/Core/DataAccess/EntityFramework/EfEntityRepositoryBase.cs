using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
{
    public async Task AddAsync(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            await context.Set<TEntity>().AddAsync(entity);  // Asinxron əlavə et
            await context.SaveChangesAsync();              // Asinxron yadda saxla
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            context.Set<TEntity>().Remove(entity);         // Sil
            await context.SaveChangesAsync();              // Asinxron yadda saxla
        }
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        using (TContext context = new TContext())
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter); // Asinxron tap
        }
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        using (TContext context = new TContext())
        {
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()                  // Hamısını gətir
                : await context.Set<TEntity>().Where(filter).ToListAsync();   // Filtrli gətir
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using (TContext context = new TContext())
        {
            context.Entry(entity).State = EntityState.Modified;  // Yenilə
            await context.SaveChangesAsync();                    // Asinxron yadda saxla
        }
    }
}
