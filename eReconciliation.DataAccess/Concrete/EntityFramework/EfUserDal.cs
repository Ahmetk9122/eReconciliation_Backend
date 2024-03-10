using eReconciliation.Core.DataAccess.EntityFramework;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.DataAccess.Concrete;

namespace eReconciliation.DataAccess
{
    public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            using (var context = new Context())
            {
                var result = from operationClaim in context.OperationClaims  //burada tüm yetkilerin listesini çekiyorum
                             join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                             //bu kod ile kullanıcı yetkileri table'da bulunan yetkiler ve bir üstteki yetkiler arasında eşleştirme yapıyorum ki, kullanıcı yetkileri table'da yazdığım Id bilgisinin karşısında name bilgisi çekebileyim.
                             //UserOperatinClaim'de User Id - OperationClaimId - CompanyId var. Benim kullanıcıya atadığım OperationClaimId'nin name karşılığını almam gerekiyor o yüzden join yapıyorum.
                             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id //burada kullanıcı ve şirket bilgisine göre kısıt veriyorum
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                //burada da yeni bir OperationClaim nesnesi türetip içine sadece UserOperationClaim kısmında yetki evrdiğim kullanıcı ve yetkileri listesini çekiyorum.
                return result.ToList();
            }

        }
    }
}