using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoCandidateCreate
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required CandidateStatus Status { get; set; }
    }
}
