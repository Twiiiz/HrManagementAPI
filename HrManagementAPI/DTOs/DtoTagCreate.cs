using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoTagCreate
    {
        public required string TagName { get; set; }

        //public DateOnly CreationDate { get; set; }

        //public DateOnly LastUpdateDate { get; set; }

        public required int HrId { get; set; }
    }
}
