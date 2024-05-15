using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;

namespace eReconciliation.Business
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company);
        IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password, int companyId);
        IDataResult<User> Login(UserForLogin userForLogin);
        IResult UserExist(string email);
        IResult UpdateUser(User user);
        IResult SendConfirmEmail(User user);
        IResult CompanyExist(Company company);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
        IDataResult<User> GetByMailConfirmValue(string value);
        IDataResult<User> GetById(int id);
        IDataResult<UserCompany> GetCompany(int userId);

    }
}