using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities;

namespace eReconciliation.Business
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegister userForRegister, string password);
        IDataResult<User> Login(UserForLogin userForLogin);
        IResult UserExist(string email);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);

    }
}