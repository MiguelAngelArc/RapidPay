using System.Text.Json.Serialization;

public class HttpErrorInfo
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public int HttpStatusCode { get; set; }

    public string ErrorCode { get; set; } = string.Empty;
}
