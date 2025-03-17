using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            // Yəni, AddMemoryCache() ilə əvvəldən yaddaşda məlumatları saxlamaq üçün bir sahə yaradırsınız,
            // sonra isə GetService<IMemoryCache>() ilə bu sahədən məlumatları götürür və istifadə edirsiniz
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddMemoryCache(); // 1 dənə instance sini yaradır hərkəsə onu verir
            serviceCollection.AddSingleton<Stopwatch>(); 
        }
    }
}
