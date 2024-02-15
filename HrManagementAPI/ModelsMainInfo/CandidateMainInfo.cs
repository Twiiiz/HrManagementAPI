using HrManagementAPI.Types;

namespace HrManagementAPI.ModelsMainInfo
{
    public class CandidateMainInfo
    {
        public int CandidateId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public CandidateStatus Status { get; set; }
    }
}
