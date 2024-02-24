using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoJobOpening
    {
        public int OpeningId { get; set; }

        public int? OfficeId { get; set; }

        public int PositionId { get; set; }

        public DateOnly OpeningDate { get; set; }

        public DateOnly LastUpdateDate { get; set; }

        public OpeningStatus Status { get; set; }

        public int? HiredCandidate { get; set; }
    }
}
