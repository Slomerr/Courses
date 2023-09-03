﻿using Courses.Models;

namespace Courses.Models;

public class StudyGroup
{
    public int StudyGroupId { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public ICollection<Employee> Employees { get; set; }

    public StudyGroup()
    {
        Employees = new List<Employee>();
    }
}