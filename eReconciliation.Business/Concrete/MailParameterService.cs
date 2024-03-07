using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.DataAccess;

namespace eReconciliation.Business
{
    public class MailParameterService : IMailParameterService
    {
        private readonly IMailParameterDal _mailParameterDal;

        public MailParameterService(IMailParameterDal mailParameterDal)
        {
            _mailParameterDal = mailParameterDal;
        }
    }
}