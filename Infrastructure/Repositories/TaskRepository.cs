using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class TaskRepository : Repository<TaskEntity, Guid>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }
}