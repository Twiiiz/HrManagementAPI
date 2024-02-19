using HrManagementAPI.Models;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models.RootParameters
{
    public class CandidateParameters
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public CandidateStatus? Status { get; set; }
    }
}
