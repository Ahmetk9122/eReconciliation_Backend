using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Core;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.Entities;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;

namespace eReconciliation.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyService _companyService;

        public AuthService(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService)
        {
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _userService = userService;

        }

        public IResult CompanyExist(Company company)
        {
            return _companyService.CompanyExists(company);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
        {
            var claims = _userService.GetClaims(user, companyId);
            var accessToken = _tokenHelper.CreateToken(user, claims, companyId);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToCheck = _userService.GetByMail(userForLogin.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);

        }

        public IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company)
        {
            #region [ Kullanıcı Kayıdı ]
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name
            };
            _userService.Add(user);
            #endregion

            #region [ Şirket Kayıdı ]

            _companyService.Add(company);

            #endregion

            #region [ Kullanıcı Şirket Mapping Kayıdı ]

            _companyService.UserCompanyMapingAdd(user.Id, company.Id);

            #endregion

            UserCompanyDto userCompanyDto = new UserCompanyDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AddedAt = user.AddedAt,
                CompanyId = company.Id,
                IsActive = true,
                MailConfirm = user.MailConfirm,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };

            return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByMail(email) != null)
                return new ErrorResult(Messages.UserExist);
            return new SuccessResult();
        }

    }
}