using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Consultant.Queries.GetListByDynamic;

public class GetListByDynamicConsultantQuery : IRequest<GetListResponse<GetListByDynamicConsultantListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }


    public class
        GetListByDynamicConsultantQueryHandler : IRequestHandler<GetListByDynamicConsultantQuery,
            GetListResponse<GetListByDynamicConsultantListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IConsultantRepository _consultantRepository;

        public GetListByDynamicConsultantQueryHandler(IMapper mapper, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _consultantRepository = consultantRepository;
        }

        public async Task<GetListResponse<GetListByDynamicConsultantListItemDto>> Handle(
            GetListByDynamicConsultantQuery request,
            CancellationToken cancellationToken)
        {
            Paginate<Domain.Entities.Consultant> consultants = await _consultantRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDynamicConsultantListItemDto> response =
                _mapper.Map<GetListResponse<GetListByDynamicConsultantListItemDto>>(consultants);

            return response;
        }
    }
}