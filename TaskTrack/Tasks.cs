using System;

namespace TaskTrack;

public class Tasks
{
    public static int TaskId { get; set; } = 0;
    public string TaskName { get; set; }
    public string? TaskDescription { get; set; }
    public bool TaskCompleted { get; set; }
    public State TaskState { get; set; }
    public Priority TaskPriority { get; set; }
    public DateTime TaskDate { get; set; }

    public Tasks(string name, string description, State state, Priority priority)
    {
        TaskId++;
        TaskName = name;
        TaskDescription = description;
        TaskCompleted = false;
        TaskState = state;
        TaskPriority = priority;
        TaskDate = DateTime.Now;
    }
   

    
   
}
