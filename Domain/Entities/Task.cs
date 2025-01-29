using Domain.Abstractions;

namespace Domain.Entities;

public class Task : Entity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
}