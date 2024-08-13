using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/operation-claims")]
    public class OperationClaimController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        [HttpGet("{id}")]
        public IActionResult OperationClaimGetById(int id)
        {
            var result = _operationClaimService.OperationClaimGetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("list")]
        public IActionResult OperationClaimGetList()
        {
            var result = _operationClaimService.OperationClaimGetList();
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddOperationClaim(OperationClaim operationClaim)
        {
            var result = _operationClaimService.AddOperationClaim(operationClaim);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut]
        public IActionResult UpdateOperationClaim(OperationClaim operationClaim)
        {
            var result = _operationClaimService.UpdateOperationClaim(operationClaim);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteOperationClaim(OperationClaim operationClaim)
        {
            var result = _operationClaimService.DeleteOperationClaim(operationClaim);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }

}