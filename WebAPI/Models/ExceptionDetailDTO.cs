namespace WebAPI.Models;

public class ExceptionDetailDTO
{
    public string? Class { get; set; }
    public string? Method { get; set; }
    public int? LineNumber { get; set; }
    public string? Message { get; set; }
    public string? InnerExceptionMessage { get; set; }
}