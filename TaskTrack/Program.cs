using System.CommandLine;

namespace TaskTrack;

class Program
{
    public static async Task Main(string[] args)
    {
       //Root Command
        var rootCommand = new RootCommand("task-cli");
        var adddCommand = new Command("add", "Add a new task");
        adddCommand.AddAlias("a");
        var listCommand = new Command("list", "List all tasks");
        listCommand.AddAlias("l");
        var removeCommand = new Command("remove", "Remove a task");
        removeCommand.AddAlias("r");
        var updateCommand = new Command("update", "Update a task");
        updateCommand.AddAlias("u");

        rootCommand.AddCommand(adddCommand);
        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(removeCommand);
        rootCommand.AddCommand(updateCommand);



    }
}
