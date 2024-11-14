using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Paging
{
    public static class IQueryablePaginateExtensions
    {
        //Bu metod IQueryable<T> tipindəki source adlı bir mənbədən səhifələnmiş məlumatlar yaratmaq üçün istifadə olunur.
        public static async Task<Paginate<T>> ToPaginateAsync<T>(
       this IQueryable<T> source,//Bu, metodun çağırıldığı IQueryable obyekti.
       int index, //İstədiyiniz səhifənin indeksi (səhifələrin sırası sıfırdan başlayır).
       int size, //Hər səhifədəki elementlərin sayı
       CancellationToken cancellationToken = default //cancel etmek ucun
       )
        {
            int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);//cancel olan elementlerin sayi

            List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);//sehife deyisdikde index.size qeder data atlasin

            Paginate<T> list = new()
            {
                Index = index,//səhifənin indeksi.
                Count = count,//Ümumi element sayı.
                Items = items,//Hal-hazırda göstərilən elementlər
                Size = size,//Hər səhifədəki element sayı
                Pages = (int)Math.Ceiling(count / (double)size)//Ümumi səhifə sayı (yuxarıya yuvarlayır
            };
            return list;

        }

        public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size)
        {
            int count = source.Count();
            var items = source.Skip(index * size).Take(size).ToList();

            Paginate<T> list =
                new()
                {
                    Index = index,
                    Size = size,
                    Count = count,
                    Items = items,
                    Pages = (int)Math.Ceiling(count / (double)size)
                };
            return list;
        }
    }
}
