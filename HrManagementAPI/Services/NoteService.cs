using HrManagementAPI.DTOs;
using HrManagementAPI.Mappers;
using HrManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HrManagementAPI.Services
{
    public class NoteService: INoteService
    {
        private readonly HrManagementContext _context;
        private readonly Mapper _mapper;

        public NoteService(HrManagementContext context)
        {
            _context = context;
            _mapper = new Mapper();
        }

        public async Task<List<DtoNote>> GetNotesAsync(int subId, int hrId)
        {
            if (!await HasPermission(subId, hrId))
                throw new ArgumentException("Current HR manager doesn't have access to the requested submission");

            var notes = await _context.Notes
                .Where(x => x.SubId == subId)
                .Select(x => _mapper.EntityToDto(x))
                .ToListAsync();

            return notes;
        }

        public async Task<DtoNote> GetNoteByIdAsync(int noteId)
        {
            var note = await _context.Notes
                .Where(x => x.NoteId == noteId)
                .Select(x => _mapper.EntityToDto(x))
                .FirstOrDefaultAsync();
            if (note == null)
                throw new ArgumentException("Unable to find requested note");

            return note;
        }

        public async Task<DtoNote> AddNoteAsync(int hrId, DtoNoteCreate noteInfo)
        {
            Note note = _mapper.DtoToEntity(noteInfo);
            if (!await HasPermission(note.SubId, hrId))
                throw new ArgumentException("Current HR manager doesn't have access to the submission");

            note.NoteDate = DateOnly.FromDateTime(DateTime.Today);
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(note);
        }

        public async Task<DtoNote> UpdateNoteAsync(int noteId, int hrId, DtoNoteCreate noteInfo)
        {
            var note = await _context.Notes.Where(x => x.NoteId == noteId).FirstOrDefaultAsync();

            if (!await HasPermission(note.SubId, hrId))
                throw new ArgumentException("Current HR manager doesn't have access to the requested note");

            note.SubId = noteInfo.SubId;
            note.Description = noteInfo.Description;
            note.NoteDate = DateOnly.FromDateTime(DateTime.Today);
            await _context.SaveChangesAsync();

            return _mapper.EntityToDto(note);
        }

        public async Task DeleteNoteAsync(int noteId, int hrId)
        {
            var note = _context.Notes.Where(x => x.NoteId == noteId).First();
            if (!await HasPermission(note.SubId, hrId))
                throw new ArgumentException("Current HR manager doesn't have access to the requested submission");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> HasPermission(int subId, int hrId)
        {
            var submission = await _context.CandidateSubmissions.Where(x => x.SubId == subId).FirstOrDefaultAsync();
            if (submission == null)
                throw new ArgumentException("Unable to find requested submission");
            if (submission.HrId != hrId)
                return false;

            return true;
        }
    }
}
