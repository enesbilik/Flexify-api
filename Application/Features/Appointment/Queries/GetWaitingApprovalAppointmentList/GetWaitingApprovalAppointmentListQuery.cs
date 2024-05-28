using System.Security.Claims;
using Application.Features.Appointment.Queries.GetConsultantUpcomingAppointmentList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetWaitingApprovalAppointmentList;

public class GetWaitingApprovalAppointmentListQuery : IRequest<GetListResponse<GetWaitingApprovalAppointmentListItemDto>>
{
    public class GetWaitingApprovalAppointmentListQueryHandler : IRequestHandler<GetWaitingApprovalAppointmentListQuery,
        GetListResponse<GetWaitingApprovalAppointmentListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConsultantRepository _consultantRepository;

        public GetWaitingApprovalAppointmentListQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository,
            IHttpContextAccessor httpContextAccessor, IConsultantRepository consultantRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _consultantRepository = consultantRepository;
        }

        public async Task<GetListResponse<GetWaitingApprovalAppointmentListItemDto>> Handle(GetWaitingApprovalAppointmentListQuery request,
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
                                && a.Status == AppointmentStatus.Pending,
                orderBy: a => a.OrderBy(a => a.StartTime)
            );

            GetListResponse<GetWaitingApprovalAppointmentListItemDto> response =
                _mapper.Map<GetListResponse<GetWaitingApprovalAppointmentListItemDto>>(appointments);

            return response;
        }
    }
}