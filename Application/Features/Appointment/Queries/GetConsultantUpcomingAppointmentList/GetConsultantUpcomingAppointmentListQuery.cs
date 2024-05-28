using System.Security.Claims;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetConsultantUpcomingAppointmentList;

public class GetConsultantUpcomingAppointmentListQuery : IRequest<GetListResponse<GetConsultantUpcomingAppointmentListItemDto>>
{
    
    public class GetConsultantUpcomingAppointmentListQueryHandler : IRequestHandler<GetConsultantUpcomingAppointmentListQuery,
        GetListResponse<GetConsultantUpcomingAppointmentListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConsultantRepository _consultantRepository;

        public GetConsultantUpcomingAppointmentListQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
            IHttpContextAccessor httpContextAccessor, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _consultantRepository = consultantRepository;
        }

        public async Task<GetListResponse<GetConsultantUpcomingAppointmentListItemDto>> Handle(GetConsultantUpcomingAppointmentListQuery request,
            CancellationToken cancellationToken)
        {
            var currentUserMail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            var currentConsultant = await _consultantRepository.GetAsync(c => c.Email == currentUserMail,
                cancellationToken: cancellationToken);

            DateTime today = DateTime.Today;

            var appointments = await _appointmentRepository.GetListAsync(
                include: a =>
                    a.Include(co => co.Consultant)
                        .Include(cl => cl.Client),
                cancellationToken: cancellationToken,
                size: 200,
                predicate: a => a.StartTime >= DateTime.Now 
                                && a.ConsultantId == currentConsultant!.Id 
                                && a.Status == AppointmentStatus.Accepted,
                
                orderBy: a => a.OrderBy(a => a.StartTime)
            );

            GetListResponse<GetConsultantUpcomingAppointmentListItemDto> response =
                _mapper.Map<GetListResponse<GetConsultantUpcomingAppointmentListItemDto>>(appointments);

            return response;
        }
    }
}