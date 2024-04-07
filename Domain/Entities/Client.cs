using System;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Client : Entity<Guid>
{
    public Client()
    {
        Id = Guid.NewGuid();
    }

    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }

    public virtual ICollection<Review> Reviews { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
}