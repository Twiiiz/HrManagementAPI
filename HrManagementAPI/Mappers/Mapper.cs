using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using System.Security.Cryptography.X509Certificates;

namespace HrManagementAPI.Mappers
{
    public class Mapper
    {
        public Candidate DtoToEntity(DtoCandidateCreate candidateInfo)
        {
            return new Candidate
            {
                CandidateId = default,
                FirstName = candidateInfo.FirstName,
                LastName = candidateInfo.LastName,
                BirthDate = candidateInfo.BirthDate,
                Email = candidateInfo.Email,
                PhoneNumber = candidateInfo.PhoneNumber,
                Status = candidateInfo.Status
            };
        }

        public DtoCandidate EntityToDto(Candidate candidate)
        {
            return new DtoCandidate
            {
                CandidateId = candidate.CandidateId,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                BirthDate = candidate.BirthDate,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                Status = candidate.Status
            };
        }

        public CandidateSubmission DtoToEntity(DtoSubmissionCreate submissionInfo)
        {
            return new CandidateSubmission
            {
                SubId = default,
                CandidateId = submissionInfo.CandidateId,
                SubDate = submissionInfo.SubDate,
                JobPosition = submissionInfo.JobPosition,
                CvFilepath = submissionInfo.CvFilepath,
                HrId = submissionInfo.HrId,
                PrefferredLocation = submissionInfo.PrefferredLocation
            };
        }

        public DtoSubmission EntityToDto(CandidateSubmission submission)
        {
            return new DtoSubmission
            {
                SubId = submission.SubId,
                CandidateId = submission.CandidateId,
                SubDate = submission.SubDate,
                JobPosition = submission.JobPosition,
                CvFilepath = submission.CvFilepath,
                HrId = submission.HrId,
                PrefferredLocation = submission.PrefferredLocation
            };
        }

        public Tag DtoToEntity(DtoTagCreate tagInfo)
        {
            return new Tag
            {
                TagId = default,
                TagName = tagInfo.TagName,
                HrId = tagInfo.HrId,
                LastUpdateDate = DateOnly.FromDateTime(DateTime.Today)
            };
        }

        public DtoTag EntityToDto(Tag tag)
        {
            return new DtoTag
            {
                TagId = tag.TagId,
                TagName = tag.TagName,
                HrId = tag.HrId,
                CreationDate = tag.CreationDate,
                LastUpdateDate = tag.LastUpdateDate
            };
        }

        public DtoTagSubmission EntityToDto(TagSubmission tagSubmission)
        {
            return new DtoTagSubmission
            {
                SubTagId = tagSubmission.SubTagId,
                TagId = tagSubmission.TagId,
                SubId = tagSubmission.SubId
            };
        }

        public TagSubmission DtoToEntity(DtoTagSubmissionCreate tagSubmissionInfo)
        {
            return new TagSubmission
            {
                SubTagId = default,
                TagId = tagSubmissionInfo.TagId,
                SubId = tagSubmissionInfo.SubId
            };
        }

        public SubmissionStatus DtoToEntity(DtoSubmissionStatusCreate submissionStatusInfo, int subId)
        {
            return new SubmissionStatus
            {
                SubStatId = default,
                SubId = subId,
                StatusName = submissionStatusInfo.StatusName,
                StatusDate = submissionStatusInfo.StatusDate
            };
        }

        public DtoSubmissionStatus EntityToDto(SubmissionStatus submissionStatus)
        {
            return new DtoSubmissionStatus
            {
                SubStatId = submissionStatus.SubStatId,
                SubId = submissionStatus.SubId,
                StatusName = submissionStatus.StatusName,
                StatusDate = submissionStatus.StatusDate
            };
        }

        public DtoJobOpening EntityToDto(JobOpening jobOpening)
        {
            return new DtoJobOpening
            {
                OpeningId = jobOpening.OpeningId,
                OfficeId = jobOpening.OfficeId,
                PositionId = jobOpening.PositionId,
                OpeningDate = jobOpening.OpeningDate,
                LastUpdateDate = jobOpening.LastUpdateDate,
                Status = jobOpening.Status,
                HiredCandidate = jobOpening.HiredCandidate
            };
        }

        public JobOpening DtoToEntity(DtoJobOpeningCreate jobOpeningInfo)
        {
            return new JobOpening
            {
                OpeningId = default,
                OfficeId = jobOpeningInfo.OfficeId,
                PositionId = jobOpeningInfo.PositionId,
                OpeningDate= jobOpeningInfo.OpeningDate,
                LastUpdateDate = DateOnly.FromDateTime(DateTime.Today),
                Status = jobOpeningInfo.Status,
                HiredCandidate = jobOpeningInfo.HiredCandidate
            };
        }

        public DtoNote EntityToDto(Note note)
        {
            return new DtoNote
            {
                NoteId = note.NoteId,
                SubId = note.SubId,
                Description = note.Description,
                NoteDate = note.NoteDate
            };
        }

        public Note DtoToEntity(DtoNoteCreate noteInfo)
        {
            return new Note
            {
                NoteId = default,
                SubId = noteInfo.SubId,
                Description = noteInfo.Description
            };
        }
    }
}
