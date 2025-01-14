using System.CommandLine;

namespace TaskTrack;

class Program
{
    private static int TaskId = 0;

    public static async Task Main(string[] args)
    {
       //Root Command
        var rootCommand = new RootCommand("task-cli");

        //Defining the subCommands
        var addCommand = new Command("add", "Add a new task")
        {
            new Argument<string> (name: "TaskName", description: "Name of the task"),
            new Argument<string> (name: "TaskDescription", description: "Description of the task"),
            new Option<string>(new string[] {"--state", "-s"}, "State of the task (default: not started)"),
            new Option<string>(new string[] {"--priority", "-p"}, "Priority of the task (default: low)")
        };
        addCommand.AddAlias("a");

        var listCommand = new Command("list", "List all tasks");
        listCommand.AddAlias("l");

        var removeCommand = new Command("remove", "Remove a task")
        {
            new Argument<string> (name: "TaskName", description: "Name of the task"),
            new Argument<int> (name: "TaskId", description: "ID of the task")
        };
        removeCommand.AddAlias("r");

        var updateCommand = new Command("update", "Update a task")
        {
            new Argument<int> (name: "TaskId", description: "ID of the task"),
            new Argument<string> (name: "TaskName", description: "Name of the task"),
            new Argument<string> (name: "TaskDescription", description: "Description of the task"),
            new Option<string>(new string[] {"--state", "-s"}, "State of the task (default: not started)"),
            new Option<string>(new string[] {"--priority", "-p"}, "Priority of the task (default: low)")

        };
        updateCommand.AddAlias("u");




        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(removeCommand);
        rootCommand.AddCommand(updateCommand);
    }
}
