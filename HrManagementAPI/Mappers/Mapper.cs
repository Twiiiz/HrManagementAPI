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
                PrefferredLocation= submission.PrefferredLocation
            };
        }
    }
}
