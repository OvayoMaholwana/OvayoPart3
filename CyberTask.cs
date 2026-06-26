public class CyberTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reminder { get; set; } = string.Empty;
    public bool IsComplete { get; set; } = false;
    public string CreatedAt { get; set; } = string.Empty;

    public CyberTask()
    {
        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
    }
}