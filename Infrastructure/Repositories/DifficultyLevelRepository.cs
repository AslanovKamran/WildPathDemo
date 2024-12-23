using Infrastructure.Databases.Abstract;
using Domain.Models.Entities;
using Domain.IRepositories;
using Domain.Exceptions;
using System.Data;
using Dapper;
using Npgsql;


namespace Infrastructure.Repositories;

public class DifficultyLevelRepository : IDifficultyLevelRepository
{

    private readonly IDatabase _dbConnection;
    public DifficultyLevelRepository(IDatabase dbConnection) => _dbConnection = dbConnection;

    #region Get All

    public async Task<IEnumerable<DifficultyLevel>> GetAllAsync()
    {
        var query = @"SELECT * FROM get_all_difficulty_levels()";
        try
        {
            using (var db = _dbConnection.CreateConnection())
            {

				var result = await db.QueryAsync<DifficultyLevel>(query);
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
    public async Task<DifficultyLevel> GetByIdAsync(Guid id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_id", id, DbType.Guid, ParameterDirection.Input);

        var query = @"SELECT * FROM get_difficulty_level_by_id(@p_id)";
        try
        {
            using (var db = _dbConnection.CreateConnection())
            {
                var result = await db.QueryFirstOrDefaultAsync<DifficultyLevel>(query, parameters);
                return result ?? throw new RecordNotFoundException($"Difficulty Level with id = {id}");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Add 

    public async Task<Guid> CreateAsync(DifficultyLevel entity)
    {
        var parameters = new DynamicParameters();

        parameters.Add("p_id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);

        var query = @"SELECT insert_difficulty_level(@p_id, @p_name, @p_description)";

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
            throw new Exception($"An error occurred while inserting the difficulty level: {ex.Message}", ex);
        }
    }

    #endregion

    #region Update

    public async Task<Guid> UpdateAsync(DifficultyLevel entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);

        var query = @"SELECT update_difficulty_level(@p_id, @p_name, @p_description)";

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
            throw new DatabaseException($"No difficulty level found with id '{entity.Id}'.", ex);
        }
        catch (PostgresException ex) when (ex.SqlState == "23505")
        {
            // Handle unique constraint violation
            throw new DatabaseException($"The name '{entity.Name}' is already in use by another category.", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new Exception("An error occurred while updating the difficulty level.", ex);
        }
    }

    #endregion


    //There is no need to delete any of the difficulty levels. 
    #region Delete

    public Task DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    #endregion
}
