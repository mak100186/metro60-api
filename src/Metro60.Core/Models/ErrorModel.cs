namespace Metro60.Core.Models;

public class ErrorModel
{
    public string Type { get; }
    public string Message { get; }
    public string StackTrace { get; }

    public ErrorModel(Exception ex)
    {
        Type = ex.GetType().Name;
        Message = ex.Message;
        StackTrace = ex.ToString();
    }
}
