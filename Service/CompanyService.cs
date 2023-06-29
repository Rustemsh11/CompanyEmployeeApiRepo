using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class CompanyService:ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyForCreationDTO company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.SaveAsync();
            var companyToReturn = _mapper.Map<CompanyDTO>(companyEntity);
            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDTO> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDTO> companyCollection)
        {
            if(companyCollection is null)
            {
                throw new CompanyCollectionBadRequest();
            }
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }
            await _repository.SaveAsync();

            var companyCollectionReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);

            var ids = string.Join(",", companyCollectionReturn.Select(c=>c.Id));
            return (companyCollectionReturn, ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfExist(companyId, trackChanges);
            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(bool trackChange)
        {
           
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChange);
            var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesDTO;
             
        }

        public async Task<IEnumerable<CompanyDTO>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChange)
        {
            if(ids is null)
            {
                throw new IdParametersBadRequestException();
            }
            var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChange);
            if(companyEntities.Count() != ids.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntities);
            return companiesToReturn;
        }

        public async Task<CompanyDTO> GetCompanyAsync(Guid companyId, bool trackChange)
        {
            var company = await GetCompanyAndCheckIfExist(companyId, trackChange);
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return companyDTO;
        }



        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDTO companyForUpdate, bool trackChanges)
        {
            var company = await GetCompanyAndCheckIfExist(companyId, trackChanges);
            _mapper.Map(companyForUpdate, company);
            await _repository.SaveAsync();
        }

        private async Task<Company> GetCompanyAndCheckIfExist(Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges);
            if(company is null)
            {
                throw new CompanyNotFoundException(id);
            }
            return company;
        }

    }
}
