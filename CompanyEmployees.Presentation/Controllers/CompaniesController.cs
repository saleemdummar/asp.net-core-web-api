﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCompanies()
        {

            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);

        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto companyDto)
        {
            if (companyDto is null)
                return BadRequest("CompanyForCreationDto object is null");
            var company = _service.CompanyService.CreateCompany(companyDto);
            return CreatedAtRoute("CompanyById", new { id = company.Id }, company);
        }

        [HttpGet("collection/({ids})", Name ="CompanyCollection")]
        public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids,trackChanges:false);
            return Ok(companies);
        }
    }
}
