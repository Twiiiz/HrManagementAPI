﻿using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public int SubId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly NoteDate { get; set; }

    public virtual CandidateSubmission Sub { get; set; } = null!;
}
