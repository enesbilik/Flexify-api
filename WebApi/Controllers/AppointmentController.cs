using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Appointment.Commands.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AppointmentController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand request)
    {
        CreatedAppointmentResponse createdAppointmentResponse = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, createdAppointmentResponse);
    }
}