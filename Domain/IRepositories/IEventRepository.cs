using Domain.IRepositories.Bases;
using Domain.Models.Entities;

namespace Domain.IRepositories;

public interface IEventRepository :ICRUDRepository<Event>
{
	// Future methods:
	// Task<IEnumerable<Event>> GetEventsByCategoryAsync(Guid categoryId);
	// Task<IEnumerable<Event>> GetUpcomingEventsAsync(DateTime currentDate);
}
