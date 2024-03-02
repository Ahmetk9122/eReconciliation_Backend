using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.DataAccess.Concrete;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, Context>, ICompanyDal
    {

    }
}