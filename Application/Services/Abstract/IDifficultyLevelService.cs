using Domain.Models.Entities;

namespace Application.Services.Abstract;

public interface IDifficultyLevelService
{
    Task<IEnumerable<DifficultyLevel>> GetAllAsync();
    Task<DifficultyLevel> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(DifficultyLevel difficultyLevel);

    Task<Guid> UpdateAsync(DifficultyLevel difficultyLevel);
    Task DeleteByIdAsync(Guid id);
}
