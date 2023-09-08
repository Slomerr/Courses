
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models;

public class Teacher
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TeacherId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string FullName { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; }
    public ICollection<Organization> Organizations { get; set; }
}