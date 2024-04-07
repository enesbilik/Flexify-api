using Application.Enums;

namespace Application.DTOs;

public record ConsultantInfoDto(
    string Name,
    string SurName,
    string PhotoUrl,
    string About,
    string Location,
    int Experience,
    string Title
)
{
    public ConsultantInfoDto() : this("", "", "", "", "", 0, "")
    {
    }
}