namespace Application.Features.Consultant.Queries.GetById;

public class GetByIdConsultantResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string SurName { get; set; }

    public string Email { get; set; }

    public string PhotoUrl { get; set; }

    public string About { get; set; }

    public string Location { get; set; }

    public int Experience { get; set; }

    public string Title { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}