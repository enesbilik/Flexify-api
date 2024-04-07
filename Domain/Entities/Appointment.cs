using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Appointment : Entity<Guid>

{
    public Appointment()
    {
        Id = Guid.NewGuid();
    }
    
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
    
    public Guid ConsultantId { get; set; }
    public Consultant Consultant { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}