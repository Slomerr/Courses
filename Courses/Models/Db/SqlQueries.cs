namespace Courses.Models.Db;

public static class SqlQueries
{
    public static FormattableString GetEmployeesNotInGroup(int studyGroupId, int organizationId)
    {
        return @$"SELECT E.EmployeeId, E.FullName, E.OrganizationId FROM Organizations O
                  INNER JOIN Employees E on o.OrganizationId = E.OrganizationId
WHERE E.EmployeeId NOT IN (SELECT EmployeesEmployeeId FROM EmployeeStudyGroup
                           WHERE StudyGroupsStudyGroupId = {studyGroupId})
  AND O.OrganizationId = {organizationId}";
    }
}