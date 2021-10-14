using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ApplicationDev.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> TraineeUsers { get; set; }
        public DbSet<AssignTraineeToCourse> AssignTraineetoCourses { get; set; }
        public DbSet<Trainer> TrainerUsers { get; set; }
        public DbSet<AssignTrainerToCourse> AssignTrainertoCourses { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}