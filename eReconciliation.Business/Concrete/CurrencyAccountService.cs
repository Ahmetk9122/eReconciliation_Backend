using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Business.ValidationRules.FluentValidation;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Aspects.Autofac.Validation;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;
using ExcelDataReader;

namespace eReconciliation.Business
{
    public class CurrencyAccountService : ICurrencyAccountService
    {
        private readonly ICurrencyAccountDal _currencyAccountDal;

        public CurrencyAccountService(ICurrencyAccountDal currencyAccountDal)
        {
            _currencyAccountDal = currencyAccountDal;
        }

        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult AddCurrencyAccount(CurrencyAccount currencyAccount)
        {
            _currencyAccountDal.Add(currencyAccount);
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        [ValidationAspect(typeof(CurrencyAccountValidator))]
        [TransactionScopeAspect]
        public IResult AddToExcelCurrencyAccount(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        string name = reader.GetString(1);
                        string address = reader.GetString(2);
                        string taxDepartment = reader.GetString(3);
                        string taxIdNumber = reader.GetString(4);
                        string identityNumber = reader.GetString(5);
                        string email = reader.GetString(6);
                        string authorized = reader.GetString(7);

                        if (code != "Cari Kodu")
                        {
                            CurrencyAccount currencyAccount = new CurrencyAccount()
                            {
                                Name = name,
                                Address = address,
                                TaxDepartment = taxDepartment,
                                TaxIdNumber = taxIdNumber,
                                IdentityNumber = identityNumber,
                                Email = email,
                                Authorized = authorized,
                                AddedAt = DateTime.Now,
                                Code = code,
                                CompanyId = companyId,
                                IsActive = true
                            };
                            _currencyAccountDal.Add(currencyAccount);
                        }
                    }
                }
            }
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        public IResult DeleteCurrencyAccount(CurrencyAccount currencyAccount)
        {
            GetCurrencyAccountById(currencyAccount.Id);
            _currencyAccountDal.Delete(currencyAccount);
            return new SuccessResult(Messages.DeletedCurrencyAccount);

        }

        public IDataResult<CurrencyAccount> GetCurrencyAccountByCode(string code, int companyId)
        {
            return new SuccessDataResult<CurrencyAccount>(_currencyAccountDal.Get(x => x.CompanyId == companyId && x.Code == code));
        }

        public IDataResult<CurrencyAccount> GetCurrencyAccountById(int id)
        {
            var result = _currencyAccountDal.Get(x => x.Id == id);
            if (result == null)
                throw new Exception("Cari bilgisine ulaşılamadı!");
            return new SuccessDataResult<CurrencyAccount>(result);
        }

        public IDataResult<List<CurrencyAccount>> GetCurrencyAccounts(int companyId)
        {
            return new SuccessDataResult<List<CurrencyAccount>>(_currencyAccountDal.GetList(x => x.CompanyId == companyId));
        }

        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult UpdateCurrencyAccount(CurrencyAccount currencyAccount)
        {
            GetCurrencyAccountById(currencyAccount.Id);
            _currencyAccountDal.Update(currencyAccount);
            return new SuccessResult(Messages.UpdatedCurrencyAccount);

        }
    }
}