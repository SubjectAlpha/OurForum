namespace OurForum.Backend.Utility;

public class ServiceResponse
{
    public List<string> Errors { get; set; } = [];
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; } = false;
    public object? Data { get; set; }
}
