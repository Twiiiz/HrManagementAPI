namespace HrManagementAPI.DTOs
{
    public class DtoSubmissionCreate
    {
        public string JobPosition { get; set; } = string.Empty;

        public DateOnly SubDate { get; set; }

        public int CandidateId { get; set; }

        public int HrId { get; set; }

        public string CvFilepath { get; set; } = string.Empty;

        public string[] PrefferredLocation { get; set; } = null!;
    }
}
