using System;
using System.Net.NetworkInformation;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class AppUser : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public AppUser()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PasswordHash = Array.Empty<byte>();
        PasswordSalt = Array.Empty<byte>();
    }

    public AppUser(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }

    public AppUser(Guid id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }
}

