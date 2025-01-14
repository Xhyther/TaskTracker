using System;

namespace TaskTrack;

public class Tasks
{
    private static int TaskId { get; set; } = 0;
    private string TaskName { get; set; }
    private string? TaskDescription { get; set; }
    private bool TaskCompleted { get; set; }
    private string TaskState { get; set; }
    private string TaskPriority { get; set; }
    private DateTime TaskDate { get; set; }

    public Tasks()
    {
        TaskDate = DateTime.Now;
    }

    
   
}
