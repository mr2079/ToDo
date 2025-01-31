namespace Domain.Abstractions;

public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}