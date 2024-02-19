using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class Candidate
{
    public int CandidateId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public CandidateStatus Status { get; set; }

    public virtual ICollection<CandidateSubmission> CandidateSubmissions { get; set; } = new List<CandidateSubmission>();

    public virtual ICollection<JobOpening> JobOpenings { get; set; } = new List<JobOpening>();
}
