using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

public class TaskStorageHelper
{
    private const string FilePath = "tasks.json";

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

    public void SaveTasks(List<CyberTask> tasks)
    {
        try
        {
            string json = JsonConvert.SerializeObject(tasks.OrderBy(t => t.Id), Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch { /* Silent fail for robustness */ }
    }

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

    public bool MarkAsComplete(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return false;

        task.IsComplete = true;
        SaveTasks(tasks);
        return true;
    }

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