using System.Diagnostics;
using System.Text.RegularExpressions;
using Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using WebAPI.Models;

namespace WebAPI.Middlewares;

public class ExceptionHandlingMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var detail = GetExceptionDetail(exception);

        _logger.LogError($"\n - path : {httpContext.Request.Path}" +
                         $"\n - class : {detail.Class}" +
                         $"\n - method : {detail.Method}" +
                         $"\n - line number : {detail.LineNumber}" +
                         $"\n - exception : {detail.Message}" +
                         $"\n - inner exception : {detail.InnerExceptionMessage}");

        List<string>? messages = null;

        if (exception is ValidationException validationException)
        {
            messages = [..validationException.Errors.Select(e => e.ErrorMessage)];
        }

        var response = Response.IsFailure(messages);

        httpContext.Response.StatusCode = StatusCodes.Status200OK;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private ExceptionDetailDTO GetExceptionDetail(Exception ex)
    {
        var stackTrace = new StackTrace(ex, true);
        var frame = stackTrace.GetFrame(0);
        var type = frame?.GetMethod()?.DeclaringType;

        var methodName = "-";
        if (!string.IsNullOrWhiteSpace(type?.Name))
        {
            var methodNameRegex = @"<([^>]+)>";
            var match = Regex.Match(type.Name, methodNameRegex);
            if (match.Success) methodName = match.Groups[1].Value;
        }

        ExceptionDetailDTO detail = new()
        {
            Message = ex.Message,
            InnerExceptionMessage =
                string.IsNullOrWhiteSpace(ex.InnerException?.Message)
                    ? "null" : ex.InnerException?.Message,
            Class = type?.DeclaringType?.Name,
            Method = methodName,
            LineNumber = frame?.GetFileLineNumber()
        };

        return detail;
    }
}