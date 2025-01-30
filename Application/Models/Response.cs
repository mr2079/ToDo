namespace Application.Models;

public record ApiResponse(
    bool Success,
    IEnumerable<string>? Messages);

public record ApiResponse<TData>(
    TData? Data,
    bool Success,
    IEnumerable<string>? Messages)
{
    public static implicit operator ApiResponse<TData>(ApiResponse response)
        => new(default, response.Success, response.Messages);
}

public static class Response
{
    private const string SuccessDefaultMessage = "عملیات با موفقیت انجام شد";
    private const string FailureDefaultMessage = "خطای ناشناخته ای رخ داده است";

    public static ApiResponse IsSuccess(
        IEnumerable<string>? messages = null)
    {
        messages ??= new List<string>
        {
            SuccessDefaultMessage
        };

        return new(
            Success: true,
            Messages: messages);
    }

    public static ApiResponse<TData> IsSuccess<TData>(
        TData? data,
        IEnumerable<string>? messages = null)
    {
        messages ??= new List<string>
        {
            SuccessDefaultMessage
        };

        return new(
            Data: data,
            Success: true,
            Messages: messages);
    }

    public static ApiResponse IsFailure(
        IEnumerable<string>? messages = null)
    {
        messages ??= new List<string>
        {
            FailureDefaultMessage
        };

        return new(
            Success: false,
            Messages: messages);
    }
}