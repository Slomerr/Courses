using AutoMapper;
using Courses.Models.Db;
using Courses.Models.Dtos;
using Courses.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Courses.Controllers;

public class EmployeesController : Controller
{
    private readonly CoursesDbContext _dbContext;

    public EmployeesController(CoursesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> AddEmployeeToGroup(int studyGroupId)
    {
        var group = await _dbContext.StudyGroups.Include(x => x.Teacher)
            .FirstOrDefaultAsync(x => x.StudyGroupId == studyGroupId);
        if (group == null)
        {
            return BadRequest("Not correct group id");
        }

        var organizations = _dbContext.Organizations.Where(x => x.TeacherId == group.TeacherId);
        var list = new List<SelectListItem>();
        list.Add(new SelectListItem(string.Empty, "0"));
        foreach (var organization in organizations)
        {
            list.Add(new SelectListItem(organization.Name, organization.OrganizationId.ToString()));
        }
        
        return View(new AddEmployeeViewModel() { StudyGroupId = group.StudyGroupId, Organizations = list });
    }

    public async Task<IActionResult> GetEmployeesFroOrganizationNotInGroup(int organizationId, int groupId)
    {
        var employees  = _dbContext.Employees
            .FromSql(SqlQueries.GetEmployeesNotInGroup(groupId, organizationId));
        var list = new List<SelectListItem>();
        list.Add(new SelectListItem(string.Empty, "0"));
        foreach (var employee in employees)
        {
            list.Add(new SelectListItem(employee.FullName, employee.EmployeeId.ToString()));
        }
        
        return PartialView(new AddEmployeePartialViewModel(){StudyGroupId = groupId, Employees = list});
    }

    [HttpPost]
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

            return RedirectToAction(nameof(AddEmployeeToGroup), new { studyGroupId });
        }

        return BadRequest("Study group not found by id");
    }
}