using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class SubmissionSkill
{
    public int SubSkillId { get; set; }

    public int SubId { get; set; }

    public string SubSkillName { get; set; } = null!;

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual CandidateSubmission Sub { get; set; } = null!;
}
