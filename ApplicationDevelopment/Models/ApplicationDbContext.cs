using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ApplicationDevelopment.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}