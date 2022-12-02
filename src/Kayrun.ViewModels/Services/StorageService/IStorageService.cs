// Adam Dernis 2022

using System.Threading.Tasks;

namespace Kayrun.Services.StorageService
{
    /// <summary>
    /// An interface for a service to store files.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns>True if the file exists, false otherwise.</returns>
        Task<bool> HasFile(string filename);

        /// <summary>
        /// Gets the names of all files in the current directory of type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to query for.</param>
        Task<string[]> QueryType(string type);

        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        Task CreateFile(string filename);

        /// <summary>
        /// Loads data from a file.
        /// </summary>
        /// <typeparam name="T">The type of the data to load.</typeparam>
        /// <param name="filename">The name of the file.</param>
        Task<T?> LoadAsync<T>(string filename);

        /// <summary>
        /// Saves data to a file.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="filename">The name of the file.</param>
        /// <param name="data">The data to save.</param>
        Task SaveAsync<T>(string filename, T data);

        /// <summary>
        /// Gets the list of folders name in a relative path.
        /// </summary>
        /// <param name="path">The path of the folder.</param>
        Task<string[]> GetFolders(string path);
    }
}
