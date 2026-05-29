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

            // Detect sentiment
            string sentiment = SentimentAnalyzer.DetectSentiment(input);

            string emotionResponse =
                SentimentAnalyzer.GetSentimentResponse(sentiment);

            // Return emotional response if detected
            if (!string.IsNullOrEmpty(emotionResponse))
            {
                return emotionResponse;
            }

            // Exit handling
            if (input.Contains("exit"))
            {
                return "👋 Goodbye! Stay safe online.";
            }

            // Greeting
            if (input.Contains("hello") ||
                input.Contains("hi"))
            {
                return "✨ Hello! How can I help you with cybersecurity today?";
            }

            // Save user name
            if (input.StartsWith("my name is"))
            {
                string name =
                    input.Replace("my name is", "").Trim();

                MemoryManager.SaveName(name);

                return $"💜 Nice to meet you, {name}!";
            }

            // Save favourite topic
            if (input.Contains("interested in"))
            {
                string[] parts =
                    input.Split(new string[] { "interested in" },
                    StringSplitOptions.None);

                if (parts.Length > 1)
                {
                    string topic = parts[1].Trim();

                    MemoryManager.SaveTopic(topic);

                    return $"✨ I'll remember that you're interested in {topic}.";
                }
            }

            // Follow-up conversation
            if (input.Contains("tell me more") ||
                input.Contains("another tip"))
            {
                if (!string.IsNullOrEmpty(MemoryManager.LastTopic))
                {
                    return ResponseManager.GetRandomResponse(
                        MemoryManager.LastTopic);
                }

                return "✨ Could you tell me which topic you'd like to know more about?";
            }

            // Detect keyword
            string keyword =
                ResponseManager.DetectKeyword(input);

            // If keyword found
            if (!string.IsNullOrEmpty(keyword))
            {
                // Save last topic
                MemoryManager.SaveLastTopic(keyword);

                string response =
                    ResponseManager.GetRandomResponse(keyword);

                // Add memory recall if available
                string memoryRecall =
                    MemoryManager.RecallMemory();

                return response + Environment.NewLine +
                       memoryRecall;
            }

            // Purpose Question
            if (input.Contains("purpose"))
            {
                return "💜 My purpose is to help users stay safe online.";
            }

            // How are you
            if (input.Contains("how are you"))
            {
                return "✨ I'm functioning perfectly and ready to help!";
            }

            // Default response
            return "❓ I didn't fully understand that. Please try rephrasing your question.";
        }
    }
}
