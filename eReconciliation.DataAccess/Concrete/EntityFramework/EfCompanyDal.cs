using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.DataAccess.Concrete;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, Context>, ICompanyDal
    {
        public void UserCompanyMapingAdd(int userId, int companyId)
        {
            using (var context = new Context())
            {
                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = companyId,
                    AddedAt = DateTime.Now,
                    IsActive = true,
                };
                context.UserCompanies.Add(userCompany);
                context.SaveChanges();
            }
        }
    }
}