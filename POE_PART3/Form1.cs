using Microsoft.VisualBasic;
using POE_PART2;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POE_PART3
{
    public partial class Form1 : Form
    {
        private string historyPath = Path.Combine(Application.StartupPath, "history.txt");

        public Form1()
        {
            InitializeComponent();

            // Play greeting audio
            AudioPlayer.PlayGreeting();

            // Display ASCII art welcome message
            DisplayWelcomeMessage();
        }

        // Welcome display layout configuration
        private void DisplayWelcomeMessage()
        {
            rtbChat.Clear();

            rtbChat.SelectionColor = Color.Cyan;
            rtbChat.AppendText(UIHelper.GetAsciiArt());
            rtbChat.AppendText(UIHelper.Divider());
            rtbChat.AppendText(Environment.NewLine);

            UIHelper.TypeText(rtbChat, "💜 Welcome to the Cybersecurity Awareness Bot!", Color.Pink);
            UIHelper.TypeText(rtbChat, "✨ Ask me about passwords, scams, phishing, privacy, malware or VPNs.", Color.White);

            rtbChat.AppendText(Environment.NewLine);
        }

        // SEND MESSAGE BUTTON HANDLER
        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            // Validate user textual input
            if (!InputHandler.IsValidInput(input))
            {
                MessageBox.Show(
                    "⚠ Please enter a message.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Display user message to history rich text panel
            DisplayUserMessage(input);

            // Fetch processed data matrix back from NLP/Keyword system
            string response = Chatbot.GetResponse(input);

            // Run typewriter thread for clean bot display
            DisplayBotMessage(response);

            // Flush out active entry container box
            txtUserInput.Clear();

            // Safely wind down the application container if explicitly commanded
            if (input.ToLower().Contains("exit"))
            {
                Application.Exit();
            }
        }

        private void DisplayUserMessage(string message)
        {
            rtbChat.SelectionColor = Color.White;
            rtbChat.AppendText($"🧑 You: {message}{Environment.NewLine}{Environment.NewLine}");

            try
            {
                File.AppendAllText(historyPath, $"USER: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Gracefully catch potential file access locks without interrupting user flow
                System.Diagnostics.Debug.WriteLine($"History append fail: {ex.Message}");
            }
        }

        private void DisplayBotMessage(string message)
        {
            UIHelper.TypeText(rtbChat, $"🤖 CyberBot: {message}", Color.Cyan);

            try
            {
                File.AppendAllText(historyPath, $"BOT: {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"History append fail: {ex.Message}");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DisplayWelcomeMessage();
        }

        private void btnTip_Click(object sender, EventArgs e)
        {
            string[] tips =
            {
                "🔐 Use strong passwords with symbols and numbers.",
                "📧 Never click suspicious email links.",
                "🌐 Avoid using public Wi-Fi without a VPN.",
                "🦠 Keep your antivirus software updated.",
                "💜 Enable two-factor authentication for extra security."
            };

            Random random = new Random();
            int index = random.Next(tips.Length);

            DisplayBotMessage(tips[index]);
        }

        private void btnTopics_Click(object sender, EventArgs e)
        {
            DisplayBotMessage(
                "📚 Available Topics:\n\n" +
                "• Password Safety\n" +
                "• Phishing\n" +
                "• Online Scams\n" +
                "• Privacy\n" +
                "• Malware\n" +
                "• VPNs\n" +
                "• Safe Browsing\n" +
                "• Social Engineering");
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(historyPath))
            {
                string history = File.ReadAllText(historyPath);
                DisplayBotMessage(history);
            }
            else
            {
                DisplayBotMessage("⚠ No chat history found.");
            }
        }

        // INTERACTIVE QUIZ ENGINE - INTEGRATED WITH QUIZMANAGER & ACTIVITYLOG
        private void btnQuiz_Click(object sender, EventArgs e)
        {
            // Initialize session state
            QuizManager.StartQuiz();

            DisplayBotMessage("🎮 Starting your Cybersecurity Quiz Challenge! Please follow the popup prompts...");

            for (int i = 0; i < QuizManager.Questions.Count; i++)
            {
                QuizQuestion currentQ = QuizManager.Questions[i];

                // Dynamically guide user response syntax based on question position
                string promptGuidance = (i < 6)
                    ? "\n\nType your choice (A, B, C, or D):"
                    : "\n\nType True or False (or T/F):";

                // Request user response input string safely
                string rawInput = Interaction.InputBox(
                    currentQ.Question + promptGuidance,
                    $"Quiz Question {i + 1} of {QuizManager.Questions.Count}"
                );

                // If user hits 'Cancel' or escapes the prompt box, abort quiz progression cleanly
                if (string.IsNullOrEmpty(rawInput))
                {
                    DisplayBotMessage("⚠ Quiz session abandoned by user.");
                    ActivityLog.Add("Quiz session canceled prematurely by user.");
                    return;
                }

                // Push submission back down to management layer for safe evaluation
                bool wasCorrect = QuizManager.SubmitAnswer(rawInput);

                if (wasCorrect)
                {
                    MessageBox.Show("✅ Correct answer!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ Incorrect. The right answer was: {currentQ.Answer.ToUpper()}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            // Generate feedback summary directly derived from metrics
            int finalScore = QuizManager.Score;
            int totalQuestions = QuizManager.Questions.Count;

            DisplayBotMessage($"🎯 Quiz Complete! Your final score: {finalScore}/{totalQuestions}");

            if (finalScore == totalQuestions)
            {
                DisplayBotMessage("🏆 Flawless Victory! You possess elite cybersecurity defense instincts!");
            }
            else if (finalScore >= (totalQuestions * 0.75))
            {
                DisplayBotMessage("✨ Excellent job! Your knowledge provides strong defense coverage online.");
            }
            else if (finalScore >= (totalQuestions * 0.5))
            {
                DisplayBotMessage("👍 Good effort! Review the bot topics to reinforce weak structural points.");
            }
            else
            {
                DisplayBotMessage("⚠ Critical Vulnerability Alert! Please carefully study system topics to secure your device infrastructure.");
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}