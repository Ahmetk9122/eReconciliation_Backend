using eReconciliation.Core.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        void Add(User user);
        void Update(User user);
        User GetByMail(string email);
        User GetById(int id);
        User GetByMailConfirmValue(string confirmValue);
    }
}