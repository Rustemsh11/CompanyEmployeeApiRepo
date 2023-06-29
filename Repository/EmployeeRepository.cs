using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEmployeeForCompany(Guid CompanyId, Employee employee)
        {
            employee.CompanyId = CompanyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)=> Delete(employee);

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange)
        {
            return await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChange)?.SingleOrDefaultAsync();
        }

        public async Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChange)
                .FilterEmployee(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .ToListAsync();
            return PageList<Employee>.ToPageList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);

            
        }
    }
}
