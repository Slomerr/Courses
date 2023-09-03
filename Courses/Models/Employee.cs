using System.Diagnostics.CodeAnalysis;
using Courses.Models;

namespace Courses.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public ICollection<StudyGroup> StudyGroups { get; set; }

    public Employee()
    {
        StudyGroups = new List<StudyGroup>();
    }
}