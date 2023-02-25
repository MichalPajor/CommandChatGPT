namespace ask;

public class ContentObj
{
    public string model { get; set; } = null!;
    public string prompt { get; set; } = null!;
    public int temperature { get; set; }
    public int max_tokens { get; set; }
}