using Application.Features.Appointment.Rules;
using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Appointment.Commands.Delete;

public class DeleteAppointmentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }


    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly AppointmentRules _appointmentRules;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository, AppointmentRules appointmentRules)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentRules = appointmentRules;
        }

        public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

            await _appointmentRules.NotFoundAppointment(appointment);
            await _appointmentRules.CanNotBeDeletePastAppointment(appointment!);
            
            appointment!.Status = Domain.Enums.AppointmentStatus.Empty;
            await _appointmentRepository.UpdateAsync(appointment);
            await _appointmentRepository.DeleteAsync(appointment);

            return Unit.Value;
        }
    }
}