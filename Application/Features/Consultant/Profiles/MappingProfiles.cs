using Application.Features.Consultant.Queries.GetById;
using Application.Features.Consultant.Queries.GetList;
using Application.Features.Consultant.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;

namespace Application.Features.Consultant.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Consultant, GetListConsultantListItemDto>().ReverseMap();
        CreateMap<Paginate<Domain.Entities.Consultant>, GetListResponse<GetListConsultantListItemDto>>().ReverseMap();

        CreateMap<Domain.Entities.Consultant, GetListByDynamicConsultantListItemDto>().ReverseMap();
        CreateMap<Paginate<Domain.Entities.Consultant>, GetListResponse<GetListByDynamicConsultantListItemDto>>()
            .ReverseMap();


        CreateMap<Domain.Entities.Consultant, GetByIdConsultantResponse>().ReverseMap();
    }
}