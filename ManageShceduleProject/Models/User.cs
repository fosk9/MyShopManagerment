using System;
using System.Collections.Generic;

namespace ManageShceduleProject.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Role { get; set; }

    public string Department { get; set; } = null!;

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Team> TeamLeaders { get; set; } = new List<Team>();

    public virtual ICollection<Team> TeamMembers { get; set; } = new List<Team>();
}
