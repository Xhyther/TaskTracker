# Task Tracker CLI

Task Tracker is a command-line interface (CLI) application that helps you track and manage your tasks. With this tool, you can organize your tasks into different states, such as "to do," "in progress," and "done." This project is an excellent way to practice programming skills like working with the filesystem, handling user inputs, and building a simple CLI application.

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

## Requirements
The application must meet the following requirements:

1. **Command-Line Execution**:
   - Run the application from the command line.
   - Accept user actions and inputs as arguments.
2. **Task Storage**:
   - Store tasks in a JSON file in the current directory.
   - Create the JSON file automatically if it does not exist.
3. **Constraints**:
   - Use the native filesystem module of your programming language to interact with the JSON file.
   - Do not use external libraries or frameworks to build the project.
4. **Error Handling**:
   - Handle errors and edge cases gracefully (e.g., invalid task IDs, missing arguments).

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
1. **Programming Language**: You can use any language to build this project.
2. **File Handling**: Use your language's native filesystem module to interact with the `tasks.json` file.
3. **CLI Input Handling**: Use positional arguments to capture user inputs and actions.

## Future Enhancements
- Add due dates or priorities to tasks.
- Support for filtering tasks by date or priority.
- Enable exporting tasks to other formats (e.g., CSV).
- Add support for task categories.

---

This project is an excellent starting point for building robust CLI tools while learning about user input handling, file operations, and application design!

