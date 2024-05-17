using System.Security.Claims;
using Application.Features.Consultant.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetUpcomingAppointmentsList;

public class GetUpcomingAppointmentsListQuery : IRequest<GetListResponse<GetUpcomingAppointmentsListItemDto>>
{
    public class GetUpcomingAppointmentsListQueryHandler : IRequestHandler<GetUpcomingAppointmentsListQuery,
        GetListResponse<GetUpcomingAppointmentsListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientRepository _clientRepository;


        public GetUpcomingAppointmentsListQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
            IHttpContextAccessor httpContextAccessor, IClientRepository clientRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _clientRepository = clientRepository;
        }

        public async Task<GetListResponse<GetUpcomingAppointmentsListItemDto>> Handle(GetUpcomingAppointmentsListQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserMail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var currentClient = await _clientRepository.GetAsync(c => c.Email == currentUserMail,
                cancellationToken: cancellationToken);


            DateTime today = DateTime.Today;


            var appointments = await _appointmentRepository.GetListAsync(
                include: a =>
                    a.Include(co => co.Consultant)
                        .Include(cl => cl.Client),
                cancellationToken: cancellationToken,
                size: 200,
                predicate: a => a.StartTime >= DateTime.Now && a.ClientId == currentClient!.Id && a.Status != AppointmentStatus.Empty,
                orderBy: a => a.OrderBy(a => a.StartTime)
            );

            GetListResponse<GetUpcomingAppointmentsListItemDto> response =
                _mapper.Map<GetListResponse<GetUpcomingAppointmentsListItemDto>>(appointments);

            return response;
        }
    }
}