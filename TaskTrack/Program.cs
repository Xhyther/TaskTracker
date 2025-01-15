using System.CommandLine;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TaskTrack;

class Program
{
    private static int _TaskId = 0;

    public static async Task Main(string[] args)
    {
        EnsureJsonFileExists();
        
       //Root Command
        var rootCommand = new RootCommand("task-cli");

        //Defining the subCommands
        var addCommand = new Command("add", "Add a new task")
        {
            new Argument<string> (name: "Name", description: "Name of the task"),
            new Argument<string> (name: "Description", description: "Description of the task"),
            
            new Option<State>(
            aliases: new string[] { "--state", "-s" },
            description: "State of the task (default: NotStarted)",
            getDefaultValue: () => State.NotStarted // Default value for the option
            ),

            new Option<Priority>(
            aliases: new string[] { "--priority", "-p" },
            description: "Priority of the task (default: Low)",
            getDefaultValue: () => Priority.Low // Default value for the option
            )

        };
        addCommand.AddAlias("a");

        var listCommand = new Command("list", "List all tasks")
        {
            new Option<string>(new string[] {"--state", "-s"}, "State of the task (default: all)"),
            new Option<string>(new string[] {"--priority", "-p"}, "Priority of the task (default: all)")

        };
        listCommand.AddAlias("l");

        var removeCommand = new Command("remove", "Remove a task")
        {
            new Argument<string> (name: "Name", description: "Name of the task"),
            new Argument<int> (name: "TaskId", description: "ID of the task")
        };
        removeCommand.AddAlias("r");

        var updateCommand = new Command("update", "Update a task")
        {
            new Argument<int> (name: "TaskId", description: "ID of the task"),
            new Argument<string> (name: "Name", description: "Name of the task"),
            new Argument<string> (name: "Description", description: "Description of the task"),
            new Option<State>(
            aliases: new string[] { "--state", "-s" },
            description: "State of the task (default: NotStarted)",
            getDefaultValue: () => State.NotStarted // Default value for the option
            ),

            new Option<Priority>(
            aliases: new string[] { "--priority", "-p" },
            description: "Priority of the task (default: Low)",
            getDefaultValue: () => Priority.Low // Default value for the option
            )


        };
        updateCommand.AddAlias("u");


        addCommand.SetHandler((string name, string description, State state, Priority priority) => 
        {
           var task = new Tasks(_TaskId++, name, description, state, priority);
            

        });

        rootCommand.AddCommand(addCommand);
        rootCommand.AddCommand(listCommand);
        rootCommand.AddCommand(removeCommand);
        rootCommand.AddCommand(updateCommand);

        await rootCommand.InvokeAsync(args);
        
    }

    static void EnsureJsonFileExists()
    {
        if (!File.Exists("tasks.json"))
        {
            var defaultContent = new
            {
                tasks = new List<object>()
            };

            File.WriteAllText("tasks.json", JsonSerializer.Serialize(defaultContent, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Created tasks.json file with default structure.");
        }

    }   
}