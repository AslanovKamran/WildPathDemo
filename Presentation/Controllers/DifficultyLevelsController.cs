﻿using Application.Services.Abstract;
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
public class DifficultyLevelsController : ControllerBase
{
    private readonly IDifficultyLevelService _service;
    public DifficultyLevelsController(IDifficultyLevelService service) => _service = service;

    #region Get All

    /// <summary>
    /// Get a list of Difficulty Levels
    /// </summary>
    /// <returns>ApiResponse</returns>

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ApiResponse<IEnumerable<DifficultyLevelResponseDTO>>>> GetAll()
    {

        try
        {
            var result = await _service.GetAllAsync();
            var response = new List<DifficultyLevelResponseDTO>();

            //Manual Mapping
            foreach (var element in result)
            {
                var resultDto = new DifficultyLevelResponseDTO
                {
                    Id = element.Id,
                    Name = element.Name,
                    Description = element.Description,
                    CreatedDate = element.CreatedDate,
                    ModifiedDate = element.ModifiedDate
                };
                response.Add(resultDto);
            }

            var apiResponse = new ApiResponse<IEnumerable<DifficultyLevelResponseDTO>>()
            {
                Data = response,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Data fetched Successfully"
            };

            return apiResponse;
        }
        catch (Exception ex)
        {

            var apiResponse = new ApiResponse<IEnumerable<DifficultyLevelResponseDTO>>()
            {
                Data = [],
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }

    }

    #endregion

    #region Get By Id

    /// <summary>
    /// Get a unique Difficulty Level by Id
    /// </summary>
    /// <returns>ApiResponse</returns>

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ApiResponse<DifficultyLevelResponseDTO>>> GetById(Guid id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);

            //Manual mapping
            var response = new DifficultyLevelResponseDTO
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedDate = result.CreatedDate,
                ModifiedDate = result.ModifiedDate
            };

            var apiResponse = new ApiResponse<DifficultyLevelResponseDTO>()
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
            var apiResponse = new ApiResponse<DifficultyLevelResponseDTO>()
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

            var apiResponse = new ApiResponse<DifficultyLevelResponseDTO>()
            {
                Data = null,
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = $"Error: {ex.Message}"
            };
            return apiResponse;
        }
    }

    #endregion

    #region Create

    /// <summary>
    /// Insert a new Difficulty Level into the database (Names must be unique)
    /// </summary>
    /// <param name="request"></param>
    /// <returns>ApiResponse</returns>
    /// 

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ApiResponse<Guid>>> Create(ApiRequest<DifficultyLevelRequestDTO> request)
    {
        try
        {
            var difficultyLevel = new DifficultyLevel()
            {
                //May be Unique Id must be generated by the Service layer, not the Presentation one?
                Id = Guid.NewGuid(),
                Name = request.Data.Name,
                Description = request.Data.Description
            };

            var insertedId = await _service.CreateAsync(difficultyLevel);

            var apiResponse = new ApiResponse<Guid>()
            {
                Data = insertedId,
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Message = "Difficulty level has been succesfully created with the provided id"

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
    /// Update Difficulty Level 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns>Api Response</returns>
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ApiResponse<Guid>>> Update(Guid id, ApiRequest<DifficultyLevelRequestDTO> request)
    {
        try
        {

            var difficultyLevel = new DifficultyLevel()
            {
                Id = id,
                Name = request.Data.Name,
                Description = request.Data.Description
            };

            var updatedId = await _service.UpdateAsync(difficultyLevel);

            var apiResponse = new ApiResponse<Guid>()
            {
                Data = updatedId,
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Difficulty level has been successfully updated at the provided Id"

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
