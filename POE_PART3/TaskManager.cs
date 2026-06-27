using System.Collections.Generic;
using System.Text;

namespace POE_PART2
{
    internal class TaskManager
    {
        // Add a new task and save it straight to the database
        public static void AddTask(string title, string description, string reminder)
        {
            // Always defer directly to your persistent MySQL storage 
            DatabaseManager.AddTask(title, description, reminder);
        }

        // Pulls task data directly from MySQL to prevent loss across application restarts
        public static string ShowTasks()
        {
            List<TaskItem> dbTasks = DatabaseManager.GetAllTasks();

            if (dbTasks == null || dbTasks.Count == 0)
            {
                return "📋 No tasks available.";
            }

            StringBuilder result = new StringBuilder();

            foreach (TaskItem task in dbTasks)
            {
                result.AppendLine($"🔹 Task ID: {task.Id}");
                result.AppendLine($"Title: {task.Title}");
                result.AppendLine($"Description: {task.Description}");

                if (!string.IsNullOrEmpty(task.Reminder))
                {
                    result.AppendLine($"Reminder: {task.Reminder}");
                }

                result.AppendLine("──────────────────");
            }

            return result.ToString();
        }

        // Deletes the task from both database storage layers safely
        public static string DeleteTask(int id)
        {
            // Execute structural deletion against your persistent DB layer
            bool isDeleted = DatabaseManager.RemoveTaskFromDb(id);

            if (isDeleted)
            {
                return $"🗑 Task {id} deleted successfully from database storage.";
            }

            return $"⚠ Task with ID {id} could not be found or failed to delete.";
        }
    }
}