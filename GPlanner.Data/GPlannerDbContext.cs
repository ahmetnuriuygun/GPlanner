using GPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
namespace GPlanner.Data
{
    public class GPlannerDbContext : DbContext
    {
        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ScheduledTask> ScheduledTasks { get; set; }

        public DbSet<DailyPlanItem> DailyPlanItems { get; set; }
        public GPlannerDbContext(DbContextOptions<GPlannerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SchoolName).HasMaxLength(150);
                entity.Property(e => e.BirthDate).HasMaxLength(20);
                entity.Property(e => e.IsNotified).IsRequired().HasDefaultValue(false);

                entity.HasMany(u => u.UserTasks)
                .WithOne()
                     .HasForeignKey(ut => ut.UserId)
                     .IsRequired();

                entity.ToTable("Users");
            });

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

            modelBuilder.Entity<DailyPlanItem>(entity =>
            {
                entity.HasKey(e => e.Date);
                entity.Property(e => e.DayOfWeek).IsRequired().HasMaxLength(20);
                entity.Property(e => e.LastGenerated).IsRequired();
                entity.HasMany(dpi => dpi.Tasks)
                                      .WithOne(st => st.DailyPlanItem)
                                      .HasForeignKey(st => st.DailyPlanDate)
                                      .OnDelete(DeleteBehavior.Cascade)
                                      .IsRequired();
                entity.ToTable("DaillyPlanItems");
            });


            modelBuilder.Entity<ScheduledTask>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Id).ValueGeneratedOnAdd();

               entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
               entity.Property(e => e.ActivityType).IsRequired().HasMaxLength(100);
               entity.Property(e => e.StartTime).IsRequired();
               entity.Property(e => e.EndTime).IsRequired();
               entity.Property(e => e.IsCompleted).IsRequired().HasDefaultValue(false);
               entity.Property(e => e.Description).HasMaxLength(500);
               entity.Property(e => e.OriginalTaskTitle).HasMaxLength(255);

               entity.Property<DateTime>("DailyPlanDate");

               entity.HasOne(st => st.DailyPlanItem)
                     .WithMany()
                     .HasForeignKey(st => st.DailyPlanDate)
                     .IsRequired(false);
               entity.ToTable("ScheduledTasks");
           });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Victor De Marez",
                    Username = "victordemarez",
                    SchoolName = "Sint- - Franciscuscollege",
                    BirthDate = new DateTime(2007, 5, 15),
                    IsNotified = true
                });


            modelBuilder.Entity<UserTask>().HasData(
                new UserTask
                {
                    TaskId = 1,
                    UserId = 1,
                    Title = "Math Exam",
                    Description = "Prepare for the upcoming math exam on algebra and geometry.",
                    Type = TaskType.GROTETOETS,
                    Date = new DateTime(2025, 12, 10),
                    Priority = 5,
                    IsArchived = false
                },
                new UserTask
                {
                    TaskId = 2,
                    UserId = 1,
                    Title = "Appointment with the doctor",
                    Description = "Annual health check-up appointment at the clinic.",
                    Type = TaskType.AFSPRAAK,
                    Date = new DateTime(2025, 12, 12),
                    Priority = 4,
                    IsArchived = false
                },
                new UserTask
                {
                    TaskId = 3,
                    UserId = 1,
                    Title = "Science Project Submission",
                    Description = "Submit the final report and presentation for the science project.",
                    Type = TaskType.HUISWERK,
                    Date = new DateTime(2025, 12, 15),
                    Priority = 3,
                    IsArchived = false
                },

                new UserTask
                {
                    TaskId = 4,
                    UserId = 1,
                    Title = "English Toets",
                    Description = "English spelling and grammar test.",
                    Type = TaskType.KLEINETOETS,
                    Date = new DateTime(2025, 12, 11),
                    Priority = 2,
                    IsArchived = false
                },

                new UserTask
                {
                    TaskId = 5,
                    UserId = 1,
                    Title = "History Assignment",
                    Description = "Complete the history assignment on World War II.",
                    Type = TaskType.HUISWERK,
                    Date = new DateTime(2025, 12, 14),
                    Priority = 1,
                    IsArchived = false
                },

                new UserTask
                {
                    TaskId = 6,
                    UserId = 1,
                    Title = "Dentist Appointment",
                    Description = "Routine dental check-up and cleaning.",
                    Type = TaskType.AFSPRAAK,
                    Date = new DateTime(2025, 12, 20),
                    Priority = 4,
                    IsArchived = false
                },

            new UserTask
            {
                TaskId = 7,
                UserId = 1,
                Title = "Chemistry Lab Report",
                Description = "Write and submit the lab report for the recent chemistry experiment.",
                Type = TaskType.HUISWERK,
                Date = new DateTime(2025, 12, 18),
                Priority = 3,
                IsArchived = false
            },
            new UserTask
            {
                TaskId = 8,
                UserId = 1,
                Title = "Physics Toets",
                Description = "Prepare for the physics test on motion and forces.",
                Type = TaskType.KLEINETOETS,
                Date = new DateTime(2025, 12, 17),
                Priority = 2,
                IsArchived = false
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}