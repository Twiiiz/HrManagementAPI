using System.Runtime.Serialization;

namespace HrManagementAPI.Models
{
    public enum OpeningStatus
    {
        [EnumMember(Value = "available")]
        available,

        [EnumMember(Value = "closed")]
        closed,

        [EnumMember(Value = "offer_under_consideration")]
        offer_under_consideration,
    }
}
