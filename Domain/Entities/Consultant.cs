using System;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Consultant : Entity<Guid>
{
    public Consultant()
    {
        Id = Guid.NewGuid();
    }

    public string Name { get; set; }

    public string SurName { get; set; }

    public string Email { get; set; }

    public string PhotoUrl { get; set; }

    public string About { get; set; }

    public string Location { get; set; }

    public int Experience { get; set; }

    public string Title { get; set; }

    public double Rating { get; set; } // Rating of the consultant

    public int ServiceCount { get; set; } // Number of services provided

    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
    public virtual ConsultantPreferences ConsultantPreferences { get; set; }
}