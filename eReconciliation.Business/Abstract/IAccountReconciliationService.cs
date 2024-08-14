using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IAccountReconciliationService
    {
        IResult Add(AccountReconciliation accountReconciliation);
        IResult AddAccountReconciliationToExcel(string filePath, int companyId);
        IResult Update(AccountReconciliation accountReconciliation);
        IResult Delete(AccountReconciliation accountReconciliation);
        IDataResult<AccountReconciliation> GetById(int accountReconciliationId);
        IDataResult<AccountReconciliation> GetByCode(string code);
        Task<IResult> SendReconciliationMail(int accountReconciliationId);
        IDataResult<List<AccountReconciliation>> GetList(int companyId);
    }
}