using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HrManagementAPI.Models;

public partial class Tag
{
    public int TagId { get; set; }

    [JsonRequired]
    public string TagName { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public DateOnly LastUpdateDate { get; set; }

    [JsonRequired]
    public int? HrId { get; set; }

    [JsonIgnore]
    public virtual Employee? Hr { get; set; }
    
    public virtual ICollection<TagSubmission> TagSubmissions { get; set; } = new List<TagSubmission>();
}
