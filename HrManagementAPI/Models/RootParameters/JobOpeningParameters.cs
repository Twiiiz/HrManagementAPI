namespace HrManagementAPI.Models.RootParameters
{
    public class JobOpeningParameters
    {
        public int? OfficeId { get; set; }

        public int? PositionId { get; set; }

        public OpeningStatus? Status { get; set; }

        public int? HiredCandidate { get; set; }

        public DateOnly? OpeningDate { get; set; }
    }
}
