using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class StatusLevel
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
