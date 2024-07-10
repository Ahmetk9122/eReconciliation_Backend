using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business;
using eReconciliation.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetCompanyList()
        {
            var result = _companyService.GetList();
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            var result = _companyService.GetCompanyById(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost]
        public IActionResult AddCompany([FromBody] CompanyDto companyDto)
        {
            var result = _companyService.Add(companyDto.Company);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPost("add/mappings")]
        public IActionResult AddCompanyWithUserMapping([FromBody] CompanyDto companyDto)
        {
            var result = _companyService.AddCompanyAndUserCompany(companyDto);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
        [HttpPut]
        public IActionResult UpdateCompany([FromBody] Company company)
        {
            var result = _companyService.UpdateCompany(company);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

    }
}