using System;
using System.Collections.Generic;

namespace ManageProduct.DAL.Entities;

public partial class User
{
    public int UserID { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleID { get; set; }

    public virtual Role Role { get; set; } = null!;
}
