using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Entities.Concrete;
using eReconciliation.Core.Extensions;
using eReconciliation.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/mail-templates")]
    public class MailTemplateController : ControllerBase
    {
        private readonly IMailTemplateService _mailTemplateService;

        public MailTemplateController(IMailTemplateService mailTemplateService)
        {
            _mailTemplateService = mailTemplateService;
        }

        [HttpGet("{id}")]
        public IActionResult GetMailTemplateById(int id)
        {
            var result = _mailTemplateService.GetMailTemplateById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("{companyId}")]
        public IActionResult GetMailTemplates(int companyId)
        {
            var result = _mailTemplateService.GetMailTemplates(companyId);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult AddMailTemplate([FromBody] MailTemplateDto mailTemplateDto)
        {
            var result = _mailTemplateService.AddMailTemplate(mailTemplateDto.ConvertTo<MailTemplate>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut("update")]
        public IActionResult UpdateMailTemplate([FromBody] MailTemplateDto mailTemplateDto)
        {
            var result = _mailTemplateService.UpdateMailTemplate(mailTemplateDto.ConvertTo<MailTemplate>());
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}