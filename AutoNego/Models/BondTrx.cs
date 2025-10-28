using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AutoNego.Models;

public sealed class BondTrx
{
    [Required]
    public int id_bond_trx { get; set; }
    [AllowNull]
    public string side { get; set; }
    [AllowNull]
    public string series { get; set; }
    [AllowNull]
    public string counterparty_code { get; set; }
    [AllowNull]
    public DateTime deal_time { get; set; }
    [AllowNull]
    public int qty { get; set; }
    [AllowNull]
    public decimal price { get; set; }
    [AllowNull]
    public decimal yield { get; set; }
    [AllowNull]
    public string channel { get; set; }
    [AllowNull]
    public string status { get; set; }
    [AllowNull]
    public DateTime last_update { get; set; }
}

public sealed class Req_Post_BondTrx
{
    [AllowNull]
    public string side { get; set; }
    [AllowNull]
    public string series { get; set; }
    [AllowNull]
    public string counterparty_code { get; set; }
    [AllowNull]
    public int qty { get; set; }
    [AllowNull]
    public decimal price { get; set; }
    [AllowNull]
    public decimal yield { get; set; }
    [AllowNull]
    public string channel { get; set; }
    [AllowNull]
    public string status { get; set; }
}
