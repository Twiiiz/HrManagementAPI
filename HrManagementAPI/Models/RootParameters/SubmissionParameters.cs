using System.Text.Json.Serialization;

namespace HrManagementAPI.Models.RootParameters
{
    public class SubmissionParameters
    {
        public int? CandidateId { get; set; }

        public string? JobPosition { get; set; }

        public string? PrefferredLocation { get; set; }
    }
}
