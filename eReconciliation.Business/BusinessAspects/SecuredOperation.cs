using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using eReconciliation.Core.Extensions;
using eReconciliation.Core.Utilities.Interceptors;
using eReconciliation.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliation.Business.BusinessAspects
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation ınvocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception("İşlem yapmaya yetkiniz yoktur.");
        }
    }
}