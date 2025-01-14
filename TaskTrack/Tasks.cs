using System;

namespace TaskTrack;

public class Tasks
{
    private static int TaskId { get; set; } = 0;
    private string TaskName { get; set; }
    private string? TaskDescription { get; set; }
    private bool TaskCompleted { get; set; }
    private TaskState TaskState { get; set; }
    private PriorityLevel TaskPriority { get; set; }
    private DateTime TaskDate { get; set; }

    public Tasks()
    {
        TaskId++;
        TaskName = $"Task {TaskId}";
        TaskDescription = string.Empty;
        TaskCompleted = false;
        TaskState = TaskState.NotStarted;
        TaskPriority = PriorityLevel.Normal;
        TaskDate = DateTime.Now;
    }

    
   
}
