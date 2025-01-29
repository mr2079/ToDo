namespace Domain.Abstractions;

public abstract class Entity<TKey> : EntityBase<TKey>, IAuditable, ISoftDeletable
{
    public TKey Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
}