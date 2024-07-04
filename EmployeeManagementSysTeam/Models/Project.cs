﻿using EmployeesManagementSysTeam.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
