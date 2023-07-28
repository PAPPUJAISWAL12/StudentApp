using System;
using System.Collections.Generic;

namespace StudentApp.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserAddress { get; set; } = null!;

    public string UserRoleType { get; set; } = null!;

    public string UserPhone { get; set; } = null!;
}
