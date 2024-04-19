using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.ConsultantPreferences.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ConsultantPreferencesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateConsultantPreferencesCommand request)
    {
        var response = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK, response);
    }
}