using System;
using System.Collections.Generic;

namespace TaskMasterServer.DataBase;

public partial class Task
{
    public int TaskId { get; set; }

    public string? TaskName { get; set; }

    public string? Description { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateOnly? Deadline { get; set; }

    public int? StatusId { get; set; }

    public int? PriorityId { get; set; }

    public int? UserId { get; set; }

    public int? AttachmentId { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Priority? Priority { get; set; }

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
