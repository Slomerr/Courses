using Courses.Models.Dtos;

namespace Courses.Models.ViewModels;

public class CreateGroupViewModel
{
    public IEnumerable<TeacherReadDto> Teachers { get; set; }
}