using HrManagementAPI.Types;
using System.Text.Json.Serialization;

namespace HrManagementAPI.QueryParameters
{
    public class CandidateParameters
    {
        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? status { get; set; }
    }
}
