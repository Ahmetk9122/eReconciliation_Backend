using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.DataAccess.Concrete;

namespace eReconciliation.DataAccess
{
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {

    }
}