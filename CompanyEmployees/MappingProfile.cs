using AutoMapper;
using Entities;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(c=>c.FullAddress
                , opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CompanyForCreationDTO, Company>();
            CreateMap<EmployeeForCreateDTO, Employee>();
            CreateMap<EmployeeForUpdateDTO, Employee>().ReverseMap();
            CreateMap<CompanyForUpdateDTO, Company>();
        }
    }
}
