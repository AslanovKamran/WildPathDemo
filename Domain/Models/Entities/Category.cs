using Domain.Models.Bases;

namespace Domain.Models.Entities;

public class Category : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
