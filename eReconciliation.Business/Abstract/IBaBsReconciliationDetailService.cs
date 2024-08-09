using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IBaBsReconciliationDetailService
    {
        IResult AddBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail);
        IResult AddBaBsReconciliationDetailToExcel(string filePath, int baBsReconciliationId);
        IResult UpdateBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail);
        IResult DeleteBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail);
        IDataResult<BaBsReconciliationDetail> BaBsReconciliationDetailGetById(int baBsReconciliationDetailId);
        IDataResult<List<BaBsReconciliationDetail>> BaBsReconciliationDetailGetList(int baBsReconciliationId);
    }
}