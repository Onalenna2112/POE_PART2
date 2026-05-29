using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POE_PART2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Play greeting audio
            AudioPlayer.PlayGreeting();

            // Display ASCII art
            DisplayWelcomeMessage();
        }

        // Welcome display
        private void DisplayWelcomeMessage()
        {
            rtbChat.Clear();

            rtbChat.SelectionColor = Color.Cyan;

            rtbChat.AppendText(UIHelper.GetAsciiArt());

            rtbChat.AppendText(UIHelper.Divider());

            rtbChat.AppendText(Environment.NewLine);

            UIHelper.TypeText(
                rtbChat,
                "💜 Welcome to the Cybersecurity Awareness Bot!",
                Color.Pink);

            UIHelper.TypeText(
                rtbChat,
                "✨ Ask me about passwords, scams, phishing, privacy, malware or VPNs.",
                Color.White);

            rtbChat.AppendText(Environment.NewLine);
        }

        // SEND BUTTON
        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = txtUserInput.Text;

            // Validate input
            if (!InputHandler.IsValidInput(input))
            {
                MessageBox.Show(
                    "⚠ Please enter a message.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Display user message
            DisplayUserMessage(input);

            // Get chatbot response
            string response = Chatbot.GetResponse(input);

            // Display bot response
            DisplayBotMessage(response);

            // Clear textbox
            txtUserInput.Clear();

            // Exit app if user types exit
            if (input.ToLower().Contains("exit"))
            {
                Application.Exit();
            }
        }

        // USER MESSAGE
        private void DisplayUserMessage(string message)
        {
            rtbChat.SelectionColor = Color.White;

            rtbChat.AppendText(
                "🧑 You: " + message + Environment.NewLine + Environment.NewLine);
            File.AppendAllText(
    Application.StartupPath + @"\history.txt",
    "USER: " + message + Environment.NewLine);
        }

        // BOT MESSAGE
        private void DisplayBotMessage(string message)
        {
            UIHelper.TypeText(
                rtbChat,
                "🤖 CyberBot: " + message,
                Color.Cyan);
            File.AppendAllText(
    Application.StartupPath + @"\history.txt",
    "BOT: " + message + Environment.NewLine);
        }

        // CLEAR CHAT BUTTON
        private void btnClear_Click(object sender, EventArgs e)
        {
            DisplayWelcomeMessage();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
@"📚 Available Topics:

• Password Safety
• Phishing
• Online Scams
• Privacy
• Malware
• VPNs
• Safe Browsing
• Social Engineering");
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            string historyPath =
    Application.StartupPath + @"\history.txt";

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

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            int score = 0;

            // QUESTION 1
            DialogResult q1 =
                MessageBox.Show(
                "❓ Is it safe to share your password with friends?",
                "Question 1",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (q1 == DialogResult.No)
            {
                score++;
            }

            // QUESTION 2
            DialogResult q2 =
                MessageBox.Show(
                "❓ Should you click suspicious email links?",
                "Question 2",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (q2 == DialogResult.No)
            {
                score++;
            }

            // QUESTION 3
            DialogResult q3 =
                MessageBox.Show(
                "❓ Is two-factor authentication useful?",
                "Question 3",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (q3 == DialogResult.Yes)
            {
                score++;
            }

            // QUESTION 4
            DialogResult q4 =
                MessageBox.Show(
                "❓ Should you use the same password everywhere?",
                "Question 4",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (q4 == DialogResult.No)
            {
                score++;
            }

            // QUESTION 5
            DialogResult q5 =
                MessageBox.Show(
                "❓ Is public Wi-Fi always secure?",
                "Question 5",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (q5 == DialogResult.No)
            {
                score++;
            }

            // FINAL RESULT
            DisplayBotMessage(
                $"🎯 Quiz Complete! Your score: {score}/5");

            // PERFORMANCE FEEDBACK
            if (score == 5)
            {
                DisplayBotMessage(
                    "🏆 Excellent! You're very cyber-aware!");
            }
            else if (score >= 3)
            {
                DisplayBotMessage(
                    "✨ Good job! Your cybersecurity knowledge is improving.");
            }
            else
            {
                DisplayBotMessage(
                    "⚠ You should learn more about cybersecurity safety.");
            }
        }
    }
}