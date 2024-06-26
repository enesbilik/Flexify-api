using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Appointment.Commands.Create;
using Application.Features.Appointment.Commands.Delete;
using Application.Features.Appointment.Commands.Update;
using Application.Features.Appointment.Queries.GetAppointmentsAvailabilityList;
using Application.Features.Appointment.Queries.GetById;
using Application.Features.Appointment.Queries.GetConsultantUpcomingAppointmentList;
using Application.Features.Appointment.Queries.GetListByDynamic;
using Application.Features.Appointment.Queries.GetUpcomingAppointmentsList;
using Application.Features.Appointment.Queries.GetWaitingApprovalAppointmentList;
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

    [HttpPost]
    public async Task<IActionResult> UpdateAppointmentStatus([FromBody] UpdateAppointmentCommand request)
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

    [HttpGet]
    public async Task<IActionResult> GetConsultantUpcomingAppointmentsList()
    {
        var getConsultantUpcomingAppointmentListQuery = new GetConsultantUpcomingAppointmentListQuery();

        GetListResponse<GetConsultantUpcomingAppointmentListItemDto> response =
            await Mediator.Send(getConsultantUpcomingAppointmentListQuery);


        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetWaitingApprovalAppointmentList()
    {
        var getWaitingApprovalAppointmentListQuery = new GetWaitingApprovalAppointmentListQuery();

        GetListResponse<GetWaitingApprovalAppointmentListItemDto> response =
            await Mediator.Send(getWaitingApprovalAppointmentListQuery);

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAppointmentQuery getByIdAppointmentQuery = new() { Id = id };

        var response = await Mediator.Send(getByIdAppointmentQuery);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var deleteAppointmentCommand = new DeleteAppointmentCommand { Id = id };

        await Mediator.Send(deleteAppointmentCommand);

        return Ok();
    }
}