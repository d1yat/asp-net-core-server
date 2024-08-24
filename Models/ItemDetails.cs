using System.Text.Json.Serialization;

namespace AspNetCoreDashboardBackend.Models;

public class ItemDetails {
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
    public int CFTId { get; set; }
    public int CFWId { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
    public string? FCCurrency { get; set; }
    public decimal FCDebit { get; set; }
    public decimal FCCredit { get; set; }
    public DateTime RefDate { get; set; }
    public string? Account { get; set; }
    public int JDTId { get; set; }
    public int JDTLineId { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CFWName { get; set; }
    public string? FatherNum { get; set; }
    public string? ParentName { get; set; }
}
