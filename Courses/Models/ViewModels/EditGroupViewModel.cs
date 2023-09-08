using Courses.Models.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Courses.Models.ViewModels;

public class EditGroupViewModel
{
    public StudyGroupReadDto Group { get; set; }
}