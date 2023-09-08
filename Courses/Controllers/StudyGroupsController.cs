using System.Net;
using System.Text.Encodings.Web;
using AutoMapper;
using Courses.Models;
using Courses.Models.Db;
using Courses.Models.Dtos;
using Courses.Models.ViewModels;
using Courses.Views.StudyGroups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Courses.Controllers;

public class StudyGroupsController : Controller
{
    private readonly CoursesDbContext _dbContext;
    private readonly IMapper _mapper;

    public StudyGroupsController(CoursesDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IActionResult> GetAllGroups()
    {
        var groups =
            await _dbContext.StudyGroups
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Organization)
                .ToListAsync();
        var views = _mapper.Map<IEnumerable<StudyGroupReadDto>>(groups);
        return View(views);
    }

    public async Task<IActionResult> CreateGroup()
    {
        var teachers = await _dbContext.Teachers.ToListAsync();
        var teacherReadDtos = _mapper.Map<IEnumerable<TeacherReadDto>>(teachers);
        return View(new CreateGroupViewModel { Teachers = teacherReadDtos });
    }

    [HttpPost]
    public async Task<IActionResult> Create(int teacherId, string name)
    {
        if (!StudyGroup.ValidateName(name, out var message))
        {
            Console.WriteLine($"--> Trying create group using not valid name [{message}]");
            return RedirectToAction(nameof(CreateGroup));
        }

        StudyGroup group = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.Name == name);
        if (group != null)
        {
            Console.WriteLine($"--> Trying create a new group, but a group with some name already exist");
            return RedirectToAction(nameof(CreateGroup));
        }

        var course = await _dbContext.Courses.FirstOrDefaultAsync();
        if (course == null)
        {
            Console.WriteLine("--> Couldn't find any course at try create study group");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        bool validTeacherId = await _dbContext.Teachers.AnyAsync(x => x.TeacherId == teacherId);
        if (!validTeacherId)
        {
            Console.WriteLine("[Error] --> Couldn't find any teacher at try create study group, wrong create dto");
            return RedirectToAction(nameof(CreateGroup));
        }

        group = new StudyGroup()
        {
            //StudyGroupId = await _dbContext.StudyGroups.CountAsync() + 1,
            Name = name,
            CourseId = course.CourseId,
            TeacherId = teacherId
        };
        group = (await _dbContext.StudyGroups.AddAsync(group)).Entity;
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(EditGroup), new { id = group.StudyGroupId });
    }

    public async Task<IActionResult> EditGroup(int id)
    {
        var group = await _dbContext.StudyGroups
            .Include(x => x.Course)
            .Include(x => x.Teacher)
            .Include(x => x.Employees)
            .ThenInclude(x => x.Organization)
            .FirstOrDefaultAsync(x => x.StudyGroupId == id);
        if (group == null)
        {
            return NotFound("Study group not found by id");
        }

        var groupRead = _mapper.Map<StudyGroupReadDto>(group);
        return View(new EditGroupViewModel { Group = groupRead });
    }

    [HttpPost]
    public async Task<IActionResult> EditName(int groupId, string newName)
    {
        if (!StudyGroup.ValidateName(newName, out var message))
        {
            Console.WriteLine($"[Warning]-->{message} for {groupId} groupId");
            return RedirectToAction(nameof(EditGroup),
                new { id = groupId });
        }

        var group = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.StudyGroupId == groupId);
        if (group == null)
        {
            return BadRequest("Not correct group id");
        }

        var tempGroup = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.Name == newName);
        if (tempGroup != null)
        {
            Console.WriteLine($"--> Group with some name already exist [{newName}]");
            return RedirectToAction(nameof(EditGroup),
                new { id = groupId });
        }

        group.Name = newName;
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(EditGroup),
            new { id = groupId });
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteEmployee(int studyGroupId, int employeeId)
    {
        var studyGroup = await _dbContext.StudyGroups
            .Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.StudyGroupId == studyGroupId);
        if (studyGroup != null)
        {
            var wasRemove = studyGroup.Employees
                .Remove(studyGroup.Employees
                    .FirstOrDefault(x => x.EmployeeId == employeeId));
            if (wasRemove)
            {
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(EditGroup), new { id = studyGroupId });
        }

        return BadRequest("Study group not found by id");
    }
}