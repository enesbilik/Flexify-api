using Application.Features.Consultant.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetListByDynamic;

public class GetListByDynamicAppointmentQuery : IRequest<GetListResponse<GetListByDynamicAppointmentListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class
        GetListByDynamicAppointmentQueryHandler : IRequestHandler<GetListByDynamicAppointmentQuery,
            GetListResponse<GetListByDynamicAppointmentListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetListByDynamicAppointmentQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }


        public async Task<GetListResponse<GetListByDynamicAppointmentListItemDto>> Handle(
            GetListByDynamicAppointmentQuery request,
            CancellationToken cancellationToken)
        {
            Paginate<Domain.Entities.Appointment> appointments = await _appointmentRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery,
                include: a => a.Include(co => co.Consultant)
                    .Include(cl => cl.Client),
                index:
                request.PageRequest.PageIndex,
                size:
                request.PageRequest.PageSize,
                cancellationToken:
                cancellationToken
            );

            var response =
                _mapper.Map<GetListResponse<GetListByDynamicAppointmentListItemDto>>(appointments);

            return response;
        }
    }
}