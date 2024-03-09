namespace HrManagementAPI.DTOs
{
    public class DtoNoteCreate
    {
        public required int SubId { get; set; }

        public required string Description { get; set; }

        public required DateOnly NoteDate { get; set; }
    }
}
