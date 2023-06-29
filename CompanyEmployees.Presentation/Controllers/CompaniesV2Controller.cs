﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/companies")]
    [ApiController]
    public class CompaniesV2Controller:ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesV2Controller(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService
                .GetAllCompaniesAsync(false);
            var companiesV2 = companies.Select(x => $"{x.Name} V2");
            return Ok(companiesV2);
        }
    }
}
