using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class TagSubmission
{
    public int SubTagId { get; set; }

    public int TagId { get; set; }

    public int SubId { get; set; }

    public virtual CandidateSubmission Sub { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
