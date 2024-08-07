using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using eReconciliation.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/account-reconciliation-details")]
    public class AccountReconciliationDetailController : ControllerBase
    {
        private readonly IAccountReconciliationDetailService _accountReconciliationDetailService;

        public AccountReconciliationDetailController(IAccountReconciliationDetailService accountReconciliationDetailService)
        {
            _accountReconciliationDetailService = accountReconciliationDetailService;
        }

        [HttpGet("{id}")]
        public IActionResult AccountReconciliationDetailGetById(int id)
        {
            var result = _accountReconciliationDetailService.AccountReconciliationDetailGetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("accountReconciliations/{accountReconciliationId}")]
        public IActionResult AccountReconciliationDetailGetList(int accountReconciliationId)
        {
            var result = _accountReconciliationDetailService.AccountReconciliationDetailGetList(accountReconciliationId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddAccountReconciliationDetail(AccountReconciliationDetailDto accountReconciliationDetailDto)
        {
            var result = _accountReconciliationDetailService.AddAccountReconciliationDetail(accountReconciliationDetailDto.ConvertTo<AccountReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("excel")]
        public IActionResult AddAccountReconciliationDetailToExcel(IFormFile file, int accountReconciliationId)
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

                var result = _accountReconciliationDetailService.AddAccountReconciliationDetailToExcel(filePath, accountReconciliationId);
                return result.Success ? Ok(result) : BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız.");
        }

        [HttpPut]
        public IActionResult UpdateAccountReconciliationDetail(AccountReconciliationDetailDto accountReconciliationDetailDto)
        {
            var result = _accountReconciliationDetailService.UpdateAccountReconciliationDetail(accountReconciliationDetailDto.ConvertTo<AccountReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteAccountReconciliationDetail(AccountReconciliationDetailDto accountReconciliationDetailDto)
        {
            var result = _accountReconciliationDetailService.DeleteAccountReconciliationDetail(accountReconciliationDetailDto.ConvertTo<AccountReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

    }
}