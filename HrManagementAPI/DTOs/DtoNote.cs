namespace HrManagementAPI.DTOs
{
    public class DtoNote
    {
        public int NoteId { get; set; }

        public int SubId { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateOnly NoteDate { get; set; }
    }
}
