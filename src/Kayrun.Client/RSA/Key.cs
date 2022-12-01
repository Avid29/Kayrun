// Adam Dernis 2022

using System.Numerics;
using System;
using CommunityToolkit.Diagnostics;

namespace Kayrun.Client.RSA
{
    /// <summary>
    /// A key split into E and N components.
    /// </summary>
    /// <remarks>
    /// <see cref="E"/> represents "D" for private keys.
    /// </remarks>
    public struct Key
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Key"/> struct.
        /// </summary>
        public Key(BigInteger e, BigInteger n)
        {
            E = e;
            N = n;
        }

        /// <summary>
        /// The E component of the key.
        /// </summary>
        /// <remarks>
        /// Represents D for private keys.
        /// </remarks>
        public BigInteger E { get; set; }

        /// <summary>
        /// The N component of the key.
        /// </summary>
        public BigInteger N { get; set; }

        /// <summary>
        /// Gets a base64 string representation of the key.
        /// </summary>
        public string ToBase64()
        {
            var bigEBytes = E.ToByteArray();
            var bigNBytes = N.ToByteArray();

            var e = bigEBytes.Length;
            var n = bigNBytes.Length;

            var eBytes = BitConverter.GetBytes(e);
            var nBytes = BitConverter.GetBytes(n);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(eBytes);
                Array.Reverse(nBytes);
            }

            var bytes = new byte[8 + e + n];

            Array.Copy(eBytes, 0, bytes, 0, 4);
            Array.Copy(bigEBytes, 0, bytes, 4, e);
            Array.Copy(nBytes, 0, bytes, e + 4, 4);
            Array.Copy(bigNBytes, 0, bytes, e + 8, n);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Parses a key from a base64 string.
        /// </summary>
        /// <param name="str">The base64 string to parse.</param>
        public static Key FromBase64(string str)
        {
            Span<byte> bytes = Convert.FromBase64String(str);

            var eBytes = bytes.Slice(0, 4).ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(eBytes);

            var e = BitConverter.ToInt32(eBytes, 0);
            var bigE = new BigInteger(bytes.Slice(4, e).ToArray());

            var nBytes = bytes.Slice(e + 4, 4).ToArray();
            if (BitConverter.IsLittleEndian)
                Array.Reverse(nBytes);

            var n = BitConverter.ToInt32(nBytes, 0);
            var bigN = new BigInteger(bytes.Slice(e + 8, n).ToArray());

            Guard.IsEqualTo(e + 8 + n, bytes.Length);

            return new Key(bigE, bigN);
        }
    }
}
