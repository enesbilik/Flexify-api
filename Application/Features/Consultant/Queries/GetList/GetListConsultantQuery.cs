using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Consultant.Queries.GetList;

public class GetListConsultantQuery : IRequest<GetListResponse<GetListConsultantListItemDto>>
{
    public PageRequest PageRequest { get; set; }


    public class
        GetListConsultantQueryHandler : IRequestHandler<GetListConsultantQuery,
            GetListResponse<GetListConsultantListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IConsultantRepository _consultantRepository;

        public GetListConsultantQueryHandler(IMapper mapper, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _consultantRepository = consultantRepository;
        }

        public async Task<GetListResponse<GetListConsultantListItemDto>> Handle(GetListConsultantQuery request,
            CancellationToken cancellationToken)
        {
            Paginate<Domain.Entities.Consultant> consultants = await _consultantRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListConsultantListItemDto> response =
                _mapper.Map<GetListResponse<GetListConsultantListItemDto>>(consultants);

            return response;
        }
    }
}