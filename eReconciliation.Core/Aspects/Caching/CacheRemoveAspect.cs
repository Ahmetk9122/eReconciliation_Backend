using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using eReconciliation.Core.CrossCuttingConcerns.Caching;
using eReconciliation.Core.Utilities.Interceptors;
using eReconciliation.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliation.Core.Aspects.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        protected override void OnSuccess(IInvocation ınvocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}