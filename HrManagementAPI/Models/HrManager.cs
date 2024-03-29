﻿using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class HrManager
{
    public int HrId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public int MonthlySalary { get; set; }

    public int? OfficeId { get; set; }

    public virtual ICollection<CandidateSubmission> CandidateSubmissions { get; set; } = new List<CandidateSubmission>();

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual Office? Office { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
