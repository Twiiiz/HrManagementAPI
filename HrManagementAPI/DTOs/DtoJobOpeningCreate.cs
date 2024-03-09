using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoJobOpeningCreate
    {
        public int? OfficeId { get; set; }

        public required int PositionId { get; set; }

        public DateOnly OpeningDate { get; set; }

        public required OpeningStatus Status { get; set; }

        public int? HiredCandidate { get; set; }
    }
}
