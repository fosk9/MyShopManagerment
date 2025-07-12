using System;
using System.Collections.Generic;

namespace ManageShceduleProject.Models;

public partial class LeaveRequest
{
    public int RequestId { get; set; }

    public string? Title { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public string? ProcessedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int SenderId { get; set; }

    public string? Response { get; set; }

    public virtual User Sender { get; set; } = null!;
}
