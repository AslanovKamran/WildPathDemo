using Domain.Models.Entities;
using System.Text.Json.Serialization;

namespace Application.DTOs.Responses;

public class EventResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipantsCount { get; set; }
    public int CurrentParticipantsCount { get; set; } = 0;
    public Guid DifficultyLevelId { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public Guid CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; } = new DifficultyLevel();
    public IEnumerable<Category> Categories { get; set; } = [];

    // Temporary property for category IDs
    [JsonIgnore]
    public string CategoryIds { get; set; } = string.Empty;
}
