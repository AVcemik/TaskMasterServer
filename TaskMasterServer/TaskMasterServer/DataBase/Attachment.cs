using System;
using System.Collections.Generic;

namespace TaskMasterServer.DataBase;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public string? AttachmentPath { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
