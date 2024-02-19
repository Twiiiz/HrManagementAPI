namespace HrManagementAPI.DTOs
{
    public class DtoSubmission
    {
        public int SubId { get; set; }

        public int CandidateId { get; set; }

        public string JobPosition { get; set; } = string.Empty;

        public DateOnly SubDate { get; set; }

        public int HrId { get; set; }

        public string CvFilepath { get; set; } = string.Empty;

        public string[] PrefferredLocation { get; set; } = null!;
    }
}
