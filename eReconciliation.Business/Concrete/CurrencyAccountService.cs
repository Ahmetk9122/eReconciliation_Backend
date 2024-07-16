using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public class CurrencyAccountService : ICurrencyAccountService
    {
        private readonly ICurrencyAccountDal _currencyAccountDal;

        public CurrencyAccountService(ICurrencyAccountDal currencyAccountDal)
        {
            _currencyAccountDal = currencyAccountDal;
        }

        public IResult AddCurrencyAccount(CurrencyAccount currencyAccount)
        {
            _currencyAccountDal.Add(currencyAccount);
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        public IResult DeleteCurrencyAccount(CurrencyAccount currencyAccount)
        {
            GetCurrencyAccountById(currencyAccount.Id);
            _currencyAccountDal.Delete(currencyAccount);
            return new SuccessResult(Messages.DeletedCurrencyAccount);

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

        public IResult UpdateCurrencyAccount(CurrencyAccount currencyAccount)
        {
            GetCurrencyAccountById(currencyAccount.Id);
            _currencyAccountDal.Update(currencyAccount);
            return new SuccessResult(Messages.UpdatedCurrencyAccount);

        }
    }
}