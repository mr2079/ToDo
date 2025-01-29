using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Abstractions;

public abstract class EntityTypeConfiguration<TEntity, TKey> 
    : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase<TKey>
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);

        AppendConfig(builder);
    }

    public abstract void AppendConfig(EntityTypeBuilder<TEntity> builder);
}