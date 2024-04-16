using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess.Abstract;
using eReconciliation.Entities.Dtos;

namespace eReconciliation.Business.Concrete
{
    public class MailService : IMailService
    {
        private readonly IMailDal _mailDal;

        public MailService(IMailDal mailDal)
        {
            _mailDal = mailDal;
        }

        public IResult SendMail(SendMailDto sendMailDto)
        {
            _mailDal.SendMail(sendMailDto);
            return new SuccessResult(Messages.MailSendSuccessful);
        }
    }
}