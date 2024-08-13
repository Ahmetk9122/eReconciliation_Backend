using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Core.Extensions;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user-operation-claims")]
    public class UserOperationClaimController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }
        [HttpGet("{id}")]
        public IActionResult UserOperationClaimGetById(int id)
        {
            var result = _userOperationClaimService.UserOperationClaimGetById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("companies/{companyId}/users/{userId}")]
        public IActionResult UserOperationClaimGetList(int companyId, int userId)
        {
            var result = _userOperationClaimService.UserOperationClaimGetList(userId, companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddUserOperationClaim(UserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.AddUserOperationClaim(userOperationClaimDto.ConvertTo<UserOperationClaim>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut]
        public IActionResult UpdateUserOperationClaim(UserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.UpdateUserOperationClaim(userOperationClaimDto.ConvertTo<UserOperationClaim>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpDelete]
        public IActionResult DeleteUserOperationClaim(UserOperationClaimDto userOperationClaimDto)
        {
            var result = _userOperationClaimService.DeleteUserOperationClaim(userOperationClaimDto.ConvertTo<UserOperationClaim>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}