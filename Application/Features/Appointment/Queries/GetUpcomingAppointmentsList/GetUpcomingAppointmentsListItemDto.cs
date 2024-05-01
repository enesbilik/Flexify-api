namespace Application.Features.Appointment.Queries.GetUpcomingAppointmentsList;

public class GetUpcomingAppointmentsListItemDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }


    public string ConsultantName { get; set; }
    public string ConsultantSurName { get; set; }
    public string ConsultantTitle { get; set; }
    public string ConsultantPhotoUrl { get; set; }
}