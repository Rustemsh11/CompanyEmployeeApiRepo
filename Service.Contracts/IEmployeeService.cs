using Entities;
using Entities.LinkModels;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(Guid companyId, LinkParameters linkParameters, bool trackChanges);
        Task<EmployeeDTO> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange);
        Task<EmployeeDTO> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreateDTO employee, bool trackChanges);

        Task DeleteEmployeeForComapnyAsync(Guid companyId, Guid id, bool trackChange);
        Task UpdateEmployeeForComapnyAsync(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges);

        Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChange, bool empTrackChange);

        Task SaveChangesForPatchAsync(EmployeeForUpdateDTO employeeForPatch, Employee employeeEntity);
    }
}
