using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Consultant.Queries.GetById;
using Application.Features.Consultant.Queries.GetList;
using Application.Features.Consultant.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsultantController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListConsultantQuery getListConsultantQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListConsultantListItemDto> response = await Mediator.Send(getListConsultantQuery);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetListByDynamic(
            [FromQuery] PageRequest pageRequest,
            [FromBody] DynamicQuery dynamicQuery = null
        )
        {
            GetListByDynamicConsultantQuery getListByDynamicConsultantQuery = new()
            {
                DynamicQuery = dynamicQuery,
                PageRequest = pageRequest
            };

            GetListResponse<GetListByDynamicConsultantListItemDto> response =
                await Mediator.Send(getListByDynamicConsultantQuery);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdConsultantQuery getByIdConsultantQuery = new() { Id = id };

            GetByIdConsultantResponse response = await Mediator.Send(getByIdConsultantQuery);

            return Ok(response);
        }
    }
}