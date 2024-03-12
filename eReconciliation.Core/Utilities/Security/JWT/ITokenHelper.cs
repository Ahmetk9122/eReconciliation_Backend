using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Core.Utilities
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, int CompanyId);
    }
}