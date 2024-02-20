using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class CandidateSubmission
{
    public int SubId { get; set; }

    public int CandidateId { get; set; }

    public DateOnly SubDate { get; set; }

    public string JobPosition { get; set; } = null!;

    public string CvFilepath { get; set; } = null!;

    public int HrId { get; set; }

    public string[] PrefferredLocation { get; set; } = null!;

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual HrManager Hr { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<SubmissionSkill> SubmissionSkills { get; set; } = new List<SubmissionSkill>();

    public virtual ICollection<SubmissionStatus> SubmissionStatuses { get; set; } = new List<SubmissionStatus>();

    public virtual ICollection<TagSubmission> TagSubmissions { get; set; } = new List<TagSubmission>();
}
