using Application.Services.Abstract;
using Domain.IRepositories;
using Domain.Models.Entities;

namespace Application.Services.Concrete;

public class DifficultyLevelService : IDifficultyLevelService
{
    private readonly IDifficultyLevelRepository _repository;

    public DifficultyLevelService(IDifficultyLevelRepository difficultyLevelRepository) => _repository = difficultyLevelRepository;

    #region Get All
    public async Task<IEnumerable<DifficultyLevel>> GetAllAsync()
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
    public async Task<DifficultyLevel> GetByIdAsync(Guid id)
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

    public async Task<Guid> CreateAsync(DifficultyLevel difficultyLevel)
    {
        try
        {
            var response = await _repository.CreateAsync(difficultyLevel);
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Update

    public async Task<Guid> UpdateAsync(DifficultyLevel difficultyLevel)
    {
        try
        {
            var response = await _repository.UpdateAsync(difficultyLevel);
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Delete

    public Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    #endregion
   

  
  
}
