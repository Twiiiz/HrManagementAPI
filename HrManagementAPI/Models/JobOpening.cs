﻿using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class JobOpening
{
    public int OpeningId { get; set; }

    public int? OfficeId { get; set; }

    public int PositionId { get; set; }

    public DateOnly OpeningDate { get; set; }

    public DateOnly LastUpdateDate { get; set; }

    public OpeningStatus Status { get; set; }

    public int? HiredCandidate { get; set; }

    public virtual Candidate? HiredCandidateNavigation { get; set; }

    public virtual Office? Office { get; set; }

    public virtual JobPosition Position { get; set; } = null!;
}
