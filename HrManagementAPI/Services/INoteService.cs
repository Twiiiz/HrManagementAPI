using HrManagementAPI.DTOs;

namespace HrManagementAPI.Services
{
    public interface INoteService
    {
        public Task<List<DtoNote>> GetNotesAsync(int subId, int hrId);

        public Task<DtoNote> GetNoteByIdAsync(int noteId);

        public Task<DtoNote> AddNoteAsync(int hrId, DtoNoteCreate noteInfo);

        public Task<DtoNote> UpdateNoteAsync(int noteId, int hrId, DtoNoteCreate noteInfo);

        public Task DeleteNoteAsync(int noteId, int hrId);
    }
}
