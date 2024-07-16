using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface ICurrencyAccountService
    {
        IDataResult<List<CurrencyAccount>> GetCurrencyAccounts(int companyId);
        IDataResult<CurrencyAccount> GetCurrencyAccountById(int id);
        IResult AddCurrencyAccount(CurrencyAccount currencyAccount);
        IResult UpdateCurrencyAccount(CurrencyAccount currencyAccount);
        IResult DeleteCurrencyAccount(CurrencyAccount currencyAccount);
    }
}