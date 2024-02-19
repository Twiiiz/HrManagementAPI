using System.Runtime.Serialization;

namespace HrManagementAPI.Models
{
    public enum PositionType
    {
        [EnumMember(Value = "developer")]
        developer,

        [EnumMember(Value = "designer")]
        designer,

        [EnumMember(Value = "qa")]
        qa,

        [EnumMember(Value = "data_related")]
        data_related,

        [EnumMember(Value = "project_manager")]
        project_manager,

        [EnumMember(Value = "sys_admin")]
        sys_admin,

        [EnumMember(Value = "support")]
        support,

        [EnumMember(Value = "hr")]
        hr
    }
}
