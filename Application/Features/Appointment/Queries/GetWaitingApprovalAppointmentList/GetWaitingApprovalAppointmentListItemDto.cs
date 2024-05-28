namespace Application.Features.Appointment.Queries.GetWaitingApprovalAppointmentList;

public class GetWaitingApprovalAppointmentListItemDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }


    public string ClientName { get; set; }
    public string ClientSurName { get; set; }
}
