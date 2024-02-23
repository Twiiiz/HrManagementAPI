﻿namespace HrManagementAPI.DTOs
{
    public class DtoSubmissionStatusCreate
    {
        public string StatusName { get; set; } = string.Empty;

        public DateOnly StatusDate { get; set; }

        public required int HrId { get; set; }
    }
}
