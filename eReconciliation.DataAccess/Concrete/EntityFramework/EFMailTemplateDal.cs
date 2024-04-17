using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.DataAccess.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess.Concrete.EntityFramework
{
    public class EFMailTemplateDal : EfEntityRepositoryBase<MailTemplate, Context>, IMailTemplateDal
    {

    }
}