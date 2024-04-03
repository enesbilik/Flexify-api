using System;
namespace Application.Features.Auth.Commands.Register;

public class RegisteredCommandResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime CreatedDate { get; set; }

}

