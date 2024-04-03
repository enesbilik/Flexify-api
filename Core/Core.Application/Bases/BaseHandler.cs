using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Core.Application.Bases;

public class BaseHandler
{
    public readonly IMapper _mapper;
    public readonly IHttpContextAccessor _httpContextAccessor;

    public BaseHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
}