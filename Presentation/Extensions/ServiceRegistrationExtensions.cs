using Application.Services.Abstract;
using Application.Services.Concrete;
using Infrastructure.Repositories;
using Domain.IRepositories;

namespace Presentation.Extensions;

public static class ServiceRegistrationExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDifficultyLevelRepository, DifficultyLevelRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IEventRepository, EventRepository>();

		//...other repositories may be added here
		return services;
	}

	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IDifficultyLevelService, DifficultyLevelService>();
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IEventService, EventService>();

		//...other services may be added here
		return services;
	}
}
