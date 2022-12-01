// Adam Dernis 2022

using System.Text.Json.Serialization;

namespace Kayrun.API.Models.Messages
{
    /// <summary>
    /// A base class for a json serializable message object.
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// Gets or sets the recipient email address of the message.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}