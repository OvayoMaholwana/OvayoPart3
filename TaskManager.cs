using System.Collections.Generic;

/// <summary>
/// Manages tasks by interacting with storage and logging actions.
/// Provides methods to add, retrieve, complete, and delete tasks.
/// </summary>
public class TaskManager
{
    // Helper class instance for storing and retrieving tasks.
    private readonly TaskStorageHelper _storage = new TaskStorageHelper();

    // Logger instance for recording activity actions.
    private readonly ActivityLogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskManager"/> class with a logger.
    /// </summary>
    /// <param name="logger">Logger used to record task-related actions.</param>
    public TaskManager(ActivityLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Adds a new task if the title is valid (non-empty).
    /// Logs the action once the task is added.
    /// </summary>
    /// <param name="title">Title of the task.</param>
    /// <param name="description">Description of the task.</param>
    /// <param name="reminder">Optional reminder for the task.</param>
    public void AddTask(string title, string description, string reminder)
    {
        if (string.IsNullOrWhiteSpace(title)) return;

        _storage.AddTask(title, description, reminder);
        _logger.LogAction($"Added new task: {title}");
    }

    /// <summary>
    /// Retrieves all tasks from storage.
    /// </summary>
    /// <returns>A list of <see cref="CyberTask"/> objects.</returns>
    public List<CyberTask> GetAllTasks()
    {
        return _storage.LoadTasks();
    }

    /// <summary>
    /// Marks a task as complete by its ID.
    /// Logs the action if successful.
    /// </summary>
    /// <param name="id">The ID of the task to mark complete.</param>
    /// <returns>True if the task was successfully marked complete; otherwise, false.</returns>
    public bool MarkTaskComplete(int id)
    {
        bool success = _storage.MarkAsComplete(id);
        if (success) _logger.LogAction($"Marked task {id} as complete");
        return success;
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// Logs the action if successful.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <returns>True if the task was successfully deleted; otherwise, false.</returns>
    public bool DeleteTask(int id)
    {
        bool success = _storage.DeleteTask(id);
        if (success) _logger.LogAction($"Deleted task {id}");
        return success;
    }
}
