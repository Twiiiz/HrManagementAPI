using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class SubmissionSkill
{
    public int SubSkillId { get; set; }

    public int SubId { get; set; }

    public string SubSkillName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual CandidateSubmission Sub { get; set; } = null!;
}
