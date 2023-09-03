using Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses.Models.Db;

public class CoursesDbContext : DbContext
{
    public DbSet<StudyGroup> StudyGroups { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudyGroup>(x =>
        {
            x.HasMany(g => g.Employees)
                .WithMany(e => e.StudyGroups);

            x.HasOne(g => g.Teacher);

            x.HasOne(g => g.Course);
        });

        modelBuilder.Entity<Organization>(x =>
        {
            x.HasOne(o => o.Teacher);

            x.HasMany(o => o.Employees);
        });
    }
}