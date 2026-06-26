using System;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Main brain of the chatbot. Coordinates all features and conversation flow.
    /// </summary>
    public class ChatBot
    {
        private readonly KeywordResponder keywordResponder;
        private readonly SentimentDetector sentimentDetector;
        private readonly MemoryStore memory;

        // ==================== PART 3 NEW FEATURES ====================
        private readonly TaskManager taskManager;
        private readonly QuizManager quizManager;
        private readonly ActivityLogger activityLogger;
        // ============================================================

        private bool awaitingName = true;
        private string lastTopic = "";
        private int followUpCount = 0;

        public ChatBot()
        {
            keywordResponder = new KeywordResponder();
            sentimentDetector = new SentimentDetector();
            memory = new MemoryStore();

            // ==================== PART 3 INITIALIZATIONS ====================
            activityLogger = new ActivityLogger();
            taskManager = new TaskManager(activityLogger);
            quizManager = new QuizManager(activityLogger);
            // ============================================================
        }

        public string GetGreeting()
        {
            return "Hello! Welcome to CyberShield.\nPlease tell me your name to get started.";
        }

        /// <summary>
        /// Main method that processes all user input
        /// </summary>
        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something.";

            string lower = input.ToLower().Trim();

            // ==================== PART 3 NLP INTENT DETECTION (Added) ====================
            // Task Assistant Intents
            if (lower.Contains("add task") || lower.Contains("new task") || lower.Contains("create task"))
                return "Please go to the **Task Assistant** tab to add a new cybersecurity task.";

            if (lower.Contains("show tasks") || lower.Contains("my tasks") || lower.Contains("view tasks"))
                return "Please switch to the **Task Assistant** tab to see all your tasks.";

            // Quiz Intents
            if (lower.Contains("start quiz") || lower.Contains("take quiz") || lower.Contains("play quiz") || lower.Contains("cyber quiz"))
                return "Please go to the **Cyber Quiz** tab to start the quiz.";

            // Activity Log Intents
            if (lower.Contains("show log") || lower.Contains("activity log") || lower.Contains("what have you done") || lower.Contains("history"))
                return activityLogger.GetLogsAsString(10);
            // ============================================================================

            // 1. Capture name on first message
            if (awaitingName)
            {
                memory.StoreName(input);
                awaitingName = false;
                followUpCount = 0;

                return $"Nice to meet you, **{memory.UserName}**! 👋\n" +
                       "What would you like to know about cybersecurity today?";
            }

            // 2. Handle follow-up requests ("tell me more")
            if (lower.Contains("tell me more") || lower.Contains("explain more") ||
                lower.Contains("more about") || lower.Contains("more details"))
            {
                if (!string.IsNullOrEmpty(lastTopic))
                {
                    followUpCount++;
                    string deeper = keywordResponder.GetDeeperResponse(lastTopic, followUpCount);
                    return $"Sure thing, {memory.UserName}! Here's more information:\n\n{deeper}";
                }
                return $"No problem, {memory.UserName}. What topic would you like me to explain further?";
            }

            // Reset follow-up counter when topic changes
            followUpCount = 0;

            // 3. Detect Sentiment FIRST
            var sentiment = sentimentDetector.Detect(input);
            string sentimentText = sentimentDetector.GetResponse(sentiment);

            // 4. Try to get keyword response
            string keywordResponse = keywordResponder.GetResponse(input);

            // MAIN FIX: Always show sentiment response if detected
            if (!string.IsNullOrEmpty(keywordResponse))
            {
                lastTopic = input;
                string personalised = memory.GetPersonalisedMessage();

                return $"{sentimentText}{personalised}{keywordResponse}\n\n" +
                       $"Is there anything else I can help you with, {memory.UserName}?";
            }
            else if (sentiment != Sentiment.Neutral)
            {
                // Sentiment detected but no keyword → Still respond empathetically
                lastTopic = input;
                string personalised = memory.GetPersonalisedMessage();

                return $"{sentimentText}{personalised}What specific cybersecurity topic are you " +
                       $"concerned about, {memory.UserName}? You can ask me about phishing, passwords, scams, malware, privacy, or VPNs.";
            }

            // 5. Special general questions
            if (lower.Contains("how are you"))
                return $"I'm doing great, thank you, {memory.UserName}! Ready to help you stay safe online.";

            if (lower.Contains("what can you do") || lower.Contains("help with"))
                return $"I can help you with many topics, {memory.UserName}. Just ask me about phishing, passwords, scams, malware, privacy, or VPNs.";

            // 6. Final fallback
            return $"I'm sorry {memory.UserName}, I can only help with cybersecurity topics like " +
                   "phishing, passwords, scams, malware, privacy, and VPNs.\nWhat would you like to know?";
        }

        /// <summary>
        /// Returns whether the bot is still waiting for the user's name
        /// </summary>
        public bool IsAwaitingName()
        {
            return awaitingName;
        }

        // ==================== PART 3 HELPER METHODS (Added) ====================
        public TaskManager GetTaskManager() => taskManager;
        public QuizManager GetQuizManager() => quizManager;
        public ActivityLogger GetActivityLogger() => activityLogger;
        // ===================================================================
    }
}