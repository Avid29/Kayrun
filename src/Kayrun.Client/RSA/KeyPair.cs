// Adam Dernis 2022

using System.Numerics;
using System;

namespace Kayrun.Client.RSA
{
    /// <summary>
    /// A key pair class used to wrap a public and private key pair.
    /// </summary>
    public class KeyPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPair"/> class.
        /// </summary>
        public KeyPair(Key publicKey, Key privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        /// <summary>
        /// Gets the public key of the pair.
        /// </summary>
        public Key PublicKey { get; set; }

        /// <summary>
        /// Gets the private key of the pair.
        /// </summary>
        public Key PrivateKey { get; set; }

        /// <summary>
        /// Generates a key pair.
        /// </summary>
        /// <param name="size">The size of the keys.</param>
        public static KeyPair GenerateKeyPair(int size)
        {
            // Split bits between p and q
            // Split by bytes to ensure the bits are divisible by 8
            var splitBytes = size / 16;

            // Set p to half the total bits +/- ~25%
            var scale = new Random().NextDouble().MapRange(0.75, 1.25);
            var pBits = (int)(splitBytes * scale) * 8;

            // Set remaining bits to q
            var qBits = size - pBits;

            // Generate 3 prime numbers: p, q, and E
            var primes = PrimeGenerator.GeneratePrimes(new[] { pBits, qBits, 16 });
            var p = primes[0];
            var q = primes[1];
            var bigE = primes[2];

            // Derive N and D
            var bigN = p * q;
            var r = (p - 1) * (q - 1);
            var bigD = ModInverse(bigE, r);

            // Create keys
            var privateKey = new Key(bigD, bigN);
            var publicKey = new Key(bigE, bigN);
            return new KeyPair(publicKey, privateKey);
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }
    }
}
