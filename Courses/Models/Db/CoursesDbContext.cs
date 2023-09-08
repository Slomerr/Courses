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
}