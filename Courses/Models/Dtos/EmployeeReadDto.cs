namespace Courses.Models.Dtos;

public class EmployeeReadDto
{
    public string FullName { get; set; }
    public OrganizationReadDto Organization { get; set; }
    public ICollection<StudyGroupReadDto> StudyGroups { get; set; }
}