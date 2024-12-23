using Domain.Models.Bases;
using System.Text.Json.Serialization;

namespace Domain.Models.Entities;

public class DifficultyLevel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
  
}
