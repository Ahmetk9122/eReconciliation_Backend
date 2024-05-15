
using eReconciliation.Core.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        void UserCompanyMapingAdd(int userId, int companyId);
        UserCompany GetCompany(int userId);
    }
}