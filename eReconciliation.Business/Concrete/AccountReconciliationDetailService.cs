using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;
using ExcelDataReader;

namespace eReconciliation.Business
{
    public class AccountReconciliationDetailService : IAccountReconciliationDetailService
    {
        private readonly IAccountReconciliationDetailDal _accountReconciliationDetailDal;

        public AccountReconciliationDetailService(IAccountReconciliationDetailDal accountReconciliationDetailDal)
        {
            _accountReconciliationDetailDal = accountReconciliationDetailDal;
        }

        [CacheAspect(60)]
        public IDataResult<AccountReconciliationDetail> AccountReconciliationDetailGetById(int accountReconciliationDetailId)
        {
            return new SuccessDataResult<AccountReconciliationDetail>(_accountReconciliationDetailDal.Get(x => x.Id == accountReconciliationDetailId));
        }

        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliationDetail>> AccountReconciliationDetailGetList(int accountReconciliationId)
        {
            return new SuccessDataResult<List<AccountReconciliationDetail>>(_accountReconciliationDetailDal.GetList(x => x.AccountReconciliationId == accountReconciliationId));
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult AddAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailDal.Add(accountReconciliationDetail);
            return new SuccessResult(Messages.AddedAccountReconciliationDetail);
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        [TransactionScopeAspect]
        public IResult AddAccountReconciliationDetailToExcel(string filePath, int accountReconciliationId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);


                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double currencyId = reader.GetDouble(2);
                            double currencyDebit = reader.GetDouble(3);
                            double currencyCredit = reader.GetDouble(4);

                            AccountReconciliationDetail accountReconciliationDetail = new AccountReconciliationDetail()
                            {
                                AccountReconciliationId = accountReconciliationId,
                                Date = date,
                                Description = description,
                                CurrencyId = Convert.ToInt16(currencyId),
                                CurrencyDebit = Convert.ToDecimal(currencyDebit),
                                CurrencyCredit = Convert.ToDecimal(currencyCredit),
                            };
                            _accountReconciliationDetailDal.Add(accountReconciliationDetail);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedAccountReconciliationDetail);
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult DeleteAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailDal.Delete(accountReconciliationDetail);
            return new SuccessResult(Messages.DeleteAccountReconciliationDetail);
        }

        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult UpdateAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailDal.Delete(accountReconciliationDetail);
            return new SuccessResult(Messages.DeleteAccountReconciliationDetail);
        }
    }
}