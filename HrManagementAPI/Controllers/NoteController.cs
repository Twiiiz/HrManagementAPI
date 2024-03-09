using HrManagementAPI.DTOs;
using HrManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HrManagementAPI.Controllers
{
    [Route("api/submissions/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetSubmissionNotes([FromQuery(Name = "sub-id")][Required] int subId, [FromQuery(Name = "hr-id")][Required] int hrId)
        {
            var notes = await _noteService.GetNotesAsync(subId, hrId);

            return Ok(notes);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubmissionNote([FromRoute(Name = "id")] int noteId)
        {
            var note = await _noteService.GetNoteByIdAsync(noteId);

            return Ok(note);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateSubmissionNote([FromQuery(Name = "hr-id")][Required] int hrId, [FromBody] DtoNoteCreate noteInfo)
        {
            var newNote = await _noteService.AddNoteAsync(hrId, noteInfo);

            return CreatedAtAction(nameof(GetSubmissionNote), new { id = newNote.NoteId }, newNote);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ReplaceSubmissionNote([FromRoute(Name = "id")] int noteId,
            [FromQuery][Required] int hrId, [FromBody] DtoNoteCreate noteInfo)
        {
            var updNote = await _noteService.UpdateNoteAsync(noteId, hrId, noteInfo);

            return Ok(updNote);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSubmissionNote([FromRoute(Name = "id")] int noteId, [FromQuery(Name = "hr-id")][Required] int hrId)
        {
            await _noteService.DeleteNoteAsync(noteId, hrId);

            return Ok("Note was deleted");
        }
    }
}
