using Microsoft.EntityFrameworkCore;
using ModulesImplementation.Models;

namespace ModulesImplementation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<SubDepartment> SubDepartments {get; set;}
        public DbSet<DepartmentHierarchy> DepartmentsHierarchy { get; set; }
        public DbSet<Reminder> Reminder { get; set; }


    }
}
