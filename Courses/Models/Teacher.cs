
namespace Courses.Models;

public class Teacher
{
    public int TeacherId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public ICollection<Organization> Organizations { get; set; }
}