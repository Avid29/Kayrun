// Adam Dernis 2022

using System.Threading.Tasks;

namespace Kayrun.ViewModels.Services.StorageService
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
        public bool HasFile(string filename);

        /// <summary>
        /// Loads data from a file.
        /// </summary>
        /// <typeparam name="T">The type of the data to load.</typeparam>
        /// <param name="filename">The name of the file.</param>
        public Task<T?> LoadAsync<T>(string filename);

        /// <summary>
        /// Saves data to a file.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="filename">The name of the file.</param>
        /// <param name="data">The data to save.</param>
        public Task SaveAsync<T>(string filename, T data);
    }
}
