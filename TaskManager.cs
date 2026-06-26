public class TaskManager
{
    private readonly TaskStorageHelper _storage = new TaskStorageHelper();
    private readonly ActivityLogger _logger;

    public TaskManager(ActivityLogger logger)
    {
        _logger = logger;
    }

    public void AddTask(string title, string description, string reminder)
    {
        if (string.IsNullOrWhiteSpace(title)) return;

        _storage.AddTask(title, description, reminder);
        _logger.LogAction($"Added new task: {title}");
    }

    public List<CyberTask> GetAllTasks()
    {
        return _storage.LoadTasks();
    }

    public bool MarkTaskComplete(int id)
    {
        bool success = _storage.MarkAsComplete(id);
        if (success) _logger.LogAction($"Marked task {id} as complete");
        return success;
    }

    public bool DeleteTask(int id)
    {
        bool success = _storage.DeleteTask(id);
        if (success) _logger.LogAction($"Deleted task {id}");
        return success;
    }
}