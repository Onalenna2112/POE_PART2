namespace POE_PART2
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Reminder { get; set; }

        // Track whether the user has completed this cybersecurity task
        public bool IsCompleted { get; set; } = false;
    }
}