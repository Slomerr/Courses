namespace Courses.Models.Dtos;

public class OrganizationReadDto
{
    public string Name { get; set; }
    public string Inn { get; set; }
    public TeacherReadDto Teacher { get; set; }
    public ICollection<EmployeeReadDto> Employees { get; set; }
}