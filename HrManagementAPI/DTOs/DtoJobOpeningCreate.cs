using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoJobOpeningCreate
    {
        public int? OfficeId { get; set; }

        public int PositionId { get; set; }

        public OpeningStatus Status { get; set; }

        public int? HiredCandidate { get; set; }
    }
}
