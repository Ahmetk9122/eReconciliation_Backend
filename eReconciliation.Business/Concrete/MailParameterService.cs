using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Aspects.Performance;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public class MailParameterService : IMailParameterService
    {
        private readonly IMailParameterDal _mailParameterDal;

        public MailParameterService(IMailParameterDal mailParameterDal)
        {
            _mailParameterDal = mailParameterDal;
        }


        [CacheAspect(60)]
        public IDataResult<MailParameter> GetMailParameter(int companyId)
        {
            return new SuccessDataResult<MailParameter>(_mailParameterDal.Get(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("MailParameter.Update,Admin")]
        [CacheRemoveAspect("IMailParameterService.Get")]
        public IResult UpdateMailParameter(MailParameter mailParameter)
        {
            var result = GetMailParameter(mailParameter.CompanyId);
            if (result.Data == null)
            {
                _mailParameterDal.Add(mailParameter);
            }
            else
            {
                result.Data.SMTP = mailParameter.SMTP;
                result.Data.Port = mailParameter.Port;
                result.Data.SSL = mailParameter.SSL;
                result.Data.Email = mailParameter.Email;
                result.Data.Password = mailParameter.Password;
                _mailParameterDal.Update(result.Data);
            }
            return new SuccessResult(Messages.MailParameterUpdated);
        }
    }
}