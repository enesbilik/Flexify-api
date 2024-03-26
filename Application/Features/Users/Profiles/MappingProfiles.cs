using System;
using System.Drawing.Drawing2D;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
        CreateMap<AppUser, CreatedUserResponse>().ReverseMap();

        CreateMap<AppUser, GetListUserListItemDto>().ReverseMap();

        CreateMap<Paginate<AppUser>, GetListResponse<GetListUserListItemDto>>().ReverseMap();



    }
}
