using System.ComponentModel.DataAnnotations;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ConsultantPreferences : Entity<Guid>
{
    public ConsultantPreferences()
    {
        Id = Guid.NewGuid();
    }

    public ConsultantPreferences(Guid consultantId)
    {
        Id = Guid.NewGuid();
        ConsultantId = consultantId;
        StartTime = TimeSpan.FromHours(8);
        EndTime = TimeSpan.FromHours(17);
        AppointmentInterval = 30;
        LunchBreakStartTime = TimeSpan.FromHours(12);
        LunchBreakEndTime = TimeSpan.FromHours(13);
        DaysAheadForAppointment = 7;
        IsWeekendAppointmentAllowed = false;
    }

    public Guid ConsultantId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int AppointmentInterval { get; set; }
    public TimeSpan LunchBreakStartTime { get; set; }
    public TimeSpan LunchBreakEndTime { get; set; }
    public int DaysAheadForAppointment { get; set; }
    public bool IsWeekendAppointmentAllowed { get; set; }
    public virtual Consultant Consultant { get; set; }
}