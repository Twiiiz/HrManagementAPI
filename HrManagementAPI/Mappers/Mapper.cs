using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using HrManagementAPI.ModelsMainInfo;
using System.Security.Cryptography.X509Certificates;

namespace HrManagementAPI.Mappers
{
    public class Mapper
    {
        public Candidate DtoToCandidate(DtoCandidateCreate candidateInfo)
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

        public DtoCandidate CandidateToDto(Candidate candidate)
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

        public CandidateSubmission DtoToSubmission(DtoSubmissionCreate submissionInfo)
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

        public DtoSubmission SubmissionToDto(CandidateSubmission submission)
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

        public Tag DtoToTag(DtoTagCreate tagInfo)
        {
            return new Tag
            {
                TagId = default,
                TagName = tagInfo.TagName,
                HrId = tagInfo.HrId,
                LastUpdateDate = DateOnly.FromDateTime(DateTime.Today)
            };
        }

        public DtoTag TagToDto(Tag tag)
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

        public DtoTagSubmission TagSubmissionToDto(TagSubmission tagSubmission)
        {
            return new DtoTagSubmission
            {
                SubTagId = tagSubmission.SubTagId,
                TagId = tagSubmission.TagId,
                SubId = tagSubmission.SubId
            };
        }

        public TagSubmission DtoToTagSubmission(DtoTagSubmissionCreate tagSubmissionInfo)
        {
            return new TagSubmission
            {
                SubTagId = default,
                TagId = tagSubmissionInfo.TagId,
                SubId = tagSubmissionInfo.SubId
            };
        }
    }
}
