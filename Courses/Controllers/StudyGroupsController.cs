using AutoMapper;
using Courses.Models;
using Courses.Models.Db;
using Courses.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IActionResult> GetAll()
    {
        var groups = 
            await _dbContext.StudyGroups
                .Include(x => x.Course)
                .Include(x => x.Teacher)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Organization)
                .ToListAsync();
        var views = _mapper.Map<IEnumerable<StudyGroupReadDto>>(groups);
        return Ok(views);
    }

    public async Task<IActionResult> Get(int id)
    {
        var result = await _dbContext.StudyGroups
            .Include(x => x.Course)
            .Include(x => x.Teacher)
            .Include(x => x.Employees)
            .ThenInclude(x => x.Organization)
            .FirstOrDefaultAsync(x => x.StudyGroupId == id);
        if (result == null)
        {
            return NotFound("Study group not found by id");
        }

        var readDto = _mapper.Map<StudyGroupReadDto>(result);
        return Ok(readDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudyGroupCreateDto createDto)
    {
        StudyGroup group = await _dbContext.StudyGroups.FirstOrDefaultAsync(x => x.Name == createDto.Name);
        if (group != null)
        {
            return RedirectToAction(nameof(Get), new { id = group.StudyGroupId });
        }
        
        group = _mapper.Map<StudyGroup>(createDto);
        group = (await _dbContext.StudyGroups.AddAsync(group)).Entity;
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Get), new { id = group.StudyGroupId });
    }

    [HttpPut]
    public async Task<IActionResult> AddEmployee(int studyGroupId, int employeeId)
    {
        var studyGroup = await _dbContext.StudyGroups
            .Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.StudyGroupId == studyGroupId);
        if (studyGroup != null)
        {
            var alreadyContains = studyGroup.Employees.Any(x => x.EmployeeId == employeeId);
            if (!alreadyContains)
            {
                var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
                if (employee != null)
                {
                    studyGroup.Employees.Add(employee);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Get), new { id = studyGroupId });
        }

        return BadRequest("Study group not found by id");
    }
    
    [HttpPut]
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

            return RedirectToAction(nameof(Get), new { id = studyGroupId });
        }

        return BadRequest("Study group not found by id");
    }
}