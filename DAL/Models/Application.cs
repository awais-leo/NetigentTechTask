using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Application
{
    public int Id { get; set; }

    public string? AppStatus { get; set; }

    public string? ProjectRef { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectLocation { get; set; }

    public DateTime? OpenDt { get; set; }

    public DateTime? StartDt { get; set; }

    public DateTime? CompletedDt { get; set; }

    public decimal? ProjectValue { get; set; }

    public int StatusId { get; set; }

    public string? Notes { get; set; }

    public DateTime? Modified { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Inqury?> Inquries { get; set; } = new List<Inqury?>();

    public virtual StatusLevel Status { get; set; } = null!;
}
