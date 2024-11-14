using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity, TEntityId, TContext>
        : IAsyncRepository<TEntity, TEntityId>, IRepository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TContext : DbContext
    {
        private readonly TContext Context;
        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;//Bolge saati
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.CreatedDate = DateTime.UtcNow;
            await Context.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();//verilənlər bazasından məlumatları çəkmək üçün bir IQueryable obyektini yaradır
            if (!enableTracking)
                queryable = queryable.AsNoTracking();//verilənlər üzərində edilən dəyişikliklər izlənməyəcək və performans artırılacaq.
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters(); //Əgər withDeleted parametri true olaraq təyin olunubsa, IgnoreQueryFilters() metodu çağırılır. Bu metod sayəsində, adi halda gizlədilən(silinmiş) qeydlər də daxil edilir.
            if (predicate != null)
                queryable = queryable.Where(predicate);//eger sert varsa serte uygun olanlari tap getir
            return await queryable.AnyAsync(cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entity, permanent);
            await Context.SaveChangesAsync();
            return entity;
        }



        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entities, permanent);
            await Context.SaveChangesAsync();
            return entities;
        }


        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();//verilənlər bazası ilə əlaqəli sorğunu (queryable) yaradaraq başlanğıc nöqtəsini təyin edir. Yəni, bu nöqtədən başlayaraq sorğuya əlavə şərtlər və filtrasyonlar əlavə edəcəyik.
            if (!enableTracking)
                queryable = queryable.AsNoTracking();// Əgər enableTracking parametri false - dursa, AsNoTracking() metodu istifadə olunur.Bu, Entity Framework-ə həmin obyekt üzərində dəyişiklikləri izləməməsini(tracking etməməsini) söyləyir
            if (include != null)
                queryable = include(queryable);//Əgər include funksiyası təyin olunubsa, bu əlaqəli obyektləri (navigation properties) sorğuya əlavə edir.
            //Misal: Məsələn, əgər hər bir istifadəçinin sifarişləri də var və siz istifadəçi ilə birlikdə həmin sifarişləri də gətirmək istəyirsinizsə, bu sətir bu işi görəcək.
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();//Əgər withDeleted parametri true-dursa, IgnoreQueryFilters() metodu ilə soft-deleted (silinmiş kimi işarələnmiş) obyektlər də sorğuya daxil edilir.
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);//Bu sətir sorğunun nəticəsini alır və predicate (şərt) uyğun gələn ilk obyekti tapır.
        }

        public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {//səhifələnmiş (Paginate<TEntity>) nəticə qaytarır.
            IQueryable<TEntity> queryable = Query();//verilənlər bazası sorğusunun yaradılması üçün başlanğıc nöqtəsini təyin edir
            if (!enableTracking)//Tracking deaktiv edilə bilər (AsNoTracking).
                queryable = queryable.AsNoTracking();
            if (include != null)//Navigasiya məlumatları daxil edilir (
                queryable = include(queryable);
            if (withDeleted)// Silinmiş obyektlər daxil edilə bilər(IgnoreQueryFilters).
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)//Filtrləmə şərti tətbiq olunur (predicate varsa).
                queryable = queryable.Where(predicate);
            if (orderBy != null)//Sıralama şərti tətbiq olunur (orderBy varsa).
                return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
            return await queryable.ToPaginateAsync(index, size, cancellationToken);//Əgər orderBy təyin olunmayıbsa, sadəcə sorğu nəticələri səhifələnmiş şəkildə qaytarılır.
           //Səhifələmə: ToPaginateAsync metodu, məlumatları təyin edilmiş səhifə nömrəsi(index) və səhifə ölçüsü(size) ilə səhifələnmiş şəkildə gətirir.
        }

        public async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);//sorgu yaradilir
            if (!enableTracking)//Tracking deaktiv edilə bilər (AsNoTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)//Navigasiya məlumatları daxil edilir (include varsa).
                queryable = include(queryable);
            if (withDeleted)//Silinmiş obyektlər daxil edilə bilər (IgnoreQueryFilters).
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null) //Filtrləmə şərti tətbiq olunur(predicate varsa).
                queryable = queryable.Where(predicate);//Əgər predicate şərti təyin olunubsa, bu, sorğunun nəticələrini həmin şərtə uyğun filtrləyir.
                                                       //Bu, nəticələri daha da dəqiq şəkildə seçmək üçün istifadə olunur.
            return await queryable.ToPaginateAsync(index, size, cancellationToken);//Sorğu nəticələri səhifələnmiş şəkildə qaytarılır. ToPaginateAsync metodu, məlumatları təyin edilmiş səhifə nömrəsi (index)
                                                                                   //və səhifə ölçüsü (size) ilə səhifələnmiş şəkildə gətirir.
        }
        public IQueryable<TEntity> Query() => Context.Set<TEntity>();

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)
        {
            if (!permanent) //(permanent=tam silmə) parametri false olaraq təyin olunubsa,varlıq soft delete ilə silinəcək
            {
                CheckHasEntityHaveOneToOneRelation(entity);//Soft delete üçün varlığın 1-ə-1 əlaqəsi olub-olmadığını yoxlayır.
                await setEntityAsSoftDeletedAsync(entity);// Varlığı soft delete etmək üçün setEntityAsSoftDeletedAsync metodunu çağırır.
            }
            else
            {
                Context.Remove(entity);//Əgər permanent (tam silmə) seçilibsə, varlıq verilənlər bazasından tamamilə silinir.
            }
        }
        protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)
        {
            bool hasEntityHaveOneToOneRelation =
                Context
                    .Entry(entity)
                    .Metadata.GetForeignKeys()//Varlığın xarici açar əlaqələrini alır.
                    .All(
                        x =>
                            x.DependentToPrincipal?.IsCollection == true
                            || x.PrincipalToDependent?.IsCollection == true
                            || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
                    ) == false;//Əgər xarici açarın 1-ə-1 əlaqəsi varsa, bu, false qaytaracaq.
            if (hasEntityHaveOneToOneRelation)
                throw new InvalidOperationException(
                    "Entity has one-to-one relationship. Soft Delete causes problems if you try to create entry again by same foreign key."
                );//Əgər belə əlaqə varsa, istisna atır ki, soft delete bu cür əlaqələrdə problem yarada bilər.
        }
        private async Task setEntityAsSoftDeletedAsync(IEntityTimestamps entity)//varlığı tam silmək əvəzinə onu deaktiv vəziyyətə gətirir.
        {
            if (entity.DeletedDate.HasValue)
                return;//Əgər varlıq artıq silinmişsə (DeletedDate doludursa), heç nə etmir.
            entity.DeletedDate = DateTime.UtcNow;//Əks halda, varlığın silinmə tarixi olaraq indiki vaxt təyin edilir.

            var navigations = Context
                .Entry(entity)
                .Metadata.GetNavigations()//onunla əlaqəli olan digər varlıqları tapır.
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();//Müəyyən əlaqə tipinə görə filtr tətbiq edir
                          // Hər bir naviqasiya üçün silinmə prosesini yoxlayır
            foreach (INavigation? navigation in navigations)
            {
                // Əgər naviqasiya owned entity isə onu keç
                if (navigation.TargetEntityType.IsOwned())
                    continue;

                // Əgər naviqasiya xüsusiyyətində məlumat yoxdursa onu da keç
                if (navigation.PropertyInfo == null)
                    continue;

                // Naviqasiya dəyərini əldə edir
                object? navValue = navigation.PropertyInfo.GetValue(entity);
                if (navigation.IsCollection)
                {
                    // Əgər naviqasiya kolleksiya isə və null-dursa sorğu vasitəsilə məlumatı yükləyir
                    if (navValue == null)
                    {
                        IQueryable query = Context.Entry(entity).Collection(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType()).ToListAsync();
                        if (navValue == null)
                            continue;
                    }

                    // Kolleksiyadakı hər bir elementi rekursiv olaraq soft delete edir
                    foreach (IEntityTimestamps navValueItem in (IEnumerable)navValue)
                        await setEntityAsSoftDeletedAsync(navValueItem);
                }
                else
                {
                    // Əgər naviqasiya obyekt tiplidirsə və null-dursa sorğu vasitəsilə məlumatı yükləyir
                    if (navValue == null)
                    {
                        IQueryable query = Context.Entry(entity).Reference(navigation.PropertyInfo.Name).Query();
                        navValue = await GetRelationLoaderQuery(query, navigationPropertyType: navigation.PropertyInfo.GetType())
                            .FirstOrDefaultAsync();
                        if (navValue == null)
                            continue;
                    }

                    // Naviqasiya obyektini rekursiv olaraq soft delete edir
                    await setEntityAsSoftDeletedAsync((IEntityTimestamps)navValue);
                }
            }

            // Entity-nin dəyişikliklərini bazaya tətbiq edir
            Context.Update(entity);
        }
        protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType) //əlaqəli məlumatları bazadan dinamik olaraq yükləmək.
        {
            // query-nin təminatçısının (provider) tipini əldə edir
            Type queryProviderType = query.Provider.GetType();

            // CreateQuery<TElement> metodunu əldə edir və onu dinamik olaraq təyin edir
            MethodInfo createQueryMethod =
                queryProviderType
                    .GetMethods()
                    .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                    ?.MakeGenericMethod(navigationPropertyType)
                ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");

            // Metodu icra edir və nəticəni IQueryable<object> tipində qaytarır
            var queryProviderQuery =
                (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;

            // Silinmiş (soft-deleted) entity-ləri filtrdən keçirərək qaytarır
            return queryProviderQuery.Where(x => !((IEntityTimestamps)x).DeletedDate.HasValue);
        }
        protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
        {
            foreach (TEntity entity in entities)
                await SetEntityAsDeletedAsync(entity, permanent);
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Paginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Paginate<TEntity> GetListByDynamic(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true)
        {
            throw new NotImplementedException();
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(TEntity entity, bool permanent = false)
        {
            throw new NotImplementedException();
        }

        public ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false)
        {
            throw new NotImplementedException();
        }
        #region Delete metodu ardicilligi
        //        Əməliyyatın Ardıcıllığı
        //Təsəvvür edin ki, siz bir obyektin silinmə əməliyyatını başlatmısınız.Aşağıda kodun debug ardıcıllığı addım-addım göstərilib:

        //1. SetEntityAsDeletedAsync metodu çağırılır.
        //Əməliyyat: Bu metod obyektin qalıcı olaraq (permanent) silinib-silinməyəcəyini yoxlayır.
        //Növbəti addım: Əgər permanent parametri false dəyərini alıbsa, bu, soft delete əməliyyatını icra edəcək və obyektin silinmiş kimi işarələnəcəyini bildirir.
        //Kodu yoxlayır: Əgər permanent parametri true olarsa, obyekt birbaşa bazadan silinir.
        //2. CheckHasEntityHaveOneToOneRelation metodu çağırılır.
        //Əməliyyat: Metod, obyektin bir-birə əlaqəsi olub-olmadığını yoxlayır.
        //Məqsəd: Əgər obyektin bir-birə əlaqəsi varsa və soft delete tətbiq edilsə, həmin əlaqənin yenidən yaradılması problem yarada bilər.
        //İstisna halları: Əgər obyekt bir-birə əlaqəyə malikdirsə, istisna (InvalidOperationException) atılır.
        //3. setEntityAsSoftDeletedAsync metodu çağırılır.
        //Əməliyyat: Obyektin soft-deleted vəziyyətinə gətirilməsi prosesi burada baş verir.
        //Əməliyyat ardıcıllığı:
        //Tarix yoxlanışı: Əgər obyekt artıq soft-deleted vəziyyətindədirsə (DeletedDate təyin olunubsa), əməliyyat buraxılır.
        //DeletedDate tarixi: Əgər obyekt silinməyibsə, DeletedDate indiki tarixə (DateTime.UtcNow) təyin edilir.
        //Əlaqəli obyektlər: Metod əlaqəli obyektləri yükləyir və onları da soft-deleted vəziyyətinə gətirir.
        //4. Əlaqəli obyektlərin yüklənməsi və işlənməsi.
        //Əlaqəli obyektlər tapılır: GetRelationLoaderQuery metodu ilə əlaqəli obyektlər bazadan yüklənir.
        //Silinmə: Əlaqəli obyektlərə də soft delete tətbiq olunur, yəni onların da DeletedDate sahəsi təyin edilir.
        //İstisna hallar: Əgər əlaqəli obyektlərdən biri DeletedDate-ə malikdirsə və ya bazadan alınmırsa, həmin obyektlər keçilir.
        //5. Əlaqəli obyektlərin vəziyyəti yenilənir.
        //Əməliyyat: Əlaqəli obyektlər artıq silinmiş kimi işarələndikdən sonra Context.Update(entity) metodu ilə dəyişikliklər yaddaşa yazılır.
        //Əsas Əməliyyatın Ardıcıllığı və Nəticələr
        //Əgər obyekt birbaşa olaraq qalıcı şəkildə (permanent) silinirsə, Context.Remove(entity) ilə birbaşa bazadan silinir.
        //Əgər soft delete tətbiq olunursa, obyekt və onun əlaqəli obyektləri silinmiş kimi işarələnir.
        //Soft delete tətbiq edilərkən, əlaqəli obyektlər də daxil olmaqla bütün obyektlərin DeletedDate sahəsi təyin edilir ki, onlar da silinmiş hesab olunsunlar.
        //Əsas Xülasə
        //Bu silmə əməliyyatının debug ardıcıllığını addım-addım belə təsvir etmək olar:

        //Permanent yoxlanışı → Əgər permanent deyilsə, soft delete prosesi başlayır.
        //Bir-birə əlaqələrin yoxlanışı → Əgər obyekt bir-birə əlaqəyə malikdirsə və soft delete tətbiq edilirsə, istisna atılır.
        //Soft delete prosesi → Əlaqəli obyektlər də daxil olmaqla obyekt silinmiş kimi işarələnir.
        //Əlaqəli obyektlərin yüklənməsi və işlənməsi → Yüklənmiş obyektlər də silinmiş kimi işarələnir.

        #endregion
    }
}
