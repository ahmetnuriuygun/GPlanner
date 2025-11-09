using GPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
namespace GPlanner.Data
{
    public class GPlannerDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }
        public GPlannerDbContext(DbContextOptions<GPlannerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);
                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();

                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Priority).IsRequired();
                entity.Property(e => e.IsArchived).IsRequired().HasDefaultValue(false);
                entity.ToTable("UserTasks");
            });

            modelBuilder.Entity<UserTask>().HasData(
                new UserTask
                {
                    TaskId = 1,
                    UserId = 1,
                    Title = "Prepare Project Defense Slides",
                    Description = "Create slides for MAUI and ASP.NET API demonstration.",
                    Type = TaskType.AFSPRAAK,
                    Date = new DateTime(2025, 12, 10),
                    Priority = 5,
                    IsArchived = false
                },
                new UserTask
                {
                    TaskId = 2,
                    UserId = 1,
                    Title = "Integrate API calls into ViewModels",
                    Description = "Replace mock data with actual HTTP GET requests to the API.",
                    Type = TaskType.GROTETOETS,
                    Date = new DateTime(2025, 12, 12),
                    Priority = 4,
                    IsArchived = false
                },
                new UserTask
                {
                    TaskId = 3,
                    UserId = 2,
                    Title = "Book flight tickets",
                    Description = "Check prices for holiday travel.",
                    Type = TaskType.HUISWERK,
                    Date = new DateTime(2025, 12, 20),
                    Priority = 3,
                    IsArchived = false
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}