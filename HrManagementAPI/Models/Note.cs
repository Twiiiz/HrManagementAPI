using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public int SubId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly NoteDate { get; set; }

    [JsonIgnore]
    public virtual CandidateSubmission Sub { get; set; } = null!;
}
