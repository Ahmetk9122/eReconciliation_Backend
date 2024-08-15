using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Aspects.Performance;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace eReconciliation.Business
{
    public class BaBsReconciliationService : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal _baBsReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;



        public BaBsReconciliationService(IBaBsReconciliationDal baBsReconciliationDal, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
            _baBsReconciliationDal = baBsReconciliationDal;
            _currencyAccountService = currencyAccountService;
        }
        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<BaBsReconciliation> BaBsReconciliationGetById(int baBsReconciliationId)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(x => x.Id == baBsReconciliationId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<BaBsReconciliation>> BaBsReconciliationGetList(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>(_baBsReconciliationDal.GetList(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddBaBsReconciliationToExcel(string filePath, int companyId)
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
                            string type = reader.GetString(1);
                            double mounth = reader.GetDouble(2);
                            double year = reader.GetDouble(3);
                            double quantity = reader.GetDouble(4);
                            double total = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetCurrencyAccountByCode(code, companyId).Data.Id;

                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = Convert.ToInt16(mounth),
                                Year = Convert.ToInt16(year),
                                Quantity = Convert.ToInt16(quantity),
                                Total = Convert.ToInt16(total),
                                Guid = Guid.NewGuid().ToString()
                            };
                            _baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedAccountReconciliation);

        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Add,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult AddBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            baBsReconciliation.Guid = Guid.NewGuid().ToString();
            _baBsReconciliationDal.Add(baBsReconciliation);
            return new SuccessResult(Messages.AddedbaBsReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult DeleteBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Delete(baBsReconciliation);
            return new SuccessResult(Messages.DeletebaBsReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.Update,Admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult UpdateBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Update(baBsReconciliation);
            return new SuccessResult(Messages.UpdatebaBsReconciliation);
        }
        [PerformanceAspect(3)]
        public IDataResult<BaBsReconciliation> GetByCode(string baBsReconciliationCode)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(x => x.Guid == baBsReconciliationCode));
        }
        [PerformanceAspect(3)]
        [SecuredOperation("BaBsReconciliation.SendMail,Admin")]
        public async Task<IResult> SendBaBsReconciliationMail(int baBsReconciliationId)
        {
            var existBaBsReconciliation = (await _baBsReconciliationDal.GetQuery(x => x.Id == baBsReconciliationId))
               .Include(x => x.Company)
               .Include(x => x.CurrencyAccount)
               .SingleOrDefault() ?? throw new Exception("Babs bilgisine ulaşılamadı");

            string subject = "Mutabakat Maili";
            string body = "Şirket Adımız: " + existBaBsReconciliation.Company.Name + " <br /> " +
               "Şirket Vergi Dairesi: " + existBaBsReconciliation.Company.TaxDepartment + " <br />" +
               "Şirket Vergi Numarası: " + existBaBsReconciliation.Company.TaxIdNumber + " - " + existBaBsReconciliation.Company.IdentityNumber + " <br /><hr>" +
               "Sizin Şirket: " + existBaBsReconciliation.CurrencyAccount.Name + "<br />" +
               "Sizin Şirket Vergi Dairesi: " + existBaBsReconciliation.CurrencyAccount.TaxDepartment + " <br />" +
               "Sizin Şirket Vergi Numarası: " + existBaBsReconciliation.CurrencyAccount.TaxIdNumber + " - " + existBaBsReconciliation.CurrencyAccount.IdentityNumber + " <br /><hr>" +
               "Ay / Yıl: " + existBaBsReconciliation.Mounth + " / " + existBaBsReconciliation.Year + "<br />" +
               "Adet: " + existBaBsReconciliation.Quantity + "<br />" +
               "Tutar: " + existBaBsReconciliation.Total + " TL <br />";
            string link = "https://localhost:7129/api/baBs-reconciliations/codes/" + existBaBsReconciliation.Guid;
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
                Email = existBaBsReconciliation.CurrencyAccount.Email,
                Subject = subject,
                Body = templateBody
            };
            _mailService.SendMail(sendMail);

            return new SuccessResult(Messages.MailSendSuccessful);
        }
    }
}