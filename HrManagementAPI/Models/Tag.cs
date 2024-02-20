using System;
using System.Collections.Generic;

namespace HrManagementAPI.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public DateOnly LastUpdateDate { get; set; }

    public int HrId { get; set; }

    public virtual HrManager Hr { get; set; } = null!;

    public virtual ICollection<TagSubmission> TagSubmissions { get; set; } = new List<TagSubmission>();
}
