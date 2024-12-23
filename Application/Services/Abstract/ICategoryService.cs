using Domain.Models.Entities;

namespace Application.Services.Abstract;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(Category category);
    Task<Guid> UpdateAsync(Category category);

}
