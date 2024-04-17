using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business.Abstract
{
    public interface IMailTemplateService
    {
        IResult AddMailTemplate(MailTemplate mailTemplate);
        IResult UpdateMailTemplate(MailTemplate mailTemplate);
        IResult DeleteMailTemplate(MailTemplate mailTemplate);
        IDataResult<MailTemplate> GetMailTemplateById(int id);
        IDataResult<MailTemplate> GetMailTemplateName(string name, int companyId);

        IDataResult<List<MailTemplate>> GetMailTemplates(int companyId);
    }
}