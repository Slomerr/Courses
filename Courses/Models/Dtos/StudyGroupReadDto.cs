namespace Courses.Models.Dtos;

public class StudyGroupReadDto
{
    public int StudyGroupId { get; set; }
    public string Name { get; set; }
    public TeacherReadDto Teacher { get; set; }
    public CourseReadDto Course { get; set; }
    public ICollection<EmployeeReadDto> Employees { get; set; }
}