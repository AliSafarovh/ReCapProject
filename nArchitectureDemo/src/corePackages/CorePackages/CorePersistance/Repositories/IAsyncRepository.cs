using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
    public interface IAsyncRepository<TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : Entity<TEntityId>
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,//Brand tipinde Find edir predicate axtarir.(predicate=x=>x.prop)
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includable = null,//Func<IQueryable<TEntity>(Brandtipini getir, IIncludableQueryable<TEntity, object>>?(Brand tipindeki modelleride getir) includable = null(model bosda ola biler))
         bool withDeleted = false,//silinmis brandlari getirme
         bool enableTracking = true,//upadte olunmus brand-leride getir.(enableTracking=treu yeni deyisikliyi save et. =false save etme. sadece melumatlari oxu)
         CancellationToken cancellationToken = default);//eger metodu cagirarken uzun zaman alarsa,prosesi cancel etmek ucundur

        Task<Paginate<TEntity>> GetListAsync(
       Expression<Func<TEntity, bool>>? predicate = null,//yalnız müəyyən bir kateqoriyaya aid olan məhsullar Find edib tapir
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,//qiymetlere gore siralama edir
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,//Func<IQueryable<TEntity>(Brandtipini getir, IIncludableQueryable<TEntity, object>>?(Brand tipindeki modelleride getir) includable = null(model bosda ola biler))
       int index = 0,//siralama 0-dan baslasin 
       int size = 10,//ilk 10mehsulu cixartsin
       bool withDeleted = false, 
       bool enableTracking = true,
       CancellationToken cancellationToken = default
   );
        Task<Paginate<TEntity>> GetListByDynamicAsync(
       DynamicQuery dynamic,//GetList kimi eyni metoddur, sadece GEtlist de het filtrleme ucun ayri kod yazmamaq ucun burada dynamicquery veririk.
       Expression<Func<TEntity, bool>>? predicate = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
       int index = 0,
       int size = 10,
       bool withDeleted = false,
       bool enableTracking = true,
       CancellationToken cancellationToken = default
   );

        Task<bool> AnyAsync(
           Expression<Func<TEntity, bool>>? predicate = null,
           bool withDeleted = false,
           bool enableTracking = true,
           CancellationToken cancellationToken = default //Bu metod eyni GEt metodudur sadece bize true veya false qaytarir. yeni mehsulun olub olmadigini verir. mehsul haqqinda melumat vermir.
       );
        Task<TEntity> AddAsync(TEntity entity);  //sade Task metodu sedece bir mehsul uzerinde isleyir. Range metodu ise eyni aynda bir nece mehsula tetbiq etmek imkani verir.

        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);

        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);

    }
}
