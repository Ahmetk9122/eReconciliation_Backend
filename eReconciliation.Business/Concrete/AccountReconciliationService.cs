using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Aspects.Performance;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.DataAccess.Concrete;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace eReconciliation.Business
{
    public class AccountReconciliationService : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal _accountReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;

        public AccountReconciliationService(IAccountReconciliationDal accountReconciliationDal, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
            _accountReconciliationDal = accountReconciliationDal;
            _currencyAccountService = currencyAccountService;
        }
        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<AccountReconciliation> GetById(int accountReconciliationId)
        {
            return new SuccessDataResult<AccountReconciliation>(_accountReconciliationDal.Get(x => x.Id == accountReconciliationId));
        }

        [PerformanceAspect(3)]
        public IDataResult<AccountReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<AccountReconciliation>(_accountReconciliationDal.Get(x => x.Guid == code));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliation>> GetList(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>(_accountReconciliationDal.GetList(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.SendMail,Admin")]
        [TransactionScopeAspect]
        public async Task<IResult> SendReconciliationMail(int accountReconciliationId)
        {
            var existAccountReconciliation = new AccountReconciliation();
            using (var context = new Context())
            {
                existAccountReconciliation = context.AccountReconciliations.Where(x => x.Id == accountReconciliationId)
                .Include(x => x.Company)
                .Include(x => x.CurrencyAccount)
                .Include(x => x.Currency)
                .SingleOrDefault();
            }

            string subject = "Mutabakat Maili";
            string body = "Şirket Adımız: " + existAccountReconciliation.Company.Name + " <br /> " +
               "Şirket Vergi Dairesi: " + existAccountReconciliation.Company.TaxDepartment + " <br />" +
               "Şirket Vergi Numarası: " + existAccountReconciliation.Company.TaxIdNumber + " - " + existAccountReconciliation.Company.IdentityNumber + " <br /><hr>" +
               "Sizin Şirket: " + existAccountReconciliation.CurrencyAccount.Name + "<br />" +
               "Sizin Şirket Vergi Dairesi: " + existAccountReconciliation.CurrencyAccount.TaxDepartment + " <br />" +
               "Sizin Şirket Vergi Numarası: " + existAccountReconciliation.CurrencyAccount.TaxIdNumber + " - " + existAccountReconciliation.CurrencyAccount.IdentityNumber + " <br /><hr>" +
               "Borç: " + existAccountReconciliation.CurrencyDebit + " " + existAccountReconciliation.Currency.Code + " <br />" +
               "Alacak: " + existAccountReconciliation.CurrencyCredit + " " + existAccountReconciliation.Currency.Code + "<br />";
            string link = "https://localhost:7129/api/account-reconciliations/codes/" + existAccountReconciliation.Guid;
            string linkDescription = "Mutabakatı Cevaplamak için Tıklayın";

            var mailTemplate = _mailTemplateService.GetMailTemplateName("Kayıt", 1);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

            var mailParameter = _mailParameterService.GetMailParameter(1);

            Entities.Dtos.SendMailDto sendMail = new Entities.Dtos.SendMailDto()
            {
                MailParameter = mailParameter.Data,
                Email = existAccountReconciliation.CurrencyAccount.Email,
                Subject = subject,
                Body = templateBody
            };
            _mailService.SendMail(sendMail);

            return new SuccessResult(Messages.MailSendSuccessful);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliation accountReconciliation)
        {
            accountReconciliation.Guid = Guid.NewGuid().ToString();
            _accountReconciliationDal.Add(accountReconciliation);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddAccountReconciliationToExcel(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);


                        if (code != "Cari Kodu" && code != null)
                        {
                            DateTime startingDate = reader.GetDateTime(1);
                            DateTime endingDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double currencyDebit = reader.GetDouble(4);
                            double currencyCredit = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetCurrencyAccountByCode(code, companyId).Data.Id;

                            AccountReconciliation accountReconciliation = new AccountReconciliation()
                            {
                                CurrencyAccountId = currencyAccountId,
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                CurrencyId = Convert.ToInt16(currencyId),
                                CurrencyDebit = Convert.ToDecimal(currencyDebit),
                                CurrencyCredit = Convert.ToDecimal(currencyCredit),
                                Id = companyId,
                                Guid = Guid.NewGuid().ToString()
                            };
                            _accountReconciliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Delete(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Delete(accountReconciliation);
            return new SuccessResult(Messages.DeleteAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Update,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Update(accountReconciliation);
            return new SuccessResult(Messages.UpdateAccountReconciliation);
        }

    }
}