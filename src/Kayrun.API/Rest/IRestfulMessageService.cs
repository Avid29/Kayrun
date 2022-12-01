// Adam Dernis 2022

using Kayrun.API.Models.Messages;
using Refit;
using System.Threading.Tasks;

namespace Kayrun.API.Rest
{
    /// <summary>
    /// An interface for RESTful api methods for the message path.
    /// </summary>
    public interface IRestfulMessageService
    {
        [Get("/Message/{email}")]
        Task<IncomingMessage> GetMessage([AliasAs("email")] string email);

        [Put("/Message/{email}")]
        Task SendMessage([AliasAs("email")] string email, [Body] OutgoingMessage message);
    }
}