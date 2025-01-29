using Domain.Entities.Abstractions;

namespace Domain.Entities;

public class Task : EntityBase<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
}