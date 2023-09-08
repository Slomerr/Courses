using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models;

public class Course
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string CourseName { get; set; }
}