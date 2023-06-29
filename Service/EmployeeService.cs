using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Entities.LinkModels;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Dynamic;

namespace Service
{
    internal class EmployeeService:IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IEmployeeLinks _employeeLinks;
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IEmployeeLinks employeeLinks)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _employeeLinks = employeeLinks;
        }

        public async Task<EmployeeDTO> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreateDTO employee, bool trackChanges)
        {
            await CheckIfCompanyExist(companyId, trackChanges);

            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeReturnDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employeeReturnDTO;
        }

        public async Task DeleteEmployeeForComapnyAsync(Guid companyId, Guid id, bool trackChange)
        {
            await CheckIfCompanyExist(companyId, trackChange);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfExist(companyId, id, trackChange); 

            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync();
        }

        public async Task<EmployeeDTO> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChange)
        {
            await CheckIfCompanyExist(companyId, trackChange);
            var employee = await GetEmployeeForCompanyAndCheckIfExist(companyId, employeeId, trackChange);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        public  async Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChange, bool empTrackChange)
        {
            await CheckIfCompanyExist(companyId, compTrackChange);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfExist(companyId, id, empTrackChange);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDTO>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync
            (Guid companyId, LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.EmployeeParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();
            await CheckIfCompanyExist(companyId, trackChanges);
            var employeesWithMetaData = await _repository.Employee
                .GetEmployeesAsync(companyId, linkParameters.EmployeeParameters,trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDTO>>(employeesWithMetaData);
            var links = _employeeLinks.TryGenerateLinks(employeesDto,
            linkParameters.EmployeeParameters.Fields,
            companyId, linkParameters.Context);
            return (linkResponse: links, metaData: employeesWithMetaData.MetaData);
        }


        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForComapnyAsync(Guid companyId, Guid id, EmployeeForUpdateDTO employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
        {
            await CheckIfCompanyExist(companyId, compTrackChanges);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfExist(companyId, id, empTrackChanges);

            _mapper.Map(employeeForUpdate,employeeEntity);
            await _repository.SaveAsync();

        }
        private async Task CheckIfCompanyExist(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if(company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
        }
        private async Task<Employee> GetEmployeeForCompanyAndCheckIfExist(Guid companyId, Guid id, bool trackChanges)
        {
            var emplyeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if(emplyeeDb is null)
            {
                throw new EmployeeNotFound(id);
            }
            return emplyeeDb;
        }
        
    }
}
