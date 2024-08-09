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
    [Route("api/baBs-reconciliation-details")]
    public class BaBsReconciliationDetailController : ControllerBase
    {
        private readonly IBaBsReconciliationDetailService _baBsReconciliationDetailService;

        public BaBsReconciliationDetailController(IBaBsReconciliationDetailService baBsReconciliationDetailService)
        {
            _baBsReconciliationDetailService = baBsReconciliationDetailService;
        }
        [HttpGet("{id}")]
        public IActionResult BaBsReconciliationDetailGetById(int id)
        {
            var result = _baBsReconciliationDetailService.BaBsReconciliationDetailGetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("baBs-reconciliations/{baBsReconciliationId}")]
        public IActionResult BaBsReconciliationDetailGetList(int baBsReconciliationId)
        {
            var result = _baBsReconciliationDetailService.BaBsReconciliationDetailGetList(baBsReconciliationId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddBaBsReconciliationDetail(BaBsReconciliationDetailDto baBsReconciliationDetailDto)
        {
            var result = _baBsReconciliationDetailService.AddBaBsReconciliationDetail(baBsReconciliationDetailDto.ConvertTo<BaBsReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("excel")]
        public IActionResult AddBaBsReconciliationDetailToExcel(IFormFile file, int baBsReconciliationId)
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

                var result = _baBsReconciliationDetailService.AddBaBsReconciliationDetailToExcel(filePath, baBsReconciliationId);
                return result.Success ? Ok(result) : BadRequest(result.Message);
            }
            return BadRequest("Dosya seçimi yapmadınız.");
        }

        [HttpPut]
        public IActionResult UpdateBaBsReconciliationDetail(BaBsReconciliationDetailDto baBsReconciliationDetailDto)
        {
            var result = _baBsReconciliationDetailService.UpdateBaBsReconciliationDetail(baBsReconciliationDetailDto.ConvertTo<BaBsReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteBaBsReconciliationDetail(BaBsReconciliationDetailDto baBsReconciliationDetailDto)
        {
            var result = _baBsReconciliationDetailService.DeleteBaBsReconciliationDetail(baBsReconciliationDetailDto.ConvertTo<BaBsReconciliationDetail>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

    }
}