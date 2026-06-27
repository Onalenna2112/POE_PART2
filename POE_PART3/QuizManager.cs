using System.Collections.Generic;

namespace POE_PART2
{
    internal class QuizManager
    {
        public static int CurrentQuestionIndex { get; set; } = 0;
        public static int Score { get; set; } = 0;

        // Expanded question repository (12 questions: 6 Multiple Choice, 6 True/False)
        public static List<QuizQuestion> Questions = new List<QuizQuestion>()
        {
            // === MULTIPLE CHOICE QUESTIONS (Answers: a, b, c...) ===
            new QuizQuestion(
                "1. What software helps detect, block, and clean computer viruses?\n\nA) File Explorer\nB) Antivirus software\nC) Web Browser\nD) Firewall Controller",
                "b"),

            new QuizQuestion(
                "2. Which technique relies on manipulating human psychology to trick users into handing over confidential data?\n\nA) Network Overclocking\nB) Firewalls\nC) Social Engineering\nD) Database Indexing",
                "c"),

            new QuizQuestion(
                "3. What does '2FA' stand for in account security?\n\nA) Two-Factor Authentication\nB) Second File Archive\nC) Twin Firewall Application\nD) Two-Way File Allocation",
                "a"),

            new QuizQuestion(
                "4. What is malware?\n\nA) Computer hardware accessories\nB) Harmful software designed to exploit or damage devices\nC) A web browser extension\nD) A cloud backup solution",
                "b"),

            new QuizQuestion(
                "5. What is the most secure way to manage your passwords?\n\nA) Write them on a sticky note\nB) Use the same password for everything\nC) Use a secure password manager tool\nD) Keep them in a plain text file",
                "c"),

            new QuizQuestion(
                "6. What should you do if you receive an unexpected email from your 'bank' asking you to update your PIN immediately?\n\nA) Reply with your information\nB) Click the link provided in the email\nC) Ignore it and call your bank directly using their official number\nD) Forward it to your friends",
                "c"),

            // === TRUE / FALSE QUESTIONS (Answers: true / false) ===
            new QuizQuestion(
                "7. True or False: Phishing attacks usually happen via deceptive emails, messages, or fake websites.",
                "true"),

            new QuizQuestion(
                "8. True or False: Two-factor authentication (2FA) guarantees your password can never be guessed.",
                "false"),

            new QuizQuestion(
                "9. True or False: A Virtual Private Network (VPN) helps protect your online activity and data when using public Wi-Fi.",
                "true"),

            new QuizQuestion(
                "10. True or False: Public Wi-Fi networks without password requirements are safe for processing online banking transactions.",
                "false"),

            new QuizQuestion(
                "11. True or False: You should safely reuse the same secure password across multiple personal or work accounts.",
                "false"),

            new QuizQuestion(
                "12. True or False: It is safe to click links inside unexpected text messages from numbers you don't recognize.",
                "false")
        };

        public static void StartQuiz()
        {
            CurrentQuestionIndex = 0;
            Score = 0;
            ActivityLog.Add("Quiz started - multi-choice/TF validation challenge initiated.");
        }

        public static bool SubmitAnswer(string userAnswer)
        {
            if (CurrentQuestionIndex >= Questions.Count) return false;

            string correctAnswer = Questions[CurrentQuestionIndex].Answer.Trim().ToLower();
            string cleanedUserAnswer = userAnswer.Trim().ToLower();

            // Normalize "t" as "true" and "f" as "false" to be user-friendly
            if (cleanedUserAnswer == "t") cleanedUserAnswer = "true";
            if (cleanedUserAnswer == "f") cleanedUserAnswer = "false";

            bool isCorrect = (cleanedUserAnswer == correctAnswer);

            if (isCorrect)
            {
                Score++;
            }

            CurrentQuestionIndex++;

            if (CurrentQuestionIndex >= Questions.Count)
            {
                ActivityLog.Add($"Quiz completed. Final score tracked: {Score}/{Questions.Count}");
            }

            return isCorrect;
        }
    }
}