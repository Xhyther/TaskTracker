# Task Tracker CLI

Task Tracker is a command-line interface (CLI) application built in **C#** to help you track and manage your tasks. With this tool, you can organize your tasks into different states, such as "to do," "in progress," and "done." This project demonstrates working with the filesystem, handling user inputs, and building a CLI application using C#.

## Features
The Task Tracker CLI application provides the following functionalities:

- **Add Tasks**: Create new tasks with descriptions.
- **Update Tasks**: Modify task descriptions.
- **Delete Tasks**: Remove tasks permanently.
- **Change Task Status**:
  - Mark a task as "in progress."
  - Mark a task as "done."
- **List Tasks**:
  - List all tasks.
  - List tasks by status (e.g., done, to do, in progress).



## Usage
Below is the list of commands and their usage:

### Adding a New Task
```bash
task-cli add "Buy groceries"
# Output: Task added successfully (ID: 1)
```

### Updating and Deleting Tasks
```bash
task-cli update 1 "Buy groceries and cook dinner"
task-cli delete 1
```

### Marking a Task as In Progress or Done
```bash
task-cli mark-in-progress 1
task-cli mark-done 1
```

### Listing All Tasks
```bash
task-cli list
```

### Listing Tasks by Status
```bash
task-cli list done
task-cli list todo
task-cli list in-progress
```

## Example Task Workflow
1. Add a task:
   ```bash
   task-cli add "Complete project documentation"
   ```
   Output:
   ```
   Task added successfully (ID: 1)
   ```
2. Update the task:
   ```bash
   task-cli update 1 "Complete and review project documentation"
   ```
   Output:
   ```
   Task 1 updated successfully.
   ```
3. Mark the task as "in progress":
   ```bash
   task-cli mark-in-progress 1
   ```
   Output:
   ```
   Task 1 marked as in-progress.
   ```
4. List all tasks:
   ```bash
   task-cli list
   ```
   Output:
   ```
   ID: 1, Description: Complete and review project documentation, Status: in-progress
   ```

## Implementation Details

### File Storage
- The tasks are stored in a JSON file named `tasks.json` in the current directory.
- The structure of the JSON file is as follows:
  ```json
  {
    "tasks": [
      { "id": 1, "description": "Buy groceries", "status": "todo" },
      { "id": 2, "description": "Cook dinner", "status": "done" }
    ]
  }
  ```

### Development Notes
1. **Programming Language**: The project is implemented in **C#**.
2. **File Handling**: Used `System.IO` for interacting with the `tasks.json` file.
3. **CLI Input Handling**: Used `System.CommandLine` for parsing user inputs and actions.

## How to Run
1. Compile the C# code:
   ```bash
   dotnet build
   ```
2. Run the CLI application directly:
   ```bash
   dotnet run -- add "Sample task"
   ```
3. Alternatively, publish the application and add it to your PATH for global execution:
   ```bash
   dotnet publish -c Release -o ./publish
   ./publish/task-cli add "Another task"
   ```

## Future Enhancements
- Add due dates or priorities to tasks.
- Support for filtering tasks by date or priority.
- Enable exporting tasks to other formats (e.g., CSV).
- Add support for task categories.

---

