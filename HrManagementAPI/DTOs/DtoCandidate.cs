using HrManagementAPI.Types;

namespace HrManagementAPI.DTOs
{
    public class DtoCandidate
    {
        public int CandidateId { get; set; }
        
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateOnly? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public CandidateStatus Status { get; set; }
    }
}
