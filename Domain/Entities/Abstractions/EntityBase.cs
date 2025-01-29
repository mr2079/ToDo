namespace Domain.Entities.Abstractions;

public abstract class EntityBase<TKey> : IEntity<TKey>, IAuditable, ISoftDeletable
{
    public TKey Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
}