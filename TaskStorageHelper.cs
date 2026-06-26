using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// TaskStorageHelper handles persistence of tasks to a JSON file.
// It provides methods to load, save, add, update, and delete tasks.
public class TaskStorageHelper
{
    // File path where tasks are stored in JSON format.
    private const string FilePath = "tasks.json";

    // Loads tasks from the JSON file.
    // Returns an empty list if the file does not exist or if an error occurs.
    public List<CyberTask> LoadTasks()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new List<CyberTask>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<CyberTask>>(json) ?? new List<CyberTask>();
        }
        catch
        {
            return new List<CyberTask>();
        }
    }

    // Saves the provided list of tasks to the JSON file.
    // Tasks are ordered by their ID before saving.
    public void SaveTasks(List<CyberTask> tasks)
    {
        try
        {
            string json = JsonConvert.SerializeObject(tasks.OrderBy(t => t.Id), Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch { /* Silent fail for robustness */ }
    }

    // Adds a new task with a unique ID.
    // The ID is generated based on the highest existing ID.
    public void AddTask(string title, string description, string reminder)
    {
        var tasks = LoadTasks();
        int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

        var newTask = new CyberTask
        {
            Id = newId,
            Title = title,
            Description = description,
            Reminder = reminder
        };

        tasks.Add(newTask);
        SaveTasks(tasks);
    }

    // Marks a task as complete by its ID.
    // Returns true if successful, false if the task does not exist.
    public bool MarkAsComplete(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        task.IsComplete = true;
        SaveTasks(tasks);
        return true;
    }

    // Deletes a task by its ID.
    // Returns true if successful, false if the task does not exist.
    public bool DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        tasks.Remove(task);
        SaveTasks(tasks);
        return true;
    }
}
