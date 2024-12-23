

namespace Application.DTOs.Requests;

public class EventRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipantsCount { get; set; }
    public int CurrentParticipantsCount { get; set; } = 0;
    public Guid DifficultyLevelId { get; set; } // Foreign key to DifficultyLevels
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public Guid CreatedById { get; set; } // Foreign key to Users
    public string CategoryIds { get; set; } = string.Empty;
}
