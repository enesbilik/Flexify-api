namespace Application.Features.Appointment.Queries.GetAppointmentsAvailabilityList;

public class GetAppointmentAvailabilityListItemDto
{
    public string Day { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
}

public class TimeSlot
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int Status { get; set; }
}