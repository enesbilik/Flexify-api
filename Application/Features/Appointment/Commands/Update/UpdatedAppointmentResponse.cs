using Domain.Enums;

namespace Application.Features.Appointment.Commands.Update;

public class UpdatedAppointmentResponse
{
    public string ClientName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
}