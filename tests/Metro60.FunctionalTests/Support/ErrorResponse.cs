namespace Metro60.FunctionalTests.Support;

public class ErrorResponse
{
    public string Type { get; set; }
    public string Message { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
    public string StackTrace { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
}
