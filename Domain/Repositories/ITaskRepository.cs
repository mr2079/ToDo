using Domain.Repositories.Base;

namespace Domain.Repositories;

public interface ITaskRepository : IRepository<TaskEntity, Guid>;