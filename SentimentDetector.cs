using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }

    /// <summary>
    /// Detects user's emotional tone and provides empathetic, helpful openers
    /// </summary>
    public class SentimentDetector
    {
        private readonly Dictionary<Sentiment, List<string>> triggers = new()
        {
            { Sentiment.Worried, new List<string>
                { "worried", "scared", "afraid", "nervous", "unsafe", "fear", "terrified", "anxious", "panic", "threatened" } },

            { Sentiment.Curious, new List<string>
                { "curious", "wondering", "how does", "how can", "explain", "tell me more", "interested", "learn", "what is" } },

            { Sentiment.Frustrated, new List<string>
                { "frustrated", "confused", "annoyed", "don't understand", "complicated", "hard", "angry", "irritated", "stupid" } },

            { Sentiment.Happy, new List<string>
                { "thanks", "thank you", "great", "awesome", "excellent", "love it", "helpful", "good job" } }
        };

        public Sentiment Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Sentiment.Neutral;

            string lower = input.ToLower();

            foreach (var sentiment in triggers.Keys)
            {
                foreach (var word in triggers[sentiment])
                {
                    if (lower.Contains(word))
                        return sentiment;
                }
            }
            return Sentiment.Neutral;
        }

        /// <summary>
        /// Returns longer, more natural empathetic responses
        /// </summary>
        public string GetResponse(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Worried => "I completely understand why you're worried — online threats can feel overwhelming. Let me give you clear, practical advice to help you stay safe: ",

                Sentiment.Curious => "That's a great question! I'm happy you're taking the time to learn about this. Here's a detailed explanation: ",

                Sentiment.Frustrated => "I see this topic is frustrating and a bit confusing. Let me break it down for you in a simple and clear way: ",

                Sentiment.Happy => "I'm really glad you're finding this helpful! Here's some more useful information for you: ",

                _ => "Here's some important information on this topic: "
            };
        }
    }
}