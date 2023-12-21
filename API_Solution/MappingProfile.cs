using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace API_Solution
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<User, UserDto>();
            CreateMap<Admin, AdminDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<AdminForCreatonDto, Admin>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<AdminForUpdateDto, Admin>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<UserForUpdateDto, User>().ReverseMap();
        }
    }
}
