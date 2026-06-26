namespace POE_PART2
{
    internal class QuizQuestion
    {
        public string Question { get; set; }

        // Renamed from CorrectAnswer to Answer to maintain alignment with your QuizManager class logic
        public string Answer { get; set; }

        public QuizQuestion(string question, string answer)
        {
            Question = question;
            // Trim whitespace alongside converting to lowercase to prevent evaluation failures
            Answer = answer.Trim().ToLower();
        }
    }
}
