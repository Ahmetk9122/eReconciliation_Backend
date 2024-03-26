
using eReconciliation.Business;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.Entities;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using eReconciliation.Core.Extensions;
using eReconciliation.Entities.Concrete;


namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auths")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICompanyService _companyService;

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
                return BadRequest(userExist.Message);
            }

            var registerResult = _authService.Register(userAndCompanyRegisteredDto.UserForRegisterDto.ConvertTo<UserForRegister>(), userAndCompanyRegisteredDto.UserForRegisterDto.Password, userAndCompanyRegisteredDto.Company);
            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(registerResult);
            }
            return BadRequest(registerResult.Message);
        }
        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegisterDto userForRegisterDto, int companyId)
        {
            var userExist = _authService.UserExist(userForRegisterDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = _authService.RegisterSecondAccount(userForRegisterDto.ConvertTo<UserForRegister>(), userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data, companyId);
            if (result.Success)
            {
                return Ok(registerResult);
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
            var result = _authService.CreateAccessToken(userForLogin.Data, 0);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}