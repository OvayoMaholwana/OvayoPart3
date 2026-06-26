// QuizManager handles the flow of a cybersecurity quiz.
// It manages questions, tracks progress, calculates score, and logs actions.
public class QuizManager
{
    // List of quiz questions.
    private readonly List<QuizQuestion> _questions = new List<QuizQuestion>();

    // Index of the current question being asked.
    private int _currentQuestionIndex = 0;

    // Tracks the user's score.
    private int _score = 0;

    // Logger for recording quiz activity.
    private readonly ActivityLogger _logger;

    // Constructor initializes the quiz manager with a logger and loads questions.
    public QuizManager(ActivityLogger logger)
    {
        _logger = logger;
        LoadQuestions();
    }

    // Loads predefined quiz questions into the list.
    private void LoadQuestions()
    {
        _questions.Clear();
        _questions.AddRange(new List<QuizQuestion>
        {
            new QuizQuestion
            {
                Question = "What does 2FA stand for?",
                Options = new[] {"Two Factor Authentication", "Two Firewall Access", "Two Factor Access", "Trusted Factor Access"},
                CorrectAnswerIndex = 0,
                Explanation = "2FA adds an extra layer of security by requiring two forms of verification."
            },
            new QuizQuestion
            {
                Question = "Which is the strongest password?",
                Options = new[] {"Password123", "MyDog2026", "P@ssw0rd!", "Tr0ub4dor&3xpl0r3r2026!"},
                CorrectAnswerIndex = 3,
                Explanation = "Long, complex passphrases with mixed characters are much stronger."
            },
            new QuizQuestion
            {
                Question = "What is Phishing?",
                Options = new[] {"A type of fishing", "Fake emails/websites to steal info", "A new programming language", "A cybersecurity tool"},
                CorrectAnswerIndex = 1,
                Explanation = "Phishing is a social engineering attack where attackers impersonate trusted entities."
            },
            new QuizQuestion
            {
                Question = "What should you do if you receive a suspicious email asking for your password?",
                Options = new[] {"Reply immediately", "Click the link", "Report it and delete", "Forward to friends"},
                CorrectAnswerIndex = 2,
                Explanation = "Never respond or click links in suspicious emails."
            },

            // ==================== Additional Questions ====================
            new QuizQuestion
            {
                Question = "What is Ransomware?",
                Options = new[] {"A virus that steals passwords", "Malware that encrypts your files and demands payment", "A type of phishing email", "A firewall software"},
                CorrectAnswerIndex = 1,
                Explanation = "Ransomware locks your files and demands ransom payment to unlock them."
            },
            new QuizQuestion
            {
                Question = "Which of these is safest for public Wi-Fi?",
                Options = new[] {"Using no VPN", "Using a VPN", "Using incognito mode", "Disabling firewall"},
                CorrectAnswerIndex = 1,
                Explanation = "A VPN encrypts your traffic and protects you on public networks."
            },
            new QuizQuestion
            {
                Question = "What does HTTPS mean?",
                Options = new[] {"HyperText Transfer Protocol Secure", "High Transfer Protocol System", "Hyperlink Text Processing System", "Host Transfer Protocol Standard"},
                CorrectAnswerIndex = 0,
                Explanation = "HTTPS encrypts data between your browser and the website."
            },
            new QuizQuestion
            {
                Question = "Social Engineering is best described as:",
                Options = new[] {"Hacking into computer systems", "Manipulating people to reveal confidential information", "Writing computer viruses", "Creating strong passwords"},
                CorrectAnswerIndex = 1,
                Explanation = "It exploits human psychology rather than technical vulnerabilities."
            },
            new QuizQuestion
            {
                Question = "Why should you keep your software updated?",
                Options = new[] {"To use more storage", "To fix security vulnerabilities", "To make it look better", "To slow down your device"},
                CorrectAnswerIndex = 1,
                Explanation = "Updates often patch known security holes that hackers can exploit."
            },
            new QuizQuestion
            {
                Question = "What is the best way to store passwords?",
                Options = new[] {"Write them on paper", "Use the same password everywhere", "Use a reputable Password Manager", "Save them in your browser only"},
                CorrectAnswerIndex = 2,
                Explanation = "Password managers securely store and generate strong unique passwords."
            },
            new QuizQuestion
            {
                Question = "What should you do before clicking on a link in an email?",
                Options = new[] {"Click it immediately", "Hover over it to see the real URL", "Share it with friends", "Ignore it completely"},
                CorrectAnswerIndex = 1,
                Explanation = "Hovering reveals if the link destination matches the displayed text."
            }
        });
    }

    // Returns the current question.
    public QuizQuestion GetCurrentQuestion() => _questions[_currentQuestionIndex];

    // Checks if the quiz has finished.
    public bool IsQuizFinished() => _currentQuestionIndex >= _questions.Count;

    // Submits an answer for the current question.
    // Updates score, logs the result, and moves to the next question.
    public string SubmitAnswer(int selectedIndex)
    {
        var q = GetCurrentQuestion();
        bool correct = selectedIndex == q.CorrectAnswerIndex;

        if (correct) _score++;

        string feedback = correct ? "✅ Correct!" : $"❌ Wrong. {q.Explanation}";

        _logger.LogAction($"Quiz: {q.Question} - {(correct ? "Correct" : "Incorrect")}");

        _currentQuestionIndex++;
        return feedback;
    }

    // Returns the final score and logs completion.
    public string GetFinalScore()
    {
        _logger.LogAction($"Quiz completed with score: {_score}/{_questions.Count}");
        return $"Quiz Complete!\nScore: {_score} out of {_questions.Count}";
    }

    // Resets the quiz progress and score.
    public void ResetQuiz()
    {
        _currentQuestionIndex = 0;
        _score = 0;
    }
}
