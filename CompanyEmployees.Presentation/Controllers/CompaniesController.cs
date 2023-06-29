using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.Presentation.ModelBinders;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Service.Contracts;
using Shared.DataTransferObjects;


namespace CompanyEmployees.Presentation.Controllers
{

    [Route("api/companies")]
    [ApiController]
    public class CompaniesController: ControllerBase
    {
        private IServiceManager _serviceManager;
        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;                
        }
        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpGet(Name = "GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            
            
            var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(false);
            return Ok(companies);
            
        }

        

        [HttpGet("{id:guid}",Name = "CompanyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Private, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _serviceManager.CompanyService.GetCompanyAsync(id, false);
            return Ok(company);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = await _serviceManager.CompanyService.GetByIdsAsync(ids, trackChange: false);
            return Ok(companies);
        }

        [HttpPost(Name = "CreateCompany")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDTO company)
        {           
            var createdCompany =  await _serviceManager.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);

        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDTO> companyCollection)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var result = await _serviceManager.CompanyService.CreateCompanyCollectionAsync(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _serviceManager.CompanyService.DeleteCompanyAsync(id, false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody]CompanyForUpdateDTO company)
        {
            await _serviceManager.CompanyService.UpdateCompanyAsync(id, company, true);
            return NoContent();
        }
    }
}
