using System;

namespace POE_PART2
{
    internal class SentimentAnalyzer
    {
        // Detect user emotion
        public static string DetectSentiment(string input)
        {
            input = input.ToLower();

            // Worried
            if (input.Contains("worried") ||
                input.Contains("scared") ||
                input.Contains("anxious"))
            {
                return "worried";
            }

            // Frustrated
            if (input.Contains("frustrated") ||
                input.Contains("annoyed") ||
                input.Contains("angry"))
            {
                return "frustrated";
            }

            // Confused
            if (input.Contains("confused") ||
                input.Contains("lost") ||
                input.Contains("don't understand"))
            {
                return "confused";
            }

            // Happy
            if (input.Contains("happy") ||
                input.Contains("excited") ||
                input.Contains("great"))
            {
                return "happy";
            }

            return "neutral";
        }

        // Generate emotional response
        public static string GetSentimentResponse(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "💜 It's okay to feel worried. Cybersecurity can be overwhelming sometimes.";

                case "frustrated":
                    return "💜 I understand your frustration. Let's solve this step by step together.";

                case "confused":
                    return "✨ Don't worry — cybersecurity takes time to learn. Ask me anything.";

                case "happy":
                    return "✨ I'm glad you're feeling positive today!";

                default:
                    return "";
            }
        }
    }
}
