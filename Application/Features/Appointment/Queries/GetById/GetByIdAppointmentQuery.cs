using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetById;

public class GetByIdAppointmentQuery : IRequest<GetByIdAppointmentResponse>
{
    public Guid Id { get; set; }

    public class GetByIdAppointmentQueryHandler : IRequestHandler<GetByIdAppointmentQuery, GetByIdAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetByIdAppointmentQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<GetByIdAppointmentResponse> Handle(GetByIdAppointmentQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(
                predicate: a => a.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken,
                include: a => a.Include(a => a.Consultant)
            );

            var response = _mapper.Map<GetByIdAppointmentResponse>(appointment);
            return response;
        }
    }
}