using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace POE_PART2
{
    internal class DatabaseManager
    {
        // Change Pwd to your local MySQL password
        private static string connString = "Server=localhost;Database=CybersecurityBotDB;Uid=root;Pwd=YourPassword;";

        public static void InitializeDatabase()
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string createTable = @"
                    CREATE TABLE IF NOT EXISTS cyber_tasks (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        title VARCHAR(255) NOT NULL,
                        description TEXT,
                        reminder VARCHAR(100),
                        reminder_date DATETIME NULL
                    );";
                    using (var cmd = new MySqlCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Initialization Failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AddTask(string title, string description, string reminder)
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    string query = @"INSERT INTO cyber_tasks (title, description, reminder) 
                                     VALUES (@title, @description, @reminder)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@reminder", reminder);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<TaskItem> GetAllTasks()
        {
            var list = new List<TaskItem>();
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, reminder FROM cyber_tasks";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TaskItem
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Title = reader["title"].ToString(),
                                Description = reader["description"].ToString(),
                                Reminder = reader["reminder"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve tasks: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list;
        }

        // Added method to support your Chatbot class delete operations
        public static bool RemoveTaskFromDb(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "DELETE FROM cyber_tasks WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete task: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}