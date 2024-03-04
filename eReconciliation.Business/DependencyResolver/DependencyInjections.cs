using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace eReconciliation.Business.DependencyResolver
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjects(this IServiceCollection services)
        {
            services.TryAddScoped<ICompanyService, CompanyService>();
            services.TryAddScoped<ICompanyDal, EfCompanyDal>();

        }
    }
}