using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        void Add(User user);
        User GetByMail(string email);
    }
}