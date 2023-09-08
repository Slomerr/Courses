using Microsoft.AspNetCore.Mvc.Rendering;

namespace Courses.Models.ViewModels;

public class AddEmployeePartialViewModel
{
    public int StudyGroupId { get; set; }    
    public List<SelectListItem> Employees { get; set; }
}