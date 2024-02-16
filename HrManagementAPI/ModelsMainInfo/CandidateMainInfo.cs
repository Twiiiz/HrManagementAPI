using HrManagementAPI.Types;
using System.Text.Json.Serialization;

namespace HrManagementAPI.ModelsMainInfo
{
    public class CandidateMainInfo
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required CandidateStatus Status { get; set; }
    }
}
