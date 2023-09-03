using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Courses.Models.Dtos;

public class StudyGroupCreateDto
{
    [Required, NotNull]
    public string Name { get; set; }
    [Required]
    public int TeacherId { get; set; }
    [Required]
    public int CourseId { get; set; }
}