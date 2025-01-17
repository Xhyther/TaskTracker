using System.CommandLine;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.CommandLine.Binding;
using System.CommandLine.Invocation;



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
            new Argument<string>(name: "name", description: "Name of the task"),
            new Argument<string>(name: "description", description: "Description of the task"),
            
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
        addCommand.AddAlias("-a");

        var listCommand = new Command("list", "List all tasks")
        {
            new Option<string>(new string[] {"--state", "-s"}, "State of the task (default: all)"),
            new Option<string>(new string[] {"--priority", "-p"}, "Priority of the task (default: all)")

        };
        listCommand.AddAlias("-l");

        var removeCommand = new Command("remove", "Remove a task")
        {
            new Argument<string> (name: "name", description: "Name of the task"),
            new Argument<int> (name: "TaskId", description: "ID of the task")
        };
        removeCommand.AddAlias("-r");

        var updateCommand = new Command("update", "Update a task")
        {
            new Argument<int> (name: "TaskId", description: "ID of the task"),
            new Argument<string> (name: "name", description: "Name of the task"),
            new Argument<string> (name: "description", description: "Description of the task"),
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
        updateCommand.AddAlias("-u");

        
        

        addCommand.SetHandler( async (InvocationContext context) =>
        {
           
            // Correctly get argument and option values
            var name = context.ParseResult.GetValueForArgument(addCommand.Arguments[0]) as string; // Get name string
            var description = context.ParseResult.GetValueForArgument(addCommand.Arguments[1])as string; // Get description string

            // Convert state and priority to enum types (State and Priority)
            var state = context.ParseResult.GetValueForOption(addCommand.Options[0]);
            var priority = context.ParseResult.GetValueForOption(addCommand.Options[1]);

            
            // Ensure the state and priority are valid enums
            if (!Enum.TryParse(state.ToString(), out State taskState))
            {
                Console.WriteLine($"Invalid state value '{state}', defaulting to 'NotStarted'.");
                taskState = State.NotStarted;  // Fallback default value
            }

            if (!Enum.TryParse(priority.ToString(), out Priority taskPriority))
            {
                Console.WriteLine($"Invalid priority value '{priority}', defaulting to 'Low'.");
                taskPriority = Priority.Low;  // Fallback default value
            }

            // Create the task object with the string values for name and description
            var task = new Tasks(name, description, taskState, taskPriority);

            try
            {
            var tasks = File.Exists("tasks.json")
            ? JsonSerializer.Deserialize<List<Tasks>>(File.ReadAllText("tasks.json")) ?? new List<Tasks>()
            : new List<Tasks>();

             tasks.Add(task);

            // Save tasks back to the JSON file
            await File.WriteAllTextAsync("tasks.json", JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
            }

            Console.WriteLine($"Task '{task.TaskName}' added successfully with state '{task.TaskState}' and priority '{task.TaskPriority}'.");

        });
        
       
   
        listCommand.SetHandler(async (InvocationContext context) =>
        {
            var state = context.ParseResult.GetValueForOption(listCommand.Options[0]);
            var priority = context.ParseResult.GetValueForOption(listCommand.Options[1]);

            var tasks = File.Exists("tasks.json")
            ? JsonSerializer.Deserialize<List<Tasks>>(File.ReadAllText("tasks.json")) ?? new List<Tasks>()
            : new List<Tasks>();

            if (state != null)
            {
                tasks = tasks.Where(t => t.TaskState.ToString().Equals(state)).ToList();
            }

            if (priority != null)
            {
                tasks = tasks.Where(t => t.TaskPriority.ToString().Equals(priority)).ToList();
            }

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task ID: {++_TaskId}");
                    Console.WriteLine($"Name: {task.TaskName}");
                    Console.WriteLine($"Description: {task.TaskDescription}");
                    Console.WriteLine($"State: {task.TaskState}");
                    Console.WriteLine($"Priority: {task.TaskPriority}");
                    Console.WriteLine();
                }
            }
        });

        removeCommand.SetHandler(async (InvocationContext context) =>
        {
            var name = context.ParseResult.GetValueForArgument(removeCommand.Arguments[0]) as string;
            var taskId = context.ParseResult.GetValueForArgument(removeCommand.Arguments[1]) as int?;

            var tasks = File.Exists("tasks.json")
            ? JsonSerializer.Deserialize<List<Tasks>>(File.ReadAllText("tasks.json")) ?? new List<Tasks>()
            : new List<Tasks>();

            var task = tasks.FirstOrDefault(t => t.TaskName.Equals(name) && Tasks.TaskId == taskId);

            if (task == null)
            {
                Console.WriteLine($"Task '{name}' with ID '{taskId}' not found.");
            }
            else
            {
                tasks.Remove(task);
                await File.WriteAllTextAsync("tasks.json", JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"Task '{name}' with ID '{taskId}' removed successfully.");
            }
        });
       
        updateCommand.SetHandler(async (InvocationContext context) =>
        {
            var taskId = context.ParseResult.GetValueForArgument(updateCommand.Arguments[0]) as int?;
            var name = context.ParseResult.GetValueForArgument(updateCommand.Arguments[1]) as string;
            var description = context.ParseResult.GetValueForArgument(updateCommand.Arguments[2]) as string;

            var state = context.ParseResult.GetValueForOption(updateCommand.Options[0]);
            var priority = context.ParseResult.GetValueForOption(updateCommand.Options[1]);

            var tasks = File.Exists("tasks.json")
            ? JsonSerializer.Deserialize<List<Tasks>>(File.ReadAllText("tasks.json")) ?? new List<Tasks>()
            : new List<Tasks>();

            var task = tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task == null)
            {
                Console.WriteLine($"Task with ID '{taskId}' not found.");
            }
            else
            {
                task.TaskName = name;
                task.TaskDescription = description;

                if (Enum.TryParse(state.ToString(), out State taskState))
                {
                    task.TaskState = taskState;
                }

                if (Enum.TryParse(priority.ToString(), out Priority taskPriority))
                {
                    task.TaskPriority = taskPriority;
                }

                await File.WriteAllTextAsync("tasks.json", JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"Task with ID '{taskId}' updated successfully.");
            }
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