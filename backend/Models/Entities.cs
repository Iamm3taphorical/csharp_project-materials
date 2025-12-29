namespace Backend.Models;

public class HashEntry
{
    public int EntryId { get; set; }
    public int BucketId { get; set; }
    public string KeyValue { get; set; } = string.Empty;
    public string DataJson { get; set; } = string.Empty;
    public int? NextEntryId { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ApiRequestLog
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public string RequestType { get; set; } = string.Empty;
    public string QueryKey { get; set; } = string.Empty;
    public DateTime RequestTime { get; set; }
}

public class WeatherDto
{
    public string City { get; set; } = string.Empty;
    public double TempC { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string RawJson { get; set; } = string.Empty;  
}
