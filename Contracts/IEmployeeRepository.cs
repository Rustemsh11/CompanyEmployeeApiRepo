using Entities;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange);
        Task<Employee> GetEmployeeAsync(Guid companyId,Guid employeeId, bool trackChange);
        void CreateEmployeeForCompany(Guid CompanyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
