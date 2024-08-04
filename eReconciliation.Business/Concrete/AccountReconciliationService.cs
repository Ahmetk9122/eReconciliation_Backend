using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;
using ExcelDataReader;

namespace eReconciliation.Business
{
    public class AccountReconciliationService : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal _accountReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;

        public AccountReconciliationService(IAccountReconciliationDal accountReconciliationDal, ICurrencyAccountService currencyAccountService)
        {
            _accountReconciliationDal = accountReconciliationDal;
            _currencyAccountService = currencyAccountService;
        }
        public IDataResult<AccountReconciliation> GetById(int accountReconciliationId)
        {
            return new SuccessDataResult<AccountReconciliation>(_accountReconciliationDal.Get(x => x.Id == accountReconciliationId));
        }

        public IDataResult<List<AccountReconciliation>> GetList(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>(_accountReconciliationDal.GetList(x => x.CompanyId == companyId));
        }
        public IResult Add(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Add(accountReconciliation);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }
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
                        string startingDate = reader.GetString(1);
                        string endingDate = reader.GetString(2);
                        string currencyId = reader.GetString(3);
                        string currencyDebit = reader.GetString(4);
                        string currencyCredit = reader.GetString(5);

                        if (code != "Cari Kodu")
                        {

                            int currencyAccountId = _currencyAccountService.GetCurrencyAccountByCode(code, companyId).Data.Id;
                            AccountReconciliation accountReconciliation = new AccountReconciliation()
                            {
                                CurrencyAccountId = currencyAccountId,
                                StartingDate = Convert.ToDateTime(startingDate),
                                EndingDate = Convert.ToDateTime(endingDate),
                                CurrencyId = Convert.ToInt16(currencyId),
                                CurrencyDebit = Convert.ToDecimal(currencyDebit),
                                CurrencyCredit = Convert.ToDecimal(currencyCredit),
                                CompanyId = companyId,
                            };
                            _accountReconciliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }
        public IResult Delete(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Delete(accountReconciliation);
            return new SuccessResult(Messages.DeleteAccountReconciliation);
        }



        public IResult Update(AccountReconciliation accountReconciliation)
        {
            _accountReconciliationDal.Update(accountReconciliation);
            return new SuccessResult(Messages.UpdateAccountReconciliation);
        }


    }
}