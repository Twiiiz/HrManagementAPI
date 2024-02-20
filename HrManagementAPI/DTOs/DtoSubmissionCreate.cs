namespace HrManagementAPI.DTOs
{
    public class DtoSubmissionCreate
    {
        public required string JobPosition { get; set; }

        public required DateOnly SubDate { get; set; }

        public required int CandidateId { get; set; }

        public required int HrId { get; set; }

        public required string CvFilepath { get; set; }

        public required string[] PrefferredLocation { get; set; } = null!;
    }
}
