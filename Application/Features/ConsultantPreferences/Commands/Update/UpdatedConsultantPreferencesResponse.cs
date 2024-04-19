namespace Application.Features.ConsultantPreferences.Commands.Update;

public class UpdatedConsultantPreferencesResponse
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int AppointmentInterval { get; set; }
    public TimeSpan LunchBreakStartTime { get; set; }
    public TimeSpan LunchBreakEndTime { get; set; }
    public int DaysAheadForAppointment { get; set; }
    public bool IsWeekendAppointmentAllowed { get; set; }
}