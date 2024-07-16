using eReconciliation.Business;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using eReconciliation.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/currency-accounts")]
    public class CurrencyAccountController : ControllerBase
    {
        private readonly ICurrencyAccountService _currencyAccountService;
        public CurrencyAccountController(ICurrencyAccountService currencyAccountService)
        {
            _currencyAccountService = currencyAccountService;
        }
        [HttpGet("companies/{companyId}")]
        public IActionResult GetCurrencyAccountsByCompanyId(int companyId)
        {
            var result = _currencyAccountService.GetCurrencyAccounts(companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("{id}")]
        public IActionResult GetCurrencyAccountById(int id)
        {
            var result = _currencyAccountService.GetCurrencyAccountById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddCurrencyAccount([FromBody] CurrencyAccountDto currencyAccountDto)
        {
            var result = _currencyAccountService.AddCurrencyAccount(currencyAccountDto.ConvertTo<CurrencyAccount>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut]
        public IActionResult UpdateCurrencyAccount([FromBody] CurrencyAccountDto currencyAccountDto)
        {
            var result = _currencyAccountService.UpdateCurrencyAccount(currencyAccountDto.ConvertTo<CurrencyAccount>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteCurrencyAccount([FromBody] CurrencyAccountDto currencyAccountDto)
        {
            var result = _currencyAccountService.DeleteCurrencyAccount(currencyAccountDto.ConvertTo<CurrencyAccount>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}