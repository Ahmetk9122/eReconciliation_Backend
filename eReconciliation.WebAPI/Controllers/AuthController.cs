
using eReconciliation.Business;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.Entities;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using eReconciliation.Core.Extensions;
using eReconciliation.Entities.Concrete;
using Microsoft.AspNetCore.Identity;


namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auths")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserAndCompanyRegisteredDto userAndCompanyRegisteredDto)
        {
            var userExist = _authService.UserExist(userAndCompanyRegisteredDto.UserForRegisterDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var companyExist = _authService.CompanyExist(userAndCompanyRegisteredDto.Company);
            if (!companyExist.Success)
            {
                return BadRequest(companyExist.Message);
            }

            var registerResult = _authService.Register(userAndCompanyRegisteredDto.UserForRegisterDto.ConvertTo<UserForRegister>(), userAndCompanyRegisteredDto.UserForRegisterDto.Password, userAndCompanyRegisteredDto.Company);
            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegistertoSecondAccountDto userForRegistertoSecondAccountDto)
        {
            var userExist = _authService.UserExist(userForRegistertoSecondAccountDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = _authService.RegisterSecondAccount(userForRegistertoSecondAccountDto.ConvertTo<UserForRegister>(), userForRegistertoSecondAccountDto.Password, userForRegistertoSecondAccountDto.CompanyId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegistertoSecondAccountDto.CompanyId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userForLogin = _authService.Login(userForLoginDto.ConvertTo<UserForLogin>());
            if (!userForLogin.Success)
            {
                return BadRequest(userForLogin.Message);
            }

            if (userForLogin.Data.IsActive)
            {
                var userCompany = _authService.GetCompany(userForLogin.Data.Id).Data;
                var result = _authService.CreateAccessToken(userForLogin.Data, userCompany.CompanyId);
                if (result.Success)
                {
                    return Ok(result.Data);
                }
                return BadRequest(result.Message);

            }
            return BadRequest("Kullanıcı pasif durumda. Aktif etmek için yöneticinize danışın.");

        }

        [HttpGet("confirmUser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            var result = _authService.UpdateUser(user);
            if (result.Success)
                return Ok();
            return BadRequest(result.Message);
        }

        [HttpGet("sendConfirmEmail")]
        public IActionResult SendConfirmEmail(int userId)
        {
            var user = _authService.GetById(userId).Data;
            var result = _authService.SendConfirmEmail(user);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}