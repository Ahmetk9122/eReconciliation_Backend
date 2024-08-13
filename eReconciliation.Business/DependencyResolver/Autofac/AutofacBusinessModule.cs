using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.Concrete;
using eReconciliation.Core.Utilities;
using eReconciliation.Core.Utilities.Interceptors;
using eReconciliation.DataAccess;
using eReconciliation.DataAccess.Abstract;
using eReconciliation.DataAccess.Concrete.EntityFramework;

namespace eReconciliation.Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyService>().As<ICompanyService>();
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            builder.RegisterType<AccountReconciliationDetailService>().As<IAccountReconciliationDetailService>();
            builder.RegisterType<EfAccountReconciliationDetailDal>().As<IAccountReconciliationDetailDal>();

            builder.RegisterType<AccountReconciliationService>().As<IAccountReconciliationService>();
            builder.RegisterType<EfAccountReconciliationDal>().As<IAccountReconciliationDal>();

            builder.RegisterType<BaBsReconciliationDetailService>().As<IBaBsReconciliationDetailService>();
            builder.RegisterType<EfBaBsReconciliationDetailDal>().As<IBaBsReconciliationDetailDal>();

            builder.RegisterType<BaBsReconciliationService>().As<IBaBsReconciliationService>();
            builder.RegisterType<EfBaBsReconciliationDal>().As<IBaBsReconciliationDal>();

            builder.RegisterType<CurrencyAccountService>().As<ICurrencyAccountService>();
            builder.RegisterType<EfCurrencyAccountDal>().As<ICurrencyAccountDal>();

            builder.RegisterType<CurrencyService>().As<ICurrencyService>();
            builder.RegisterType<EfCurrencyDal>().As<ICurrencyDal>();

            builder.RegisterType<MailParameterService>().As<IMailParameterService>();
            builder.RegisterType<EfMailParameterDal>().As<IMailParameterDal>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<MailService>().As<IMailService>();
            builder.RegisterType<EfMailDal>().As<IMailDal>();

            builder.RegisterType<MailTemplateService>().As<IMailTemplateService>();
            builder.RegisterType<EFMailTemplateDal>().As<IMailTemplateDal>();

            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserOperationClaimService>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaim>().As<IUserOperationClaimDal>();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        }
    }
}