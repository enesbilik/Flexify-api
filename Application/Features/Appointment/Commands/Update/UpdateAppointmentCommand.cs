using Application.Services.Repositories;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Appointment.Commands.Update;

public class UpdateAppointmentCommand : IRequest<UpdatedAppointmentResponse>
{
    public Guid Id { get; set; }
    public AppointmentStatus Status { get; set; }

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, UpdatedAppointmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<UpdatedAppointmentResponse> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
            appointment.Status = request.Status;

            await _appointmentRepository.UpdateAsync(appointment);

            var updatedAppointmentResponse = _mapper.Map<UpdatedAppointmentResponse>(appointment);

            return updatedAppointmentResponse;
        }
    }
}