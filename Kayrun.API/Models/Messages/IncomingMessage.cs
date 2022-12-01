// Adam Dernis 2022

using System;
using System.Text.Json.Serialization;

namespace Kayrun.API.Models.Messages
{
    /// <summary>
    /// A class for a json serializable incoming message object.
    /// </summary>
    public class IncomingMessage : MessageBase
    {
        /// <summary>
        /// Gets or sets the timestamp of the message.
        /// </summary>
        [JsonPropertyName("messageTime")]
        public DateTime Timestamp { get; set; }
    }
}