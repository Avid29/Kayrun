// Adam Dernis 2022

using Kayrun.API.Models.Keys;
using Refit;
using System.Threading.Tasks;

namespace Kayrun.API.Rest
{
    /// <summary>
    /// An interface for RESTful api methods for the key path.
    /// </summary>
    public interface IRestfulKeyService
    {
        [Get("/Key/{email}")]
        Task<KeyEntry> GetKey([AliasAs("email")] string email);

        [Put("/Key/{email}")]
        [Headers("Content-Type: application/json;")]
        Task SetKey([AliasAs("email")] string email, [Body] KeyEntry message);
    }
}
