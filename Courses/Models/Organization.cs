namespace Courses.Models;

public class Organization
{
    public int OrganizationId { get; set; }
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