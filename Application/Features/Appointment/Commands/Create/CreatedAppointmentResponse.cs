namespace Application.Features.Appointment.Commands.Create;

public class CreatedAppointmentResponse
{
    public string ConsultantName { get; set; }
    public string ClientName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; }
}