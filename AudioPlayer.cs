using System;
using System.IO;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Responsible for playing the voice greeting audio file
    /// </summary>
    public class AudioPlayer
    {
        public void PlayVoiceGreeting()
        {
            // Make sure this filename matches your actual file name
            const string fileName = "welcome.wav";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"[Audio] File not found: {fileName}");
                return;
            }

            try
            {
                var player = new MediaPlayer();
                player.Open(new Uri(fullPath, UriKind.Absolute));
                player.Play();

                Console.WriteLine("🎤 Playing voice greeting...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio Error] {ex.Message}");
            }
        }
    }
}