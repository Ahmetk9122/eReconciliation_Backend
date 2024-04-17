using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Transactions;
using eReconciliation.Business.Abstract;
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
        private readonly IMailParameterService _mailParameterService;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;

        public AuthService(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IMailParameterService mailParameterService, IMailService mailService, IMailTemplateService mailTemplateService)
        {
            _mailTemplateService = mailTemplateService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _mailParameterService = mailParameterService;
            _mailService = mailService;
            _userService = userService;

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
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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

                #region [ Send Mail ]

                string subject = "Kullanıcı Onay Maili";
                string body = "Kullanıcınız sisteme kayıt oldu kaydınızı tamamlamak için aşağıdaki linke tıklayınız.";
                string link = "https://localhost:7220";
                string linkDescription = "Kaydı onaylamak için tıklayın";

                var mailTemplate = _mailTemplateService.GetMailTemplateName("Kayıt", 4);
                string templateBody = mailTemplate.Data.Value;
                templateBody = templateBody.Replace("{{title}}", subject);
                templateBody = templateBody.Replace("{{message}}", body);
                templateBody = templateBody.Replace("{{link}}", link);
                templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);

                var mailParameter = _mailParameterService.GetMailParameter(10);

                var sendMail = new SendMailDto()
                {
                    MailParameter = mailParameter.Data,
                    Email = user.Email,
                    Subject = subject,
                    Body = templateBody
                };
                _mailService.SendMail(sendMail);

                #endregion
                scope.Complete();



                return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
            }
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
        public IResult CompanyExist(Company company)
        {
            var result = _companyService.CompanyExists(company);
            if (result.Success == false)
            {
                return new ErrorResult(Messages.CompanyAlreadyExist);
            }
            return new SuccessResult();
        }

    }
}