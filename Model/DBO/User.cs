using System;
using System.Collections.Generic;

namespace Demo_var_6;

public partial class User
{
    public int UserId { get; set; }

    public int? RoleId { get; set; }

    public string? Lfp { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
