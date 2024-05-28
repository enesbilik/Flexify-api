namespace Application.Features.Appointment.Queries.GetConsultantUpcomingAppointmentList;

public class GetConsultantUpcomingAppointmentListItemDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }


    public string ClientName { get; set; }
    public string ClientSurName { get; set; }
}