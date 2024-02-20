using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class Office
{
    public int OfficeId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<HrManager> HrManagers { get; set; } = new List<HrManager>();

    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();
}
