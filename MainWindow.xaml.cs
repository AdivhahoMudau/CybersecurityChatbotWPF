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
        public MainWindow()
        {
            InitializeComponent();
            InitializeChatbot();
            PlayVoiceGreeting();
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
            AddUserMessage(userinput);

            string response = _responses.GetResponse(userinput, _userName);
            AddBotMessage(response);
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
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage($"Goodbye {_userName}! Stay safe online!");
            MessageBox.Show("Thank you for using Cybersecurity Chatbot!\n\nRemember: Stay safe online!",
                "Goodbye",
                MessageBoxButton.OK, MessageBoxImage.Information);

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
