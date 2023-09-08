

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models;

public class StudyGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudyGroupId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public StudyGroup()
    {
        Employees = new List<Employee>();
    }

    public static bool ValidateName(string name, out string message)
    {
        message = null;
        if (string.IsNullOrEmpty(name))
        {
            message = "Empty name value";
            return false;
        }

        return true;
    }
}