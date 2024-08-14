using eReconciliation.Business;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using eReconciliation.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/account-reconciliations")]
    public class AccountReconciliationController : ControllerBase
    {
        private readonly IAccountReconciliationService _accountReconciliationService;

        public AccountReconciliationController(IAccountReconciliationService accountReconciliationService)
        {
            _accountReconciliationService = accountReconciliationService;
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdAccountReconciliation(int id)
        {
            var result = _accountReconciliationService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("codes/{code}")]
        public IActionResult GetByIdAccountReconciliation(string code)
        {
            var result = _accountReconciliationService.GetByCode(code);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("company/{companyId}")]
        public IActionResult GetListdAccountReconciliation(int companyId)
        {
            var result = _accountReconciliationService.GetList(companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddAccountReconciliation(AccountReconciliationDto accountReconciliationDto)
        {
            var result = _accountReconciliationService.Add(accountReconciliationDto.ConvertTo<AccountReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("excel")]
        public IActionResult AddFromExcelAccountReconciliation(IFormFile file, int companyId)
        {
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx";
                var filePath = $"{Directory.GetCurrentDirectory()}/Content/{fileName}";

                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                var result = _accountReconciliationService.AddAccountReconciliationToExcel(filePath, companyId);
                return result.Success ? Ok(result) : BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız.");
        }
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendReconciliationMail(int id)
        {
            var result = await _accountReconciliationService.SendReconciliationMail(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut]
        public IActionResult UpdateAccountReconciliation(AccountReconciliationDto accountReconciliationDto)
        {
            var result = _accountReconciliationService.Update(accountReconciliationDto.ConvertTo<AccountReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteAccountReconciliation(AccountReconciliationDto accountReconciliationDto)
        {
            var result = _accountReconciliationService.Delete(accountReconciliationDto.ConvertTo<AccountReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

    }
}