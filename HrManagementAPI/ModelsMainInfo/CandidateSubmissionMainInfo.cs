namespace HrManagementAPI.ModelsMainInfo
{
    public class CandidateSubmissionMainInfo
    {
        public int SubId { get; set; }

        public string JobPosition { get; set; } = null!;

        public DateOnly SubDate { get; set; }

        public int HrId { get; set; }
    }
}
