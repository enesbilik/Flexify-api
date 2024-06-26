using Domain.Enums;

namespace Application.Features.Appointment.Commands.Create;

public class CreatedAppointmentResponse
{
    public string ClientName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
}