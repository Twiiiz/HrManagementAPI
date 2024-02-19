using HrManagementAPI.DTOs;
using HrManagementAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace HrManagementAPI.Mappers
{
    public class Mapper
    {
        public Candidate DtoToCandidate(DtoCreateCandidate candidateInfo)
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
    }
}
