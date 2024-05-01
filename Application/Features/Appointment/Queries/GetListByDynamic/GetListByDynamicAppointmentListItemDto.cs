using Domain.Enums;

namespace Application.Features.Appointment.Queries.GetListByDynamic;

public class GetListByDynamicAppointmentListItemDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string ConsultantName { get; set; }
    public string ClientName { get; set; }
}


