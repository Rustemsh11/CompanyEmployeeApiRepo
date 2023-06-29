using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

    using System.Net.Http;  

namespace Entities.LinkModels
{
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext
        Context);

}
