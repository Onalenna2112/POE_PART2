using System;

namespace POE_PART2
{
    internal class InputHandler
    {
        // Validate user input
        public static bool IsValidInput(string input)
        {
            // Check for empty input
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return true;
        }

        // Clean user input
        public static string CleanInput(string input)
        {
            return input.Trim().ToLower();
        }
    }
}
