using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class JobPosition
{
    public int PositionId { get; set; }

    public PositionType PositionType { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();
}
