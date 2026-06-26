using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Added documentation
namespace CybersecurityChatbotWPF  //Changed from part 1's namespace to match part 2's namespace
{
    internal class Responses
    {
        private Dictionary<string,List< string>>_responseKeywords;
        private Random random;

        public Responses()
        {
            random = new Random();
            InitializeKeywords();
        }

        private void InitializeKeywords()
        {
            _responseKeywords = new Dictionary<string, List< string>>(StringComparer.OrdinalIgnoreCase)
            { 
                
                ["how_are_you"] = new List<string> { "how are you", "how do you do", "how's it going", "how are you doing", "you ok" },
                ["purpose"] = new List<string> { "purpose" , "what do you do" , "what is your purpose" , "why do you exist" , "what can you do"},
                ["password"] = new List<string> { "password" , "passphrase" , "secure password" , "password safety", "strong password"} ,
                ["phishing"] = new List<string> {"phishing", "scam email" , "suspicious email", "email scam" , "fraud email"},
                ["browsing"] = new List<string> { "browsing" , "safe browsing" , "website safety" , "internet safety" , "web safety"},
                ["help"] = new List<string> { "help" , "what can i ask" , "topics" , "options"},
                ["thank"] = new List<string> { "thank" , "thanks" , "appreciate" ,"thx" },
                ["hello"] = new List<string> { "hello" , "hi" , "hey" , "good morning" , "good afternoon" , "good evening"}, 
                ["cybersecurity"] = new List<string> {"cybersecurity" , "cyber security" , "online safety" , "internet security"}
                };
                }
            
        
        public string GetResponse(string userInput, string userName)
        {
            // Check for matches with keywords
            foreach (var category in _responseKeywords)
            {
                foreach (string keyword in category.Value) {
                    if (userInput.Contains(keyword.ToLower())) {
                        //Added new cybersecurity tip
                        return GetResponseForCategory(category.Key, userName);

            }
                }
            }
            // Default response for unrecognized input
            return GetDefaultResponse(userName);
        }
        private string GetResponseForCategory(string categoryKey, string userName) {
            switch (categoryKey.ToLower()) {
                case "how_are_you":
                    string[] howAreYouResponses =
                    {
                        $"I'm doing great,{userName}! Thanks for asking! I'm fully charged and ready to help you stay secure online!",
                        $"I'm excellent,{userName}! Nothing makes me happier than helping people learn about cybersecurity!",
                        $"I'm feeling secure and ready to help,{userName}! How can i assist today?"
                    };
                    return howAreYouResponses[random.Next(howAreYouResponses.Length)];

                case "purpose":


                    string[] purposeResponses = {
                        $"My purpose is to help you learn about cybersecurity, {userName}. I can teach you about password safety, how to spot phishing at tempts, and safe browsing practices. Together, we can keep you safe online!",
                        $"I exist to educate South Africans about online threats! I focus on password safety, phishing awareness, and safe browsing habits, {userName}. Stay safe with me!",
                        $"I'm here to help you navigate the digital world safely and avoid common online threats."
                    };
                    return purposeResponses[random.Next(purposeResponses.Length)];

                case "password":
                    return GetPasswordSafetyTip();

                case "phishing":
                    return GetPhishingTip();

                case "browsing":
                    return GetSafeBrowsingTip();


                case "help":
                    return GetHelpMessage();

                case "thank":
                    string[] thankResponses = {
                        $"You're welcome, {userName}! Stay safe out there!",
                        $"My pleasure, {userName}! Remember, cybersecurity is everyone's responsibility!",
                        $"Happy to help, {userName}! Feel free to ask me anything about staying safe online!"
                    };
                    return thankResponses[random.Next(thankResponses.Length)];

                case "hello":
                    string[] helloResponses = {
                        $"Hello, {userName}! How can I help you stay safe online today?",
                        $"Hi there, {userName}! Ready to learn about cybersecurity?",
                        $"Greetings, {userName}! What would you like to know about online safety?"
                    };
                    return helloResponses[random.Next(helloResponses.Length)];

                case "Cybersecurity":
                    return "Cybersecurity is the practice of protecting systems, networks, and programs from digital attacks. In South Africa, cyber threats are increasing, which is why I'm here to help you learn how to stay safe online";

                default:
                    return GetDefaultResponse(userName);

            }
        }

        private string GetPasswordSafetyTip() {

            return "**Password Safety Tips:**\n\n" +
                "• Use passwords that are at least 12 characters long\n" +
                   "• Combine uppercase, lowercase, numbers, and symbols\n" +
                   "• Never reuse passwords across different websites\n" +
                   "• Consider using a password manager like LastPass or Bitwarden\n" +
                   "• Enable Two-Factor Authentication (2FA) whenever possible\n" +
                   "• Avoid using personal information like birthdays or names\n\n" +
                   "Did you know? A strong password can take years to crack, while a weak one takes seconds!";
        }
        private string GetPhishingTip() {

            return " **How to Spot Phishing Attempts:**\n\n" +
                   "• Check the sender's email address carefully - scammers use fake addresses\n" +
                   "• Look for spelling and grammar errors in the message\n" +
                   "• Never click suspicious links - hover to see the actual URL first\n" +
                   "• Don't share personal information via email or text\n" +
                   "• Be wary of urgent requests for money or information\n" +
                   "• Legitimate companies never ask for passwords via email\n\n" +
                   "Remember: If it seems too good to be true, it probably is!";
        }
        private string GetSafeBrowsingTip() {

            return "**Safe Browsing Practices:**\n\n" +
                "• Look for 'https://' and a padlock icon in the address bar\n" +
                   "• Avoid using public Wi-Fi for sensitive transactions like banking\n" +
                   "• Keep your browser and extensions updated to the latest version\n" +
                   "• Use a reputable ad-blocker and antivirus software\n" +
                   "• Clear your browsing data regularly\n" +
                   "• Be careful what you download - only use official sources\n\n" +
                   "Pro tip: Consider using a VPN when connecting to public networks!";
        }
        private string GetHelpMessage()
        {
            return "**I can help you with:**\n\n" +
                "• Password safety tips and best practices\n" +
                "• Identifying phishing attempts and scams\n" +
                "• Safe browsing practices for everyday internet use\n" +
                "• General cybersecurity awareness in South Africa\n\n" +
                "**Try asking me:**\n" +
                "- 'Tell me about password safety'\n" +
                "- 'How do I spot phishing?'\n" +
                "- 'What is safe browsing?'\n" +
                "- 'How are you?'\n" +
                "- 'What is your purpose?'\n\n" +
                "Type 'exit' when you're ready to leave. Stay safe online!";
        }
        private string GetDefaultResponse(string userName) {
            string[] defaultResponses =
            {
                $"I'm not sure I understand, {userName}. Could you ask me about password safety, phishing, or safe browsing?" ,
                $"Hmm, {userName}, I didn't quite get that. Would you like to learn about cybersecurity topics? Just type 'help' to see what I can do!",
                $"Sorry, {userName}, I'm still learning! Try asking me about 'password safety', 'phishing', or 'safe browsing'." ,
                $"I didn't catch that, {userName}. Want to learn about how to create strong passwords or spot phishing emails?",
                $"Not sure what you mean,{userName}. Type 'help' to see the topics I can help you with!" };

            return defaultResponses[random.Next(defaultResponses.Length)];
            }
        }
    
        
    }

