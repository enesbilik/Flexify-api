using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Appointment.Commands.Create;
using Application.Features.Appointment.Queries.GetAppointmentsAvailabilityList;
using Application.Features.Appointment.Queries.GetListByDynamic;
using Application.Features.Appointment.Queries.GetUpcomingAppointmentsList;
using Application.Features.Consultant.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
//[Authorize]
public class AppointmentController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand request)
    {
        CreatedAppointmentResponse createdAppointmentResponse = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, createdAppointmentResponse);
    }


    [HttpPost]
    public async Task<IActionResult> GetListByDynamic(
        [FromQuery] PageRequest pageRequest,
        [FromBody] DynamicQuery dynamicQuery = null
    )
    {
        GetListByDynamicAppointmentQuery getListByDynamicAppointmentQuery = new()
        {
            DynamicQuery = dynamicQuery,
            PageRequest = pageRequest
        };

        GetListResponse<GetListByDynamicAppointmentListItemDto> response =
            await Mediator.Send(getListByDynamicAppointmentQuery);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAppointmentAvailabilityList([FromBody] GetAppointmentAvailabilityListQuery request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUpcomingAppointmentsList()
    {
        var getUpcomingAppointmentsListQuery = new GetUpcomingAppointmentsListQuery();

        GetListResponse<GetUpcomingAppointmentsListItemDto> response =
            await Mediator.Send(getUpcomingAppointmentsListQuery);

        return Ok(response);
    }
}