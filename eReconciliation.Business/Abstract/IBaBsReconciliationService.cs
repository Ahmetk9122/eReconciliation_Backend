using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IBaBsReconciliationService
    {
        IResult AddBaBsReconciliation(BaBsReconciliation baBsReconciliation);
        IResult AddBaBsReconciliationToExcel(string filePath, int companyId);
        IResult UpdateBaBsReconciliation(BaBsReconciliation baBsReconciliation);
        IResult DeleteBaBsReconciliation(BaBsReconciliation baBsReconciliation);
        IDataResult<BaBsReconciliation> BaBsReconciliationGetById(int baBsReconciliationId);
        IDataResult<List<BaBsReconciliation>> BaBsReconciliationGetList(int companyId);
        IDataResult<BaBsReconciliation> GetByCode(string baBsReconciliationCode);
        Task<IResult> SendBaBsReconciliationMail(int baBsReconciliation);
    }
}