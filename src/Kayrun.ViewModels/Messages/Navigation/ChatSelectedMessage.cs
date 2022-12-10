// Adam Dernis 2022

using Kayrun.Bindables.Chats.Abstract;

namespace Kayrun.Messages.Navigation
{
    public class ChatSelectedMessage
    {
        public ChatSelectedMessage(BindableChat chat)
        {
            Chat = chat;
        }

        public BindableChat Chat { get; }
    }
}
