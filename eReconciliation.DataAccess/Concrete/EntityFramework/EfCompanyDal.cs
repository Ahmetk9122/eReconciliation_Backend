using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.DataAccess.Concrete;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.DataAccess
{
    public class EfCompanyDal : EfEntityRepositoryBase<Company, Context>, ICompanyDal
    {
        public UserCompany GetCompany(int userId)
        {
            using (var context = new Context())
            {
                var result = context.UserCompanies.Where(x => x.UserId == userId).FirstOrDefault();
                return result;
            }

        }

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