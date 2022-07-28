namespace Functoso.Application.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ForMember(t => t.CompanyName, o => o.MapFrom(u => u.Company != null ? u.Company.Name : null));
    }
}
