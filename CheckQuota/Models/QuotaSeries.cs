using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace CheckQuota.Models;

public sealed class QuotaSeries
{
    public int id_quota_series { get; init; }
    public string series { get; set; } = String.Empty;
    public int quota { get; set; } = 0;
    public DateTime last_update { get; init; }
}

public sealed class Req_QuotaSeries
{
    [Required]
    public string series { get; set; } = String.Empty;
    [Required]
    public int quota { get; set; } = 0;
}

public sealed class Req_Checking_QuotaSeries
{
    [Required]
    public string series { get; set; } = String.Empty;
    [Required]
    public int qty { get; set; } = 0;
}

public sealed class Req_Update_QuotaSeries
{
    [Required]
    public int id_quota_series { get; set; }
    [AllowNull]
    public string series { get; set; } = String.Empty;
    [AllowNull]
    public int qty { get; set; } = 0;
}
