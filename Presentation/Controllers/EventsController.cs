using Application.Services.Abstract;
using Application.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Domain.Apis;
using System.Net;
using Application.DTOs.Requests;
using Domain.Models.Entities;
using Domain.Exceptions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _service;
    public EventsController(IEventService service) => _service = service;

    #region Get All

    /// <summary>
    /// Get a list of Event
    /// </summary>
    /// <returns>ApiResponse</returns>

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ApiResponse<IEnumerable<EventResponseDTO>>> GetAll()
    {
        try
        {
            var result = await _service.GetAllAsync();
            var response = result.Select(element => new EventResponseDTO
            {
                Id = element.Id,
                Name = element.Name,
                Description = element.Description,
                StartDate = element.StartDate,
                EndDate = element.EndDate,
                MaxParticipantsCount = element.MaxParticipantsCount,
                CurrentParticipantsCount = element.CurrentParticipantsCount,
                DifficultyLevelId = element.DifficultyLevelId,
                Price = element.Price,
                Location = element.Location,
                CreatedById = element.CreatedById,
                CreatedDate = element.CreatedDate,
                ModifiedDate = element.ModifiedDate,
                DifficultyLevel = element.DifficultyLevel,
                Categories = element.Categories
            });

            var apiResponse = new ApiResponse<IEnumerable<EventResponseDTO>>()
            {
                Data = response,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Data fetched successfully"
            };
            return apiResponse;

        }
        catch (Exception ex)
        {
            var apiResponse = new ApiResponse<IEnumerable<EventResponseDTO>>()
            {
                Data = [],
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }
    }

    #endregion

    #region Get By Id

    /// <summary>
    /// Get a unique Event by Id
    /// </summary>
    /// <returns>ApiResponse</returns>

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ApiResponse<EventResponseDTO>> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);
            var response = new EventResponseDTO
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                MaxParticipantsCount = result.MaxParticipantsCount,
                CurrentParticipantsCount = result.CurrentParticipantsCount,
                DifficultyLevelId = result.DifficultyLevelId,
                Price = result.Price,
                Location = result.Location,
                CreatedById = result.CreatedById,
                CreatedDate = result.CreatedDate,
                ModifiedDate = result.ModifiedDate,
                DifficultyLevel = result.DifficultyLevel,
                Categories = result.Categories
            };

            var apiResponse = new ApiResponse<EventResponseDTO>()
            {
                Data = response,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Data fetched successfully"
            };
            return apiResponse;

        }
        catch (Exception ex)
        {
            var apiResponse = new ApiResponse<EventResponseDTO>()
            {
                Data = null,
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }
    }

    #endregion

    #region Create

    /// <summary>
    /// Create a new instance of Event
    /// </summary>
    /// <param name="request"></param>
    /// <returns>ApiResponse</returns>

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ApiResponse<Guid>> Create(ApiRequest<EventRequestDTO> request)
    {
        try
        {
            var newEvent = new Event()
            {
                Id = Guid.NewGuid(),
                Name = request.Data.Name,
                Description = request.Data.Description,
                StartDate = request.Data.StartDate,
                EndDate = request.Data.EndDate,
                MaxParticipantsCount = request.Data.MaxParticipantsCount,
                CurrentParticipantsCount = request.Data.CurrentParticipantsCount,
                DifficultyLevelId = request.Data.DifficultyLevelId,
                Price = request.Data.Price,
                Location = request.Data.Location,
                CreatedById = request.Data.CreatedById,
                CategoryIds = request.Data.CategoryIds
            };
            var insertedId = await _service.CreateAsync(newEvent);
            var apiResponse = new ApiResponse<Guid>()
            {
                Data = insertedId,
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Message = "New event has been succesfully created with the provided id"

            };
            return apiResponse;

        }
        catch (DatabaseException ex)
        {
            var apiResponse = new ApiResponse<Guid>()
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"Database Exception: {ex.Message}"

            };
            return apiResponse;
        }
        catch (Exception ex)
        {

            var apiResponse = new ApiResponse<Guid>()
            {
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
            return apiResponse;
        }
    }

    #endregion

    #region Update


    /// <summary>
    /// Update an Event by the provided Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns>Apiresponse</returns>
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ApiResponse<Guid>>> Update(Guid id, ApiRequest<EventRequestDTO> request)
    {
        try
        {
			var updatedEvent = new Event()
			{
				Id = id,
				Name = request.Data.Name,
				Description = request.Data.Description,
				StartDate = request.Data.StartDate,
				EndDate = request.Data.EndDate,
				MaxParticipantsCount = request.Data.MaxParticipantsCount,
				CurrentParticipantsCount = request.Data.CurrentParticipantsCount,
				DifficultyLevelId = request.Data.DifficultyLevelId,
				Price = request.Data.Price,
				Location = request.Data.Location,
				CategoryIds = request.Data.CategoryIds
			};
			var updatedId = await _service.UpdateAsync(updatedEvent);
			var apiResponse = new ApiResponse<Guid>()
			{
				Data = updatedId,
				Success = true,
				StatusCode = HttpStatusCode.OK,
				Message = "An event has been succesfully updated with the provided id"

			};
			return apiResponse;
		}
		catch (DatabaseException ex)
		{
			var apiResponse = new ApiResponse<Guid>()
			{
				Success = false,
				StatusCode = HttpStatusCode.BadRequest,
				Message = $"Database Exception: {ex.Message}"

			};
			return apiResponse;
		}
		catch (Exception ex)
		{

			var apiResponse = new ApiResponse<Guid>()
			{
				Success = false,
				StatusCode = HttpStatusCode.InternalServerError,
				Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
			};
			return apiResponse;
		}
	}

    #endregion

    #region Delete 

    /// <summary>
    /// Delete an Event by the provided Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ApiResponse<Guid>> Delete(Guid id)
    {
        try
        {
            var deletedId = await _service.DeleteByIdAsync(id);
            var apiResponse = new ApiResponse<Guid>()
            {
                Data = deletedId,
                Success = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = "An event has been succesfully deleted"

            };
            return apiResponse;
        }
        catch (DatabaseException ex)
        {
            var apiResponse = new ApiResponse<Guid>()
            {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = $"Database Exception: {ex.Message}"

            };
            return apiResponse;
        }
        catch (Exception ex)
        {

            var apiResponse = new ApiResponse<Guid>()
            {
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Error: {ex.InnerException?.Message ?? ex.Message}"
            };
            return apiResponse;
        }
    }

    #endregion
}
