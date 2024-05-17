using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Appointment.Rules;

public class AppointmentRules : BaseBusinessRules
{
    public Task CanNotBeDeletePastAppointment(Domain.Entities.Appointment appointment)
    {
        if (appointment.StartTime < DateTime.Now)
            throw new BusinessException("Can not be delete past appointment");
        return Task.CompletedTask;
    }

    public Task NotFoundAppointment(Domain.Entities.Appointment? appointment)
    {
        if (appointment == null)
            throw new BusinessException("Appointment not found");
        return Task.CompletedTask;
    }
}