using Microsoft.EntityFrameworkCore;
using ToDoList.WebApi.EF.Configurations;
using ToDoList.WebApi.Models;

namespace ToDoList.WebApi.EF
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }
        public DbSet<TaskItem> TaskItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("toDoList");
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
