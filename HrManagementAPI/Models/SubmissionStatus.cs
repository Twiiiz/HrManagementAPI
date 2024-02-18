using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class SubmissionStatus
{
    public int SubStatId { get; set; }

    public int SubId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateOnly StatusDate { get; set; }

    public virtual CandidateSubmission Sub { get; set; } = null!;
}
