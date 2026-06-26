using System;
using System.IO;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public class AudioPlayer
    {
        public void PlayVoiceGreeting()
        {
            const string fileName = "welcome.wav";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"[Audio] File not found: {fullPath}");
                return;
            }

            try
            {
                var player = new MediaPlayer();
                player.Open(new Uri(fullPath, UriKind.Absolute));

                // Important: Keep reference so it doesn't get garbage collected immediately
                player.MediaEnded += (s, e) => player.Close();

                player.Play();
                Console.WriteLine("🎤 Playing welcome.wav...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio Error] {ex.Message}");
            }
        }
    }
}