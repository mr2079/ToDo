using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Task;

namespace Infrastructure.Configurations;

public class TaskConfiguration : EntityTypeConfiguration<Task, Guid>
{
    public override void AppendConfig(EntityTypeBuilder<Task> builder)
    {
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