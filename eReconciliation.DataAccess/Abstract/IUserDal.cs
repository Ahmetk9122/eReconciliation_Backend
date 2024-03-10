using eReconciliation.Core.DataAccess;
using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.DataAccess
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user, int companyId);
    }
}