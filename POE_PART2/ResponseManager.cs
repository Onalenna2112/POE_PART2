using System;
using System.Collections.Generic;

namespace POE_PART2
{
    internal class ResponseManager
    {
        // Random generator
        private static Random random = new Random();

        // Dictionary storing keywords and responses
        private static Dictionary<string, List<string>> keywordResponses =
            new Dictionary<string, List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "✨ Use strong passwords with symbols, numbers and uppercase letters.",
                    "🔐 Never reuse the same password for multiple accounts.",
                    "💜 Password managers can help you store passwords safely."
                }
            },

            {
                "phishing",
                new List<string>()
                {
                    "⚠ Be careful of suspicious email links.",
                    "📧 Scammers often pretend to be trusted companies.",
                    "💜 Always verify the sender's email address carefully."
                }
            },

            {
                "privacy",
                new List<string>()
                {
                    "🔒 Review your privacy settings regularly.",
                    "✨ Avoid sharing personal information publicly online.",
                    "💜 Two-factor authentication improves account security."
                }
            },

            {
                "scam",
                new List<string>()
                {
                    "⚠ Never share OTP codes with strangers.",
                    "💸 Online scams often create panic to trick victims.",
                    "✨ Verify messages before sending money online."
                }
            },

            {
                "malware",
                new List<string>()
                {
                    "🦠 Malware can damage your computer and steal information.",
                    "⚠ Avoid downloading files from unknown websites.",
                    "💜 Install trusted antivirus software for protection."
                }
            },

            {
                "vpn",
                new List<string>()
                {
                    "🌐 VPNs help protect your online privacy.",
                    "🔒 A VPN encrypts your internet connection.",
                    "✨ VPNs are useful when using public Wi-Fi."
                }
            },

            {
                "social engineering",
                new List<string>()
                {
                    "⚠ Social engineering attacks manipulate people into revealing sensitive information.",
                    "💜 Always verify who you are communicating with online.",
                    "🔒 Never share personal details with strangers."
                }
            },

            {
                "safe browsing",
                new List<string>()
                {
                    "🌐 Only visit trusted websites.",
                    "🔒 Check that websites use HTTPS before entering personal information.",
                    "⚠ Avoid downloading files from unknown sources."
                }
            }
        };

        // Detect keyword from user input
        public static string DetectKeyword(string input)
        {
            input = input.ToLower();

            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    return keyword;
                }
            }

            return "";
        }

        // Get random response
        public static string GetRandomResponse(string keyword)
        {
            if (keywordResponses.ContainsKey(keyword))
            {
                List<string> responses = keywordResponses[keyword];

                int index = random.Next(responses.Count);

                return responses[index];
            }

            return "❓ I am not sure I understand. Please try rephrasing.";
        }
    }
}