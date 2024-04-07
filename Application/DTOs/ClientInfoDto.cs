using Application.Enums;

namespace Application.DTOs;

public record ClientInfoDto(
    string Name,
    string SurName
)
{
    public ClientInfoDto() : this(string.Empty, string.Empty)
    {
    }
}