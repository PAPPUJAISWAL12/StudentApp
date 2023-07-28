using System;
using System.Collections.Generic;

namespace StudentApp.Models;

public partial class UserMessage
{
    public int MsgId { get; set; }

    public string Uname { get; set; } = null!;

    public string Uaddress { get; set; } = null!;

    public string Uphone { get; set; } = null!;

    public string Umsg { get; set; } = null!;
}
