using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBot _chatBot;
        private readonly TaskManager _taskManager;
        private readonly QuizManager _quizManager;
        private readonly ActivityLogger _activityLogger;

        public MainWindow()
        {
            InitializeComponent();

            _activityLogger = new ActivityLogger();
            _chatBot = new ChatBot();
            _taskManager = new TaskManager(_activityLogger);
            _quizManager = new QuizManager(_activityLogger);

            // Load initial data
            LoadTasksIntoList();
            UpdateActivityLog();

            // Startup greeting
            AppendBotMessage(_chatBot.GetGreeting());
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

              private void SendMessage()
        {
            string input = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendUserMessage(input);
            txtUserInput.Clear();

            // Optional: Simple "typing..." effect
            var typing = new TextBlock 
            { 
                Text = "CyberGuard is typing...", 
                Foreground = Brushes.Gray, 
                FontStyle = FontStyles.Italic,
                Margin = new Thickness(15, 5, 0, 10)
            };
            chatPanel.Children.Add(typing);
            scrollViewer.ScrollToBottom();

            // Process response
            string response = _chatBot.ProcessInput(input);

            // Remove typing indicator
            chatPanel.Children.Remove(typing);

            AppendBotMessage(response);

            UpdateActivityLog();
        }
        private void AppendUserMessage(string message)
        {
            // User message bubble (Right side)
            var userBubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0, 149, 255)),
                CornerRadius = new CornerRadius(18, 18, 4, 18),
                Margin = new Thickness(50, 8, 10, 8),
                Padding = new Thickness(15),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 550
            };

            var text = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White,
                FontSize = 15
            };

            userBubble.Child = text;
            chatPanel.Children.Add(userBubble);
            scrollViewer.ScrollToBottom();
        }

        private void AppendBotMessage(string message)
        {
            // Bot message bubble (Left side)
            var botBubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(45, 55, 72)),
                CornerRadius = new CornerRadius(18, 18, 18, 4),
                Margin = new Thickness(10, 8, 50, 8),
                Padding = new Thickness(15),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 550
            };

            var text = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.White,
                FontSize = 15
            };

            botBubble.Child = text;
            chatPanel.Children.Add(botBubble);
            scrollViewer.ScrollToBottom();
        }

        
        private void LoadTasksIntoList()
        {
            lstTasks.Items.Clear();
            foreach (var task in _taskManager.GetAllTasks())
            {
                string status = task.IsComplete ? "✓ COMPLETE" : "Pending";
                lstTasks.Items.Add($"[{task.Id}] {task.Title} - {status}");
            }
        }

        private void ClearTaskInputs()
        {
            txtTaskTitle.Clear();
            txtTaskDesc.Clear();
            txtTaskReminder.Clear();
        }

        private void btnMarkComplete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex == -1) return;
            string selected = lstTasks.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(selected)) return;

            int id = int.Parse(selected.Split(']')[0].Replace("[", ""));

            _taskManager.MarkTaskComplete(id);
            LoadTasksIntoList();
        }

        private void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex == -1) return;
            string selected = lstTasks.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(selected)) return;

            int id = int.Parse(selected.Split(']')[0].Replace("[", ""));

            _taskManager.DeleteTask(id);
            LoadTasksIntoList();
        }

        // ==================== QUIZ ====================
        private void btnSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (lstQuizOptions.SelectedIndex == -1) return;

            string feedback = _quizManager.SubmitAnswer(lstQuizOptions.SelectedIndex);
            txtQuizFeedback.Text = feedback;

            if (_quizManager.IsQuizFinished())
            {
                txtQuizFeedback.Text += "\n\n" + _quizManager.GetFinalScore();
                btnSubmitAnswer.IsEnabled = false;
            }
            else
            {
                LoadCurrentQuizQuestion();
            }
        }

        private void LoadCurrentQuizQuestion()
        {
            var q = _quizManager.GetCurrentQuestion();
            txtQuizQuestion.Text = q.Question;
            lstQuizOptions.Items.Clear();
            foreach (var option in q.Options)
                lstQuizOptions.Items.Add(option);
        }

        private void btnResetQuiz_Click(object sender, RoutedEventArgs e)
        {
            _quizManager.ResetQuiz();
            btnSubmitAnswer.IsEnabled = true;
            txtQuizFeedback.Text = string.Empty;     // ← Fixed here
            LoadCurrentQuizQuestion();
        }

        // ==================== ACTIVITY LOG ====================
        private void UpdateActivityLog()
        {
            txtActivityLog.Text = _activityLogger.GetLogsAsString(15);
        }

        private void btnRefreshLog_Click(object sender, RoutedEventArgs e)
        {
            UpdateActivityLog();
        }
    }
}