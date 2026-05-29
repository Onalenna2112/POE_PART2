using System;

namespace POE_PART2
{
    internal class MemoryManager
    {
        // Stores user's name
        public static string UserName { get; set; } = "";

        // Stores user's favourite cybersecurity topic
        public static string FavouriteTopic { get; set; } = "";

        // Stores previous topic for follow-up conversations
        public static string LastTopic { get; set; } = "";

        // Save user name
        public static void SaveName(string name)
        {
            UserName = name;
        }

        // Save favourite topic
        public static void SaveTopic(string topic)
        {
            FavouriteTopic = topic;
        }

        // Save last discussed topic
        public static void SaveLastTopic(string topic)
        {
            LastTopic = topic;
        }

        // Generate personalised recall message
        public static string RecallMemory()
        {
            if (!string.IsNullOrEmpty(FavouriteTopic))
            {
                return $"💜 Since you're interested in {FavouriteTopic}, this topic may help keep you safer online.";
            }

            return "";
        }
    }
}
