using Courses.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Courses.Models.ViewModels;

public class AddEmployeeViewModel
{
    public int StudyGroupId { get; set; }    
    public List<SelectListItem> Organizations { get; set; }
}