using Application.Features.Appointment.Commands.Create;
using AutoMapper;

namespace Application.Features.Appointment.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Appointment, CreateAppointmentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Appointment, CreatedAppointmentResponse>().ReverseMap();
    }
}