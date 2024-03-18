
using eReconciliation.Business;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.Entities;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using eReconciliation.Core.Extensions;


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
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = _authService.UserExist(userForRegisterDto.Email);
            if (!userExist.Success)
            {
                return BadRequest(userExist.Message);
            }
            var registerResult = _authService.Register(userForRegisterDto.ConvertTo<UserForRegister>(), userForRegisterDto.Password);
            if (registerResult.Success)
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