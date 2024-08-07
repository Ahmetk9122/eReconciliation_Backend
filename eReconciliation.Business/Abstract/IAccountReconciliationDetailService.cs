using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IAccountReconciliationDetailService
    {
        IResult AddAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail);
        IResult AddAccountReconciliationDetailToExcel(string filePath, int accountReconciliationId);
        IResult UpdateAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail);
        IResult DeleteAccountReconciliationDetail(AccountReconciliationDetail accountReconciliationDetail);
        IDataResult<AccountReconciliationDetail> AccountReconciliationDetailGetById(int accountReconciliationDetailId);
        IDataResult<List<AccountReconciliationDetail>> AccountReconciliationDetailGetList(int accountReconciliationId);
    }
}