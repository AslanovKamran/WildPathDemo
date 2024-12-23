using Infrastructure.Databases.Abstract;
using Domain.Models.Entities;
using Domain.IRepositories;
using Domain.Exceptions;
using System.Data;
using Dapper;
using Npgsql;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly IDatabase _dbConnection;
	public CategoryRepository(IDatabase dbConnection) => _dbConnection = dbConnection;

	#region Get All

	public async Task<IEnumerable<Category>> GetAllAsync()
	{
		var query = @"SELECT * FROM get_all_categories()";
		try
		{
			using (var db = _dbConnection.CreateConnection())
			{
				var result = await db.QueryAsync<Category>(query);
				return result?.ToList() ?? []; //Return data or empty List
			}
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
		var parameters = new DynamicParameters();
		parameters.Add("p_id", id, DbType.Guid, ParameterDirection.Input);

		var query = @"SELECT * FROM get_category_by_id(@p_id)";

		try
		{
			using (var db = _dbConnection.CreateConnection())
			{
				var result = await db.QueryFirstOrDefaultAsync<Category>(query, parameters);
				return result ?? throw new RecordNotFoundException($"Category with id = {id}");
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	#endregion

	#region Add

	public async Task<Guid> CreateAsync(Category entity)
	{
		var parameters = new DynamicParameters();
		parameters.Add("p_id", entity.Id, DbType.Guid, ParameterDirection.Input);
		parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
		parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);

		var query = @"SELECT insert_category (@p_id, @p_name, @p_description)";

		try
		{
			using (var db = _dbConnection.CreateConnection())
			{
				await db.ExecuteAsync(query, parameters);
				return entity.Id;
			}
		}
		catch (PostgresException ex) when (ex.SqlState == "23505")
		{
			// Handle unique constraint violation
			throw new DatabaseException($"The name '{entity.Name}' already exists. Names must be unique.", ex);
		}
		catch (Exception ex)
		{
			// Handle other exceptions
			throw new Exception($"An error occurred while inserting the category: {ex.Message}", ex);
		}
	}


	#endregion

	#region Update


	public async Task<Guid> UpdateAsync(Category entity)
	{
		var parameters = new DynamicParameters();

		parameters.Add("p_id", entity.Id, DbType.Guid, ParameterDirection.Input);
		parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
		parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);

		var query = @"SELECT update_category(@p_id, @p_name, @p_description)";
		try
		{
			using (var db = _dbConnection.CreateConnection())
			{
				await db.ExecuteAsync(query, parameters);
				return entity.Id;
			}
		}
		catch (PostgresException ex) when (ex.SqlState == "P0001")
		{
			// Handle non-existent ID
			throw new DatabaseException($"No category found with id '{entity.Id}'.", ex);
		}
		catch (PostgresException ex) when (ex.SqlState == "23505")
		{
			// Handle unique constraint violation
			throw new DatabaseException($"The name '{entity.Name}' is already in use by another category.", ex);
		}
		catch (Exception ex)
		{
			// Handle other exceptions
			throw new Exception("An error occurred while updating the category.", ex);
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
