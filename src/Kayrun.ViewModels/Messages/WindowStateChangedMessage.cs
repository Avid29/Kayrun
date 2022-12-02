using Kayrun.ViewModels.Enums;

namespace Kayrun.Messages
{
    /// <summary>
    /// A message sent when the 
    /// </summary>
    public class WindowStateChangedMessage
    {
        public WindowStateChangedMessage(WindowHostState state)
        {
            WindowState = state;
        }

        public WindowHostState WindowState { get; }
    }
}
