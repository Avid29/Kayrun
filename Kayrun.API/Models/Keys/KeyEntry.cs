// Adam Dernis 2022

using System.Text.Json.Serialization;

namespace Kayrun.API.Models.Keys
{
    /// <summary>
    /// A key entry object
    /// </summary>
    public class KeyEntry
    {
        // Json objects don't follow nullablity rules
#pragma warning disable CS8618

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEntry"/> class.
        /// </summary>
        public KeyEntry()
        {
        }

        /// <summary>
        /// Gets or sets the email this key associates with.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the key associated with the email.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}