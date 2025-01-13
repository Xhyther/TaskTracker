using System;

namespace TaskTrack;

public class Tasks
{
    private static int TaskId { get; set; } = 0;
    private string TaskName { get; set; } = $"Task {TaskId}";
    private string? TaskDescription { get; set; } = string.Empty;
    private bool TaskCompleted { get; set; } = false;
    private TaskState TaskState { get; set; } = TaskState.NotStarted;
    private PriorityLevel TaskPriority { get; set; } = PriorityLevel.Normal;
    private DateTime TaskDate { get; set; } = DateTime.Now;

    
    public Tasks()
    {
        TaskId++;
    }
}
