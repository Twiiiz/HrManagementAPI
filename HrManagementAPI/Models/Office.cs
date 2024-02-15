using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class Office
{
    public int OfficeId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();
}
