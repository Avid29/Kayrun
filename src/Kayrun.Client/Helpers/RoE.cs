// Adam Dernis 2022

namespace Kayrun.Client.Helpers
{
    /// <summary>
    /// A result or an error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <typeparam name="TError">The error type.</typeparam>
    public struct RoE<T, TError>
    {
        /// <summary>
        /// Creates a successful RoE.
        /// </summary>
        /// <param name="result">The result.</param>
        public RoE(T result)
        {
            Result = result;
            Error = default;
            Success = true;
        }

        /// <summary>
        /// Creates a failed RoE.
        /// </summary>
        /// <param name="error">The error.</param>
        public RoE(TError error)
        {
            Error = error;
            Result = default;
            Success = false;
        }

        /// <summary>
        /// The result, if succeeded.
        /// </summary>
        public T? Result { get; }

        /// <summary>
        /// The error, if failed.
        /// </summary>
        public TError? Error { get; }

        /// <summary>
        /// Gets whether or not the result was a success.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Implicitly converts a result into a successful <see cref="RoE{T, TError}"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        public static implicit operator RoE<T, TError>(T result)
            => new(result);

        /// <summary>
        /// Implicitly converts an error into a failed <see cref="RoE{T, TError}"/>.
        /// </summary>
        /// <param name="error">The error.</param>
        public static implicit operator RoE<T, TError>(TError error)
            => new(error);
    }
}
