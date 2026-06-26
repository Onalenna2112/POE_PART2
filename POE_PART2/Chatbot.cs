using System;

namespace POE_PART2
{
    internal class Chatbot
    {
        // Main chatbot processing method
        public static string GetResponse(string input)
        {
            // Clean input
            input = InputHandler.CleanInput(input);

            // 1. Detect sentiment & emotional responses first
            string sentiment = SentimentAnalyzer.DetectSentiment(input);
            string emotionResponse = SentimentAnalyzer.GetSentimentResponse(sentiment);

            if (!string.IsNullOrEmpty(emotionResponse))
            {
                return emotionResponse;
            }

            // 2. Command Processing: Exit Handling
            if (input.Contains("exit"))
            {
                ActivityLog.Add("User exited the chatbot.");
                return "👋 Goodbye! Stay safe online.";
            }

            // 3. Command Processing: Greetings
            if (input.Contains("hello") ||
                input.Contains("hi") ||
                input.Contains("hey") ||
                input.Contains("good morning") ||
                input.Contains("good afternoon"))
            {
                ActivityLog.Add("User greeted the chatbot.");
                return "✨ Hello! How can I help you with cybersecurity today?";
            }

            // 4. Command Processing: Activity Log Reporting (Task 4)
            if (input.Contains("activity log") ||
                input.Contains("show log") ||
                input.Contains("show history") ||
                input.Contains("what have you done for me") ||
                input.Contains("summary"))
            {
                return ActivityLog.ShowRecent();
            }

            // 5. Command Processing: Task & Reminder Operations (Task 1 & Task 3 Simulation)
            if (input.Contains("add task") ||
                input.Contains("add a task") ||
                input.Contains("create task") ||
                input.Contains("new task"))
            {
                string task = input.Replace("add a task to", "")
                                   .Replace("add task to", "")
                                   .Replace("add task", "")
                                   .Replace("create task", "")
                                   .Replace("new task", "")
                                   .Replace("-", "")
                                   .Trim();

                string description = "Cybersecurity task created by user.";

                if (task.Contains("2fa") || task.Contains("two-factor"))
                {
                    description = "Enable two-factor authentication to strengthen account security.";
                }
                else if (task.Contains("privacy"))
                {
                    description = "Review account privacy settings to ensure your data is protected.";
                }

                // Database operation
                DatabaseManager.AddTask(task, description, "");

                // Logging action to activity history
                ActivityLog.Add($"Task added: '{task}' (no reminder set).");

                return $"Task added with the description \"{description}\" Would you like a reminder?";
            }

            // Reminder Processing
            if (input.Contains("tomorrow"))
            {
                string task = input.Replace("remind me to", "")
                                   .Replace("tomorrow", "")
                                   .Trim();

                ActivityLog.Add($"Reminder set for '{task}' on tomorrow's date.");
                return $"⏰ Reminder set for '{task}' on tomorrow's date.";
            }

            if (input.Contains("remind me in"))
            {
                string reminder = input.Replace("remind me in", "").Trim();
                ActivityLog.Add($"Reminder set for task in {reminder}.");
                return $"Got it! I'll remind you in {reminder}.";
            }

            if (input.Contains("show tasks") || input.Contains("view tasks"))
            {
                return TaskManager.ShowTasks();
            }

            if (input.StartsWith("delete task"))
            {
                try
                {
                    int id = Convert.ToInt32(input.Replace("delete task", "").Trim());
                    ActivityLog.Add($"Deleted task ID: {id}");
                    return TaskManager.DeleteTask(id);
                }
                catch
                {
                    return "⚠ Please enter a valid task number.";
                }
            }

            // 6. Memory Profiles
            if (input.StartsWith("my name is"))
            {
                string name = input.Replace("my name is", "").Trim();
                MemoryManager.SaveName(name);
                ActivityLog.Add($"Saved user name: {name}");
                return $"💜 Nice to meet you, {name}!";
            }

            if (input.Contains("interested in"))
            {
                string topic = input.Replace("i am interested in", "")
                                    .Replace("interested in", "")
                                    .Trim();

                MemoryManager.SaveTopic(topic);
                MemoryManager.SaveLastTopic(topic);
                return $"✨ I'll remember that you're interested in {topic}.";
            }

            // 7. General Cyber Awareness Knowledge Keywords
            string keyword = ResponseManager.DetectKeyword(input);
            if (!string.IsNullOrEmpty(keyword))
            {
                MemoryManager.SaveLastTopic(keyword);
                ActivityLog.Add($"Discussed topic: {keyword}");

                string response = ResponseManager.GetRandomResponse(keyword);
                string memoryRecall = MemoryManager.RecallMemory();

                return response + Environment.NewLine + memoryRecall;
            }

            // Quiz Redirection
            if (input.Contains("quiz") || input.Contains("test my knowledge") || input.Contains("challenge me"))
            {
                return "📝 Click the QUIZ button to test your cybersecurity knowledge!";
            }

            // Context navigation helpers
            if (input.Contains("tell me more") || input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(MemoryManager.LastTopic))
                {
                    ActivityLog.Add($"Requested more information about {MemoryManager.LastTopic}");
                    return ResponseManager.GetRandomResponse(MemoryManager.LastTopic);
                }
                return "✨ Could you tell me which topic you'd like to know more about?";
            }

            if (input.Contains("purpose"))
            {
                return "💜 My purpose is to help users stay safe online.";
            }

            if (input.Contains("how are you"))
            {
                return "✨ I'm functioning perfectly and ready to help!";
            }

            if (input.Contains("topics") || input.Contains("what can you help with") || input.Contains("what do you know"))
            {
                return @"📚 Available Topics:
• Password Safety
• Phishing
• Online Scams
• Privacy
• Malware
• VPNs
• Safe Browsing
• Social Engineering";
            }

            if (input.Contains("tip") || input.Contains("advice") || input.Contains("help me stay safe"))
            {
                string[] tips = {
                    "🔐 Use strong passwords with symbols and numbers.",
                    "📧 Never click suspicious email links.",
                    "🌐 Avoid public Wi-Fi without a VPN.",
                    "🦠 Keep your antivirus updated.",
                    "💜 Enable two-factor authentication."
                };

                Random random = new Random();
                return tips[random.Next(tips.Length)];
            }

            // Default Fallback
            return "🤔 I didn't quite understand that. Could you rephrase?\n\nTry saying:\n• Add a task to enable 2FA\n• Remind me to update my password tomorrow\n• Show activity log";
        }
    }
}