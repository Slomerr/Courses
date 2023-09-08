namespace Courses.Models.Dtos;

public class OrganizationReadDto
{
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string Inn { get; set; }
    public TeacherReadDto Teacher { get; set; }
    public IEnumerable<EmployeeReadDto> Employees { get; set; }
}