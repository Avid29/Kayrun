// Adam Dernis 2022

namespace Kayrun.Messages
{
    public class ChatCreatedMessage
    {
        public ChatCreatedMessage(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
