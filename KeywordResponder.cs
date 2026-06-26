using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Handles keyword detection and returns detailed responses.
    /// Now supports deeper follow-up responses.
    /// </summary>
    public class KeywordResponder
    {
        private readonly Random random = new Random();

        // Regular responses (first time)
        private readonly Dictionary<string, List<string>> responses = new();

        // Deeper / Follow-up responses (when user says "tell me more")
        private readonly Dictionary<string, List<string>> deeperResponses = new();

        public KeywordResponder()
        {
            // ==================== REGULAR RESPONSES ====================
            responses.Add("phishing", new List<string>
            {
                "Phishing is one of the most common attacks in South Africa. Scammers impersonate banks, SARS, and delivery companies.",
                "Always hover over links before clicking and check the actual URL. Never enter personal details on suspicious pages."
            });

            responses.Add("password", new List<string>
            {
                "Strong passwords should be at least 12-16 characters long with a mix of letters, numbers, and symbols.",
                "Never reuse the same password on multiple websites. Use a password manager like Bitwarden."
            });

            responses.Add("scam", new List<string>
            {
                "Common scams in South Africa include fake SARS refunds, bank OTP scams, and fake delivery notifications.",
                "If someone pressures you to act quickly, it's almost always a scam."
            });

            responses.Add("malware", new List<string>
            {
                "Malware can steal your data or damage your device. Always keep your Windows and apps updated.",
                "Avoid downloading cracked software and be careful with email attachments."
            });

            responses.Add("privacy", new List<string>
            {
                "Review your social media privacy settings regularly and limit what you share publicly.",
                "Be cautious about apps that ask for unnecessary permissions like access to your contacts."
            });

            // ==================== DEEPER / FOLLOW-UP RESPONSES ====================
            deeperResponses.Add("phishing", new List<string>
            {
                "Advanced phishing often uses spoofed emails that look almost identical to real ones. Check the email headers if you're technical.",
                "Many phishing attacks now come via WhatsApp and SMS. Never click links in unexpected messages.",
                "Report phishing emails to the Anti-Phishing Working Group (reportphishing@apwg.org) or use South Africa's Cyber Security Hub."
            });

            deeperResponses.Add("password", new List<string>
            {
                "Passphrases are often better than complex passwords because they are longer and easier to remember.",
                "Consider using hardware security keys (like YubiKey) for maximum protection on important accounts.",
                "Change your passwords immediately if you suspect they may have been leaked in a data breach."
            });

            deeperResponses.Add("scam", new List<string>
            {
                "Romance scams are rising in South Africa. Scammers build trust over months before asking for money.",
                "Job offer scams often ask you to pay for 'training' or 'equipment' — legitimate companies never do this.",
                "Always verify any government-related claim directly through official channels."
            });

            deeperResponses.Add("malware", new List<string>
            {
                "Ransomware is a dangerous type of malware that locks your files and demands payment.",
                "Browser hijackers and adware are very common from fake software downloads.",
                "Use Windows Sandbox or a virtual machine when testing suspicious files."
            });

            deeperResponses.Add("privacy", new List<string>
            {
                "Your digital footprint can be used against you. Search your own name online regularly.",
                "Consider using privacy-focused search engines like DuckDuckGo instead of Google.",
                "Use end-to-end encrypted messaging apps like Signal or WhatsApp."
            });

            // ==================== PART 3: NEW NLP INTENTS ====================
            // Task Assistant
            responses.Add("add task", new List<string> { "You can add a new task using the Task Assistant tab." });
            responses.Add("new task", new List<string> { "You can add a new task using the Task Assistant tab." });
            responses.Add("create task", new List<string> { "You can add a new task using the Task Assistant tab." });

            responses.Add("show tasks", new List<string> { "Please check the Task Assistant tab to view all your tasks." });
            responses.Add("my tasks", new List<string> { "Please check the Task Assistant tab to view all your tasks." });
            responses.Add("view tasks", new List<string> { "Please check the Task Assistant tab to view all your tasks." });

            // Quiz
            responses.Add("start quiz", new List<string> { "You can start the Cybersecurity Quiz in the Cyber Quiz tab." });
            responses.Add("take quiz", new List<string> { "You can start the Cybersecurity Quiz in the Cyber Quiz tab." });
            responses.Add("play quiz", new List<string> { "You can start the Cybersecurity Quiz in the Cyber Quiz tab." });

            // Activity Log
            responses.Add("show log", new List<string> { "You can view the Activity Log in the Activity Log tab." });
            responses.Add("activity log", new List<string> { "You can view the Activity Log in the Activity Log tab." });
            responses.Add("what have you done", new List<string> { "Check the Activity Log tab to see everything I've done." });
            // ============================================================
        }

        public string GetResponse(string input)
        {
            string lower = input.ToLower();
            foreach (var key in responses.Keys)
            {
                if (lower.Contains(key))
                {
                    var list = responses[key];
                    return list[random.Next(list.Count)];
                }
            }
            return "";
        }

        /// <summary>
        /// Returns NEW information when user asks for more details
        /// </summary>
        public string GetDeeperResponse(string input, int followUpCount)
        {
            string lower = input.ToLower();

            foreach (var key in deeperResponses.Keys)
            {
                if (lower.Contains(key))
                {
                    var list = deeperResponses[key];
                    int index = followUpCount % list.Count;   // Cycle through responses
                    return list[index];
                }
            }

            // If no deeper response available, return a general one
            return "Here's some additional information on this topic. Let me know if you'd like even more details!";
        }
    }
}