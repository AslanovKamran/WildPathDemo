using Application.Services.Abstract;
using Application.DTOs.Responses;
using Application.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Entities;
using Domain.Exceptions;
using Domain.Apis;
using System.Net;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service) => _service = service;

    #region Get All

    /// <summary>
    /// Get a list of Categroies
    /// </summary>
    /// <returns>ApiResponse</returns>

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ApiResponse<IEnumerable<CategoryResponseDTO>>> GetAll()
    {
        try
        {
            var result = await _service.GetAllAsync();
            var response = new List<CategoryResponseDTO>();

            foreach (var element in result)
            {
                var resultDto = new CategoryResponseDTO()
                {
                    Id = element.Id,
                    Name = element.Name,
                    Description = element.Description,
                    CreatedDate = element.CreatedDate,
                    ModifiedDate = element.ModifiedDate,
                };
                response.Add(resultDto);
            }

            var apiResponse = new ApiResponse<IEnumerable<CategoryResponseDTO>>()
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

            var apiResponse = new ApiResponse<IEnumerable<CategoryResponseDTO>>()
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
    /// Get a unique Category by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ApiResponse</returns>

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ApiResponse<CategoryResponseDTO>> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);
            var response = new CategoryResponseDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedDate = result.CreatedDate,
                ModifiedDate = result.ModifiedDate
            };

            var apiResponse = new ApiResponse<CategoryResponseDTO>()
            {
                Data = response,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Data fetched successfully"
            };

            return apiResponse;

        }
        catch (RecordNotFoundException ex)
        {
            var apiResponse = new ApiResponse<CategoryResponseDTO>()
            {
                Data = null,
                Success = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = $"Not Found Error: {ex.Message}"
            };
            return apiResponse;
        }
        catch (Exception ex)
        {

            var apiResponse = new ApiResponse<CategoryResponseDTO>()
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
    /// Insert a new Category into the database (Names must be unique)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ApiResponse<Guid>> Create(ApiRequest<CategoryRequestDTO> request)
    {
        try
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Data.Name,
                Description = request.Data.Description
            };
            var insertedId = await _service.CreateAsync(category);

            var apiResponse = new ApiResponse<Guid>()
            {
                Data = insertedId,
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Message = "Category has been succesfully created with the provided id"

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
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }

    }
    #endregion

    #region Update
    /// <summary>
    /// Update Category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns>Api response</returns>
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ApiResponse<Guid>> Update(Guid id, ApiRequest<CategoryRequestDTO> request) 
    {
        try
        {
            var category = new Category()
            {
                Id = id,
                Name = request.Data.Name,
                Description = request.Data.Description
            };

            var updatedId = await _service.UpdateAsync(category);

            var apiResponse = new ApiResponse<Guid>()
            {
                Data = updatedId,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Category has been successfully updated at the provided Id"

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
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }
        
    }

    #endregion

}
