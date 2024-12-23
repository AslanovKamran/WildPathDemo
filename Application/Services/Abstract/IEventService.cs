using Domain.Models.Entities;

namespace Application.Services.Abstract;

public interface IEventService
{
	Task<IEnumerable<Event>> GetAllAsync();
	Task<Event> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(Event entity);
	Task<Guid> UpdateAsync(Event entity);
	Task<Guid> DeleteByIdAsync(Guid id);
}
