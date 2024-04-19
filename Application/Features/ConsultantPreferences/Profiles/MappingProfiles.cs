using Application.Features.ConsultantPreferences.Commands.Update;
using AutoMapper;

namespace Application.Features.ConsultantPreferences.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.ConsultantPreferences, UpdateConsultantPreferencesCommand>().ReverseMap();
        CreateMap<Domain.Entities.ConsultantPreferences, UpdatedConsultantPreferencesResponse>().ReverseMap();
    }
}