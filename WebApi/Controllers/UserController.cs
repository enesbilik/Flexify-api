using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Users.Commands.Create;
using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
    {
        CreatedUserResponse response = await Mediator.Send(createUserCommand);
        return Ok(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);

        return Ok(response);
    }


    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetById([FromRoute] Guid id)
    //{
    //    GetByIdBrandQuery getByIdBrandQuery = new() { Id = id };

    //    GetByIdBrandResponse response = await Mediator.Send(getByIdBrandQuery);

    //    return Ok(response);
    //}

    //[HttpPut]
    //public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
    //{
    //    UpdatedBrandResponse response = await Mediator.Send(updateBrandCommand);
    //    return Ok(response);
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete([FromRoute] Guid id)
    //{
    //    var deleteBrandCommand = new DeleteBrandCommand() { Id = id };

    //    var response = await Mediator.Send(deleteBrandCommand);

    //    return Ok(response);
    //}


}

