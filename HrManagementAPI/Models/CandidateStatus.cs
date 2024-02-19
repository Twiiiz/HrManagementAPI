using System.Runtime.Serialization;

namespace HrManagementAPI.Models
{
    public enum CandidateStatus
    {
        [EnumMember(Value = "hired")]
        hired,

        [EnumMember(Value = "not_hired")]
        not_hired,

        [EnumMember(Value = "offer_made")]
        offer_made,

        [EnumMember(Value = "offer_denied")]
        offer_denied,

        [EnumMember(Value = "spam")]
        spam
    }
}
