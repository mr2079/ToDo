using DNTPersianUtils.Core;

namespace Application.Models;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Status => IsCompleted ? "تکمیل شده" : "تکمیل نشده";
    public DateTime DueDate { get; set; }
    public string ShamsiDueDate => DueDate.ToLongPersianDateTimeString();
}