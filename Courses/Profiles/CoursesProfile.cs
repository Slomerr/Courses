using AutoMapper;
using Courses.Models;
using Courses.Models.Dtos;

namespace Courses.Profiles;

public class CoursesProfile : Profile
{
    public CoursesProfile()
    {
        CreateMap<Course, CourseReadDto>();
        CreateMap<Teacher, TeacherReadDto>();
        CreateMap<StudyGroup, StudyGroupReadDto>()
            .ForMember(x => x.Course,
                expression => expression.MapFrom(s => new CourseReadDto
                {
                    CourseName = s.Course.CourseName
                }))
            .ForMember(x => x.Employees,
                expression => expression.MapFrom(s => s.Employees == null ? null
                    : s.Employees.Select(e => new EmployeeReadDto() 
                    {
                        FullName = e.FullName,
                        Organization = new OrganizationReadDto
                        {
                            Name = e.Organization.Name,
                            Inn = e.Organization.Inn
                        }
                    })))
            .ForMember(x => x.Teacher,
                expression => expression.MapFrom(s => new TeacherReadDto
                {
                    FullName = s.Teacher.FullName,
                    Email = s.Teacher.Email
                }));
        CreateMap<StudyGroupCreateDto, StudyGroup>();
    }
}