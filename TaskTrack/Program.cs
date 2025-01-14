using System.CommandLine;

namespace TaskTrack;

class Program
{
    public static async Task Main(string[] args)
    {
       //Root Command
        var rootCommand = new RootCommand("task-cli");
        var addCommand = new Command("add", "Add a new task");
        addCommand.AddAlias("a");
        var listCommand = new Command("list", "List all tasks");
        listCommand.AddAlias("l");
        var removeCommand = new Command("remove", "Remove a task");
        removeCommand.AddAlias("r");
        var updateCommand = new Command("update", "Update a task");
        updateCommand.AddAlias("u");

        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(removeCommand);
        rootCommand.AddCommand(updateCommand);

        //Defining the Argument for adding a Task
        var TaskNameArgument = new Argument<string>
        (name: "TaskName", 
        description: "Name of the task");

        var DeleteNameArgument = new Argument<string>
        (name: "Name of Task to delete",
        description: "Name of the task to delete");

        var DeleteIDArgument = new Argument<int>
        (name: "Id of Task to delete",
        description: "ID of the task to delete");

        var UpdatebyIDArgument = new Argument<int>
        (name: "Id of Task to update",
        description: "ID of the task to update");


        //Adding a task to the add command
        addCommand.AddArgument(TaskNameArgument);
        removeCommand.AddArgument(DeleteNameArgument);
        removeCommand.AddArgument(DeleteIDArgument);
        updateCommand.AddArgument(TaskNameArgument);
        updateCommand.AddArgument(DeleteNameArgument);




    }
}
