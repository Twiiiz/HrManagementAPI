using HrManagementAPI.Types;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class JobOpening
{
    public int OpeningId { get; set; }

    public int? OfficeId { get; set; }

    [JsonRequired]
    public int PositionId { get; set; }

    [JsonRequired]
    public DateOnly OpeningDate { get; set; }

    [JsonRequired]
    public DateOnly LastUpdateDate { get; set; }

    [JsonRequired]
    public OpeningStatus Status { get; set; }

    public int? HiredCandidate { get; set; }

    public virtual Candidate? HiredCandidateNavigation { get; set; }

    public virtual Office? Office { get; set; }

    public virtual JobPosition Position { get; set; } = null!;
}
