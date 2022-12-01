// Adam Dernis 2022

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kayrun.API.Models.Keys
{
    /// <summary>
    /// An override for the <see cref="KeyEntry"/> class used to store private keys with multiple associated keys.
    /// </summary>
    public class PrivateKeyEntry : KeyEntry
    {
        /// <summary>
        /// Gets or sets the list of emails the key is associated with.
        /// </summary>
        [JsonPropertyName("email")]
        public new HashSet<string>? Email { get; set; }
    }
}
