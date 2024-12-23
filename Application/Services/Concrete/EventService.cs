using Application.Services.Abstract;
using Domain.Models.Entities;
using Domain.IRepositories;

namespace Application.Services.Concrete;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository) => _repository = repository;

    #region Get All

    public async Task<IEnumerable<Event>> GetAllAsync()
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

    public async Task<Event> GetByIdAsync(Guid id)
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

    #region Create
    public async Task<Guid> CreateAsync(Event entity)
    {
        try
        {
            var response = await _repository.CreateAsync(entity);
            return response;

        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Update

    public async Task<Guid> UpdateAsync(Event entity)
    {
        try
        {
            var response = await _repository.UpdateAsync(entity);
            return response;

        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Delete 

    public async Task<Guid> DeleteByIdAsync(Guid id)
    {
        try
        {
            await _repository.DeleteByIdAsync(id);
            return id;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

}
