using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class EmployeeSkill
{
    public int EmpSkillId { get; set; }

    public int? EmployeeId { get; set; }

    public string EmpSkillName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Employee? Employee { get; set; }
}
