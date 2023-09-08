
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models;

public class Organization
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrganizationId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    public string Inn { get; set; }
    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public Organization()
    {
        Employees = new List<Employee>();
    }
}