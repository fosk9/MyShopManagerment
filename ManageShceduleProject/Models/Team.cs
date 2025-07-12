using System;
using System.Collections.Generic;

namespace ManageShceduleProject.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public int LeaderId { get; set; }

    public int MemberId { get; set; }

    public virtual User Leader { get; set; } = null!;

    public virtual User Member { get; set; } = null!;
}
