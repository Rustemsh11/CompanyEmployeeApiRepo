using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts
{
    public interface IEmployeeLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<EmployeeDTO> employeeDTO, string fields,
            Guid companyId, HttpContext httpContext);
    }
}
