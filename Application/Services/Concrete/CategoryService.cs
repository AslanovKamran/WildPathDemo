using Application.Services.Abstract;
using Domain.IRepositories;
using Domain.Models.Entities;

namespace Application.Services.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository) => _repository = repository;

    #region Get All

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        try
        {
            var response = await _repository.GetAllAsync();
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Get By Id

    public async Task<Category> GetByIdAsync(Guid id)
    {
        try
        {
            var response = await _repository.GetByIdAsync(id);
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Add

    public async Task<Guid> CreateAsync(Category category)
    {
        try
        {
            var response = await _repository.CreateAsync(category);
            return response;
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

    #region Update

    public async Task<Guid> UpdateAsync(Category category)
    {
        try
        {
            var response = await _repository.UpdateAsync(category);
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion
}
