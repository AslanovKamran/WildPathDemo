using Infrastructure.Databases.Abstract;
using Domain.Models.Entities;
using Domain.IRepositories;
using Domain.Exceptions;
using Newtonsoft.Json;
using System.Data;
using Dapper;
using Npgsql;
using System.Data.Common;

namespace Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IDatabase _dbConnection;

    public EventRepository(IDatabase dbConenction) => _dbConnection = dbConenction;

    #region Get All

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        var query = @"SELECT * FROM get_all_events()";

        try
        {
            using (var db = _dbConnection.CreateConnection())
            {
                var results = await db.QueryAsync(query);

                var events = results.Select(row => new Event
                {
                    Id = row.id,
                    Name = row.name,
                    Description = row.description,
                    StartDate = row.start_date,
                    EndDate = row.end_date,
                    MaxParticipantsCount = row.max_participants_count,
                    CurrentParticipantsCount = row.current_participants_count,
                    DifficultyLevelId = row.difficulty_level_id,
                    Price = row.price,
                    Location = row.location,
                    CreatedById = row.created_by_id,
                    CreatedDate = row.created_date,
                    ModifiedDate = row.modified_date,
                    DifficultyLevel = new DifficultyLevel
                    {
                        Id = row.dl_id,
                        Name = row.dl_name,
                        Description = row.dl_description,
                        CreatedDate = row.dl_created_date,
                        ModifiedDate = row.dl_modified_date
                    },
                    Categories = JsonConvert.DeserializeObject<List<Category>>(row.categories) // Map JSON to Categories
                }).ToList();

                return events ?? []; //Return data or empty List
            }
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
        var parameters = new DynamicParameters();
        parameters.Add("p_event_id", id, DbType.Guid, ParameterDirection.Input);

        var query = @"SELECT * FROM get_event_by_id(@p_event_id)";

        try
        {
            using (var db = _dbConnection.CreateConnection())
            {
                var result = await db.QueryAsync(query, parameters);
                var eventItem = result.Select(row => new Event
                {
                    Id = row.id,
                    Name = row.name,
                    Description = row.description,
                    StartDate = row.start_date,
                    EndDate = row.end_date,
                    MaxParticipantsCount = row.max_participants_count,
                    CurrentParticipantsCount = row.current_participants_count,
                    DifficultyLevelId = row.difficulty_level_id,
                    Price = row.price,
                    Location = row.location,
                    CreatedById = row.created_by_id,
                    CreatedDate = row.created_date,
                    ModifiedDate = row.modified_date,
                    DifficultyLevel = new DifficultyLevel
                    {
                        Id = row.dl_id,
                        Name = row.dl_name,
                        Description = row.dl_description,
                        CreatedDate = row.dl_created_date,
                        ModifiedDate = row.dl_modified_date
                    },
                    Categories = JsonConvert.DeserializeObject<List<Category>>(row.categories) // Map JSON to Categories
                }).FirstOrDefault();
                return eventItem ?? throw new RecordNotFoundException($"Event with id {id}");
            }
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



        var query = @"SELECT add_event(
                    @p_event_id, 
                    @p_name,
                    @p_description,
                    @p_starts_at,
                    @p_ends_at,
                    @p_max_participants_count,
                    @p_current_participants_count,
                    @p_difficulty_id,
                    @p_price,
                    @p_location,
                    @p_created_by,
                    @p_category_ids
                 )";

        var parameters = new DynamicParameters();
        parameters.Add("p_event_id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);
        parameters.Add("p_starts_at", entity.StartDate, DbType.DateTime2, ParameterDirection.Input); // Pass as is
        parameters.Add("p_ends_at", entity.EndDate, DbType.DateTime2, ParameterDirection.Input);     // Pass as is
        parameters.Add("p_max_participants_count", entity.MaxParticipantsCount, DbType.Int32, ParameterDirection.Input);
        parameters.Add("p_current_participants_count", entity.CurrentParticipantsCount, DbType.Int32, ParameterDirection.Input);
        parameters.Add("p_difficulty_id", entity.DifficultyLevelId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("p_price", entity.Price, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("p_location", entity.Location, DbType.String, ParameterDirection.Input);
        parameters.Add("p_created_by", entity.CreatedById, DbType.Guid, ParameterDirection.Input);
        parameters.Add("p_category_ids", entity.CategoryIds, DbType.String, ParameterDirection.Input);


        try
        {
            using (var db = _dbConnection.CreateConnection())
            {
                var eventId = await db.ExecuteScalarAsync<Guid>(query, parameters);
                return eventId;
            }
        }


        catch (PostgresException ex)
        {
            throw new DatabaseException("Database error occurred: " + ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the event.", ex);
        }

    }

	#endregion

	#region Update

	public async Task<Guid> UpdateAsync(Event entity)
	{
		var query = @"SELECT update_event(
                        @p_event_id, 
                        @p_name,
                        @p_description,
                        @p_starts_at,
                        @p_ends_at,
                        @p_max_participants_count,
                        @p_current_participants_count,
                        @p_difficulty_id,
                        @p_price,
                        @p_location,
                        @p_category_ids
                     )";

		var parameters = new DynamicParameters();
		parameters.Add("p_event_id", entity.Id, DbType.Guid, ParameterDirection.Input);
		parameters.Add("p_name", entity.Name, DbType.String, ParameterDirection.Input);
		parameters.Add("p_description", entity.Description, DbType.String, ParameterDirection.Input);
		parameters.Add("p_starts_at", entity.StartDate, DbType.DateTime2, ParameterDirection.Input);
		parameters.Add("p_ends_at", entity.EndDate, DbType.DateTime2, ParameterDirection.Input);
		parameters.Add("p_max_participants_count", entity.MaxParticipantsCount, DbType.Int32, ParameterDirection.Input);
		parameters.Add("p_current_participants_count", entity.CurrentParticipantsCount, DbType.Int32, ParameterDirection.Input);
		parameters.Add("p_difficulty_id", entity.DifficultyLevelId, DbType.Guid, ParameterDirection.Input);
		parameters.Add("p_price", entity.Price, DbType.Decimal, ParameterDirection.Input);
		parameters.Add("p_location", entity.Location, DbType.String, ParameterDirection.Input);
		parameters.Add("p_category_ids", entity.CategoryIds, DbType.String, ParameterDirection.Input);

		try
		{
			using (var db = _dbConnection.CreateConnection())
			{
				var updatedId = await db.ExecuteScalarAsync<Guid>(query, parameters);
				return updatedId;
			}
		}
		catch (PostgresException ex)
		{
			throw new DatabaseException("Database error occurred: " + ex.Message, ex);
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while updating the event.", ex);
		}
	}

    #endregion

    #region Delete

    public async Task DeleteByIdAsync(Guid id)
    {
        var query = @"SELECT delete_event(@p_event_id)";

        var parameters = new DynamicParameters();
        parameters.Add("p_event_id", id, DbType.Guid, ParameterDirection.Input);

        try
        {
            using (var db = _dbConnection.CreateConnection())
            {
                await db.ExecuteAsync(query, parameters);
            }
        }
        catch (PostgresException ex)
        {
            throw new DatabaseException($"Database error occurred: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the event.", ex);
        }
    }

    #endregion


}
