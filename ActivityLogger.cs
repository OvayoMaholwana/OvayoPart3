using System;
using System.Collections.Generic;
using System.Linq;

// ActivityLogger class is responsible for recording and managing activity logs.
public class ActivityLogger
{
    // Internal list to store log entries.
    private List<string> logs = new List<string>();

    // Maximum number of logs to retain.
    private const int MaxLogs = 20;

    // Records a new action with a timestamp.
    // If the log exceeds the maximum size, the oldest entry is removed.
    public void LogAction(string action)
    {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {action}";
        logs.Add(entry);

        if (logs.Count > MaxLogs)
            logs.RemoveAt(0);
    }

    // Retrieves the most recent logs up to the specified count (default is 10).
    public List<string> GetRecentLogs(int count = 10)
    {
        return logs.Skip(Math.Max(0, logs.Count - count)).ToList();
    }

    // Returns the most recent logs as a single formatted string.
    public string GetLogsAsString(int count = 10)
    {
        var recent = GetRecentLogs(count);
        return string.Join("\n", recent);
    }

    // Clears all logs from memory.
    public void ClearLogs() => logs.Clear();
}
