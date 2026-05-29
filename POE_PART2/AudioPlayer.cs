using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace POE_PART2
{
    internal class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    string path = Application.StartupPath + @"\Assets\greeting.wav";

                    SoundPlayer player = new SoundPlayer(path);

                    player.Load();

                    player.Play();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("⚠ Greeting audio could not be played.");
            }
        }
    }
}