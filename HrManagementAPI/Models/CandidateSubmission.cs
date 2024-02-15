using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class CandidateSubmission
{
    public int SubId { get; set; }

    [JsonRequired]
    public int CandidateId { get; set; }

    [JsonRequired]
    public DateOnly SubDate { get; set; }

    [JsonRequired]
    public string JobPosition { get; set; } = null!;

    [JsonRequired]
    public string CvFilepath { get; set; } = null!;

    [JsonRequired]
    public int HrId { get; set; }

    [JsonRequired]
    public string[] PrefferredLocation { get; set; } = null!;

    [JsonIgnore]
    public virtual Candidate Candidate { get; set; } = null!;

    [JsonIgnore]
    public virtual Employee Hr { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    [JsonIgnore]
    public virtual ICollection<SubmissionSkill> SubmissionSkills { get; set; } = new List<SubmissionSkill>();

    [JsonIgnore]
    public virtual ICollection<SubmissionStatus> SubmissionStatuses { get; set; } = new List<SubmissionStatus>();

    [JsonIgnore]
    public virtual ICollection<TagSubmission> TagSubmissions { get; set; } = new List<TagSubmission>();
}
