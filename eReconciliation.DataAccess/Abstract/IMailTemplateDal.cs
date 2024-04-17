using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess.Abstract
{
    public interface IMailTemplateDal : IEntityRepository<MailTemplate>
    {
    }
}