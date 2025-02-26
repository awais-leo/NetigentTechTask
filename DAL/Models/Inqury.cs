using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Inqury
{
    public int Id { get; set; }

    public int? ApplicationId { get; set; }

    public string? SendToPerson { get; set; }

    public string? SendToRole { get; set; }

    public int? SendToPersonId { get; set; }

    public string? Subject { get; set; }

    public string? Inquiry { get; set; }

    public string? Response { get; set; }

    public DateTime? AskedDt { get; set; }

    public DateTime? CompletedDt { get; set; }

    public virtual Application? Application { get; set; }
}
