using Application.Features.Appointment.Commands.Create;
using Application.Features.Appointment.Queries.GetListByDynamic;
using Application.Features.Consultant.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Appointment, CreateAppointmentCommand>().ReverseMap();
        CreateMap<Domain.Entities.Appointment, CreatedAppointmentResponse>().ReverseMap();

        CreateMap<Domain.Entities.Appointment, GetListByDynamicAppointmentListItemDto>().ReverseMap();
        CreateMap<Paginate<Domain.Entities.Appointment>, GetListResponse<GetListByDynamicAppointmentListItemDto>>()
            .ReverseMap();
    }
}