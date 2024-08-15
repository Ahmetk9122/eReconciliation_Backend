using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business;
using eReconciliation.Core.Extensions;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/baBs-reconciliations")]
    public class BaBsReconciliationController : ControllerBase
    {
        private readonly IBaBsReconciliationService _baBsReconciliationService;

        public BaBsReconciliationController(IBaBsReconciliationService baBsReconciliationService)
        {
            _baBsReconciliationService = baBsReconciliationService;
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdBaBsReconciliation(int id)
        {
            var result = _baBsReconciliationService.BaBsReconciliationGetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("company/{companyId}")]
        public IActionResult GetListdBaBsReconciliation(int companyId)
        {
            var result = _baBsReconciliationService.BaBsReconciliationGetList(companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("codes/{code}")]
        public IActionResult BaBsReconciliationGetByCode(string code)
        {
            var result = _baBsReconciliationService.GetByCode(code);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendBaBsReconciliationMail(int id)
        {
            var result = await _baBsReconciliationService.SendBaBsReconciliationMail(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddBaBsReconciliation(BaBsReconciliationDto baBsReconciliationDto)
        {
            var result = _baBsReconciliationService.AddBaBsReconciliation(baBsReconciliationDto.ConvertTo<BaBsReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("excel")]
        public IActionResult AddFromExcelBaBsReconciliation(IFormFile file, int companyId)
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

                var result = _baBsReconciliationService.AddBaBsReconciliationToExcel(filePath, companyId);
                return result.Success ? Ok(result) : BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız.");
        }

        [HttpPut]
        public IActionResult UpdateBaBsReconciliation(BaBsReconciliationDto baBsReconciliationDto)
        {
            var result = _baBsReconciliationService.UpdateBaBsReconciliation(baBsReconciliationDto.ConvertTo<BaBsReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteBaBsReconciliation(BaBsReconciliationDto baBsReconciliationDto)
        {
            var result = _baBsReconciliationService.DeleteBaBsReconciliation(baBsReconciliationDto.ConvertTo<BaBsReconciliation>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}