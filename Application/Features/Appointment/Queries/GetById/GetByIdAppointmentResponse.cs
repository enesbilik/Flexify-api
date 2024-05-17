using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Appointment.Queries.GetById;

public class GetByIdAppointmentResponse
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string ConsultantName { get; set; }
    public string ConsultantSurName { get; set; }
    public string ConsultantLocation { get; set; }
}