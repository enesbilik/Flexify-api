using System;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using System.Drawing.Drawing2D;
using Application.DTOs;
using Domain.Entities;
using Application.Features.Auth.Commands.Register;

namespace Application.Features.Auth.Profiles;


public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AppUser, RegisterCommand>().ReverseMap();
        CreateMap<AppUser, RegisteredCommandResponse>().ReverseMap();
        
        CreateMap<Consultant, ConsultantInfoDto>().ReverseMap();
        CreateMap<Client, ClientInfoDto>().ReverseMap();
        

    }
}

