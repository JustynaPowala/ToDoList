using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.WebApi.Models;

namespace ToDoList.WebApi.EF.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Date).IsRequired();
            builder.Property(t => t.Content).HasMaxLength(255).IsRequired(false);
            builder.Property(t => t.CreatedDate).IsRequired();
            builder
           .Property(t => t.Status)
           .HasConversion(x => x.ToString(), x => Enum.Parse<TaskItemStatus>(x, true))
           .IsRequired();
        }
    }
}
