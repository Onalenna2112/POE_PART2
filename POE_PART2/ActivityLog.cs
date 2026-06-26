using System;
using System.Collections.Generic;
using System.Text;

namespace POE_PART2
{
    internal class ActivityLog
    {
        // One centralized list for all system and user interactions
        private static readonly List<string> logs = new List<string>();

        // Add an action item with a timestamp
        public static void Add(string action)
        {
            logs.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {action}");
        }

        // Centralized summary generation used by both "show log" and "what have you done for me"
        public static string ShowRecent()
        {
            if (logs.Count == 0)
            {
                return "📋 No actions have been performed yet.";
            }

            // Fetch the last 5-10 actions to keep it concise as requested by the rubric
            int targetCount = 5;
            int start = Math.Max(0, logs.Count - targetCount);
            int countToFetch = logs.Count - start;

            List<string> recentLogs = logs.GetRange(start, countToFetch);
            StringBuilder summary = new StringBuilder("📋 Here's a summary of recent actions:\n\n");

            int displayIndex = 1;
            foreach (string log in recentLogs)
            {
                summary.AppendLine($"{displayIndex}. {log}");
                displayIndex++;
            }

            return summary.ToString();
        }
    }
}