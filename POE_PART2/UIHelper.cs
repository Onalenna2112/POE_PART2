using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace POE_PART2
{
    internal class UIHelper
    {
        // ASCII Art Banner
        public static string GetAsciiArt()
        {
            return @"

 ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
╚██████╗   ██║   ██████╔╝███████╗██║  ██║
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝

     CYBERSECURITY AWARENESS BOT

";
        }

        // Typing Effect
        public static void TypeText(RichTextBox chatBox, string message, Color color)
        {
            chatBox.SelectionColor = color;

            foreach (char character in message)
            {
                chatBox.AppendText(character.ToString());

                Application.DoEvents();

                Thread.Sleep(15);
            }

            chatBox.AppendText(Environment.NewLine + Environment.NewLine);
        }

        // Divider Line
        public static string Divider()
        {
            return "══════════════════════════════════════════════";
        }
    }
}
