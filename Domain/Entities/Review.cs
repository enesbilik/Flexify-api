using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Review : Entity<Guid>
{
    public Review()
    {
        Id = Guid.NewGuid();
    }

    public string Comment { get; set; }
    public int Rating { get; set; }


    public Guid ConsultantId { get; set; }
    public Consultant Consultant { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}