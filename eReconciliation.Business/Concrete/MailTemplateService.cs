using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business.Concrete
{
    public class MailTemplateService : IMailTemplateService
    {
        private readonly IMailTemplateDal _mailTemplateDal;

        public MailTemplateService(IMailTemplateDal mailTemplateDal)
        {
            _mailTemplateDal = mailTemplateDal;

        }

        [CacheAspect(60)]
        public IDataResult<MailTemplate> GetMailTemplateById(int id)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateDal.Get(x => x.Id == id));
        }

        [CacheAspect(60)]
        public IDataResult<List<MailTemplate>> GetMailTemplates(int companyId)
        {
            return new SuccessDataResult<List<MailTemplate>>(_mailTemplateDal.GetList(x => x.CompanyId == companyId));
        }
        [CacheAspect(60)]
        public IDataResult<MailTemplate> GetMailTemplateName(string name, int companyId)
        {
            return new SuccessDataResult<MailTemplate>(_mailTemplateDal.Get(x => x.CompanyId == companyId && x.Type == name));

        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult AddMailTemplate(MailTemplate mailTemplate)
        {
            _mailTemplateDal.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult UpdateMailTemplate(MailTemplate mailTemplate)
        {
            _mailTemplateDal.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }

        [CacheRemoveAspect("IMailTemplateService.Get")]
        public IResult DeleteMailTemplate(MailTemplate mailTemplate)
        {
            _mailTemplateDal.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }

    }
}