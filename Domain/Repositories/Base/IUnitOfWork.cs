namespace Domain.Repositories.Base;

public interface IUnitOfWork : IDisposable
{
    ITaskRepository TaskRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}