using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaskConfiguration : EntityTypeConfiguration<TaskEntity, Guid>
{
    public override void AppendConfig(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasQueryFilter(t => !t.IsDeleted);

        builder
            .Property(t => t.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(t => t.Description)
            .HasMaxLength(500)
            .IsRequired();
    }
}