using System;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using System.Drawing.Drawing2D;
using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>, ICachableRequest, ILoggableRequest
{

    public PageRequest PageRequest { get; set; }

    public string CacheKey => $"GetListUserQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

    public bool BypassCache { get; }

    public TimeSpan? SlidingExpiration { get; }

    public string? CacheGroupKey => "GetUsers";

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            Paginate<AppUser> users = await _userRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetListUserListItemDto> response = _mapper.Map<GetListResponse<GetListUserListItemDto>>(users);
            return response;
        }
    }

}