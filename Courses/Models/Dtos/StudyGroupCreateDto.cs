using System.ComponentModel.DataAnnotations;

namespace Courses.Models.Dtos;

public class StudyGroupCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int TeacherId { get; set; }
    public int CourseId { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(Name) && TeacherId > 0 && CourseId > 0;
    }
}