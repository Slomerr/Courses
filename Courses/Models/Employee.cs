using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string FullName { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public ICollection<StudyGroup> StudyGroups { get; set; }

    public Employee()
    {
        StudyGroups = new List<StudyGroup>();
    }
}