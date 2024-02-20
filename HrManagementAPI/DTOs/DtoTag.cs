using HrManagementAPI.Models;

namespace HrManagementAPI.DTOs
{
    public class DtoTag
    {
        public int TagId { get; set; }

        public string TagName { get; set; } = string.Empty;

        public DateOnly CreationDate { get; set; }

        public DateOnly LastUpdateDate { get; set; }

        public int HrId { get; set; }

        public virtual ICollection<TagSubmission> TagSubmissions { get; set; } = new List<TagSubmission>();
    }
}
