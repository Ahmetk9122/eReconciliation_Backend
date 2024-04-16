using eReconciliation.Business;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;
using eReconciliation.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/mail-parameters")]
    public class MailParameterController : ControllerBase
    {
        private readonly IMailParameterService _mailParameterService;

        public MailParameterController(IMailParameterService mailParameterService)
        {
            _mailParameterService = mailParameterService;
        }

        [HttpGet("{companyId}")]
        public IActionResult GetMailParametersByCompanyId(int companyId)
        {
            var result = _mailParameterService.GetMailParameter(companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost()]
        public IActionResult GetMailParametersByCompanyId([FromBody] MailParameterDto mailParameterDto)
        {
            var result = _mailParameterService.UpdateMailParameter(mailParameterDto.ConvertTo<MailParameter>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}