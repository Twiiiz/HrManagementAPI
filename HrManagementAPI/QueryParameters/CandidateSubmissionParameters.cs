using System.Text.Json.Serialization;

namespace HrManagementAPI.QueryParameters
{
    public class CandidateSubmissionParameters
    {
        public int? candidate_id { get; set; }

        public string? job_position { get; set; }

        public string? prefferred_location { get; set; }
    }
}
