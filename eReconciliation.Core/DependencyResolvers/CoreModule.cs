using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.CrossCuttingConcerns.Caching;
using eReconciliation.Core.CrossCuttingConcerns.Caching.Microsoft;
using eReconciliation.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliation.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}