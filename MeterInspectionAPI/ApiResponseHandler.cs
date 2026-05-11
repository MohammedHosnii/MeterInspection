using System.Collections.Generic;
using System.Net;

namespace MeterInspectionApi;


public class ApiResponseHandler
{
    // Constructor kept protected for inheritance
    public ApiResponseHandler() { }

    // Reusable private method for error responses
    private ApiResponse<T> CreateErrorResponse<T>(HttpStatusCode statusCode, string? message, List<string>? errors = null) where T : class
    {
        return ApiResponse<T>.Error(statusCode, message,errors);
    }

    // Error responses
    public ApiResponse<T> BadRequest<T>(string? message = null, List<string>? errors = null) where T : class
    {
        return CreateErrorResponse<T>(HttpStatusCode.BadRequest, message,errors);
    }

    public ApiResponse<T> Unauthorized<T>(string? message = null) where T : class
    {
        return CreateErrorResponse<T>(HttpStatusCode.Unauthorized, message);
    }

    public ApiResponse<T> NotFound<T>(string? message = null) where T : class
    {
        return CreateErrorResponse<T>(HttpStatusCode.NotFound, message);
    }

    public ApiResponse<T> UnprocessableEntity<T>(string? message = null) where T : class
    {
        return CreateErrorResponse<T>(HttpStatusCode.UnprocessableEntity, message);
    }

    // Success responses
    public ApiResponse<T> Success<T>(T entity, string? message = null, Dictionary<string, object>? meta = null) where T : class
    {
        return ApiResponse<T>.Success(entity, message, meta);
    }

    public ApiResponse<T> Created<T>(T entity, Dictionary<string, object>? meta = null) where T : class
    {
        return ApiResponse<T>.Created(entity, meta);
    }

    public ApiResponse<T> Deleted<T>(string? message = null) where T : class
    {
        return ApiResponse<T>.Deleted(message);
    }
}