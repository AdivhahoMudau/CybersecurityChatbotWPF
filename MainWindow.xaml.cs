using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Media;
// Part 2 WPF Chatbot - Created by Adivhaho Angel Mudau
// Commit 2 - Adding documentation

namespace CybersecurityChatbotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        private Responses _responses;
        private string _userName;
        private SoundPlayer _soundPlayer;

        private bool _isQuizMode = false;
        private int _currentQuizIndex = 0;
        private int _quizScore = 0;
        private string[] _quizQuestions;
        private string[] _quizOptions;
        private string[] _quizAnswers;
        public MainWindow()
        {
            InitializeComponent();
            PlayVoiceGreeting();

            InitializeChatbot();

        }

        private void InitializeChatbot()
        {
            _responses = new Responses();

            // Clear any placeholder text
            rtxtChat.Document.Blocks.Clear();

            // Ask for user name
            AskForUserName();
        }

        private void AskForUserName()
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Welcome to Cybersecurity Chatbot!\n\nPlease enter your name:",
                "Welcome", "");
            if (!string.IsNullOrWhiteSpace(name))
            {
                _userName = name;
                AddBotMessage($"Hello {_userName}! I'm your Cybersecurity Awareness Assistant.");
                AddBotMessage("I'm here to help you stay safe online.");
                AddBotMessage("");
                AddBotMessage("You can ask me about:");
                AddBotMessage("• Password safety");
                AddBotMessage("• Phishing attacks");
                AddBotMessage("• Safe browsing");
                AddBotMessage("• Or just type your question below!");
            }
            else
            {
                _userName = "User";
                AddBotMessage(" Hello! I'm your Cybersecurity Awareness Assistant.");

            }
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (File.Exists(audioPath))
                {
                    _soundPlayer = new SoundPlayer(audioPath);
                    _soundPlayer.Play();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"greeting.wav not found at: {audioPath}");

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Voice greeting error:{ex.Message}");
            }
        }
        private void AddUserMessage(string message)
        {
            Paragraph para = new Paragraph();
            Run run = new Run($"{_userName}: {message}\n");
            run.Foreground = new SolidColorBrush(Colors.LightBlue);
            para.Inlines.Add(run);
            rtxtChat.Document.Blocks.Add(para);
            ScrollToBottom();
        }
        private void AddBotMessage(string message)
        {
            Paragraph para = new Paragraph();
            Run run = new Run($"Bot: {message}\n");
            run.Foreground = new SolidColorBrush(Colors.LightGreen);
            para.Inlines.Add(run);
            rtxtChat.Document.Blocks.Add(para);
            ScrollToBottom();
        }
        private void AddSystemMessage(string message)
        {
            Paragraph para = new Paragraph();
            Run run = new Run($"{message}\n");
            run.Foreground = new SolidColorBrush(Colors.Gray);
            para.Inlines.Add(run);
            rtxtChat.Document.Blocks.Add(para);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            scrollChat.ScrollToEnd();

        }

        private void ProcessUserInput(string userinput)
        {
            if (string.IsNullOrWhiteSpace(userinput))
            {
                AddBotMessage("Please type a question or use the buttons below");
                return;


            }

            if (_isQuizMode)
            {
                CheckQuizAnswers(userinput);
                return;
            }
            AddUserMessage(userinput);

            string response = _responses.GetResponse(userinput, _userName);
            AddBotMessage(response);
        }

        private void InitializeQuiz()
        {
            _quizQuestions = new String[]
                {
               "What is a phishing attack?",
               "What is a strong password practice?",
               "What does HTTPS stand for?",
               "What is Two-Factor Authentication (2FA)?",
               "What should you do if you receive a suspicious email?"
                };

            _quizOptions = new string[]
 {
    "A) A type of computer virus\nB) A fraudulent attempt to obtain sensitive information\nC) A secure way to browse the internet\nD) A method of encrypting data",
    "A) Use 'password123'\nB) Use the same password everywhere\nC) Use at least 12 characters with symbols and numbers\nD) Write it on a sticky note",
    "A) HyperText Transfer Protocol Secure\nB) High Transfer Text Protocol\nC) Hyperlink Text Transfer Protocol\nD) None of the above",
    "A) Two different passwords\nB) Password + code from your phone\nC) Two usernames\nD) Fingerprint only",
    "A) Click the link to see what happens\nB) Reply with your personal information\nC) Delete it and report it as spam\nD) Forward it to your friends"
 };
            _quizAnswers = new String[]
            {
            "B",
            "C",
            "A",
            "B",
            "C"
            };
        }
private void AskNextQuizQuestion()
{
    if (_currentQuizIndex < _quizQuestions.Length)
    {
        AddBotMessage("");
        AddBotMessage($"Question {_currentQuizIndex + 1} of {_quizQuestions.Length}:");
        AddBotMessage($"{_quizQuestions[_currentQuizIndex]}");
        AddBotMessage($"{_quizOptions[_currentQuizIndex]}");
        AddBotMessage("Type your answer (A, B, C, or D):");
    }
    else
    {

        AddBotMessage("");
        AddBotMessage($" QUIZ COMPLETED!");
        AddBotMessage($" Your score: {_quizScore} out of {_quizQuestions.Length}!");

        if (_quizScore == _quizQuestions.Length)
        {
            AddBotMessage("Excellent score! You're a cybersecurity expert!");
        }
        else if (_quizScore >= 3)
        {
            AddBotMessage("Good job! Keep learning about cybersecurity.");
        }
        else
        {
            AddBotMessage("Keep learning! Cybersecurity is important for everyone.");
        }

        _isQuizMode = false;
    } 
}
 
private void CheckQuizAnswers(string userInput)
{
    string answer = userInput.Trim().ToUpper();
            if (answer != "A" && answer != "B" && answer != "C" && answer != "D")
            {
        AddBotMessage("Please type A, B, C, or D. ");
        AskNextQuizQuestion();
        return;
    }
    string correctAnswer = _quizAnswers[_currentQuizIndex];

    if (answer == correctAnswer) {

    _quizScore++;
        AddBotMessage("Correct! Great job");
    }
    else
    {
        AddBotMessage($"Wrong! The correct answer was {correctAnswer}.");
    }
    _currentQuizIndex++;
        AskNextQuizQuestion();
    
}

    //  Button Click Handlers
    private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            ProcessUserInput(userInput);
            txtUserInput.Clear();
            txtUserInput.Focus();
        }
        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                btnSend_Click(sender, e);
            }
        }

        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("password", _userName);
            AddUserMessage("Tell me about password safety");
            AddBotMessage(response);
        }

        private void btnPhishing_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("phishing", _userName);
            AddUserMessage("Tell me about phishing attacks");
            AddBotMessage(response);
        }

        private void btnBrowsing_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("browsing", _userName);
            AddUserMessage("Tell me about safe browsing");
            AddBotMessage(response);
        }
        private void btnHowAreYou_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("how are you", _userName);
            AddUserMessage("How are you");
            AddBotMessage(response);
        }
        private void btnPurpose_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("purpose", _userName);
            AddUserMessage("What is your purpose");
            AddBotMessage(response);

        }
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            string response = _responses.GetResponse("help", _userName);
            AddUserMessage("Help me");
            AddBotMessage(response);
        }
private void btnQuiz_Click(object sender, RoutedEventArgs e)
{
    if (!_isQuizMode)
    {
        InitializeQuiz();
        _isQuizMode = true;
        _currentQuizIndex = 0;
        _quizScore = 0;
        AddBotMessage("Welcome to the cybersecurity Quiz!");
        AddBotMessage("I'll ask you 5 questions. Type the letter ( A, B, C, or D for your answer.");
        AddBotMessage("Let's beign!");

        AskNextQuizQuestion();
    }
    else
    {
        AddBotMessage("Quiz already in progress! Type your answer (A, B, C, or D).");
    }
}
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage($"Goodbye {_userName}! Stay safe online!");
            MessageBox.Show("Thank you for using Cybersecurity Chatbot!\n\nRemember: Stay safe online!",
                "Goodbye",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            Application.Current.Shutdown();
        }
        protected override void OnClosed(EventArgs e)
        {
            if (_soundPlayer != null)
            {
                _soundPlayer.Dispose();
            }
            base.OnClosed(e);
        }
    }
}
