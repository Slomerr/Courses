namespace Courses.Models.Db;

public static class SqlQueries
{
    public static FormattableString GetEmployeesNotInGroup(int studyGroupId, int organizationId)
    {
        return @$"SELECT e.employee_id, e.full_name, e.organization_id FROM organizations o
                                                           INNER JOIN employees e on o.organization_id = e.organization_id
WHERE e.employee_id NOT IN (SELECT employees_employee_id FROM employee_study_group
                            WHERE study_groups_study_group_id = {studyGroupId})
  AND o.organization_id = {organizationId}";
    }
}