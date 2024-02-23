namespace HrManagementAPI.DTOs
{
    public class DtoSubmissionStatus
    {
        public int SubStatId { get; set; }

        public int SubId { get; set; }

        public string StatusName { get; set; } = string.Empty;

        public DateOnly StatusDate { get; set; }
    }
}
