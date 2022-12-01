// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Kayrun.Client.RSA
{
    public static class PrimeGenerator
    {
        private static readonly int[] PrimesTo100 =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
        };

        private static readonly int[] PrimesTo1K =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
            101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
            211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293,
            307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397,
            401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
            503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
            601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
            701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797,
            809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887,
            907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997,
        };

        /// <summary>
        /// Generates an array of prime numbers.
        /// </summary>
        /// <param name="bits">The bits of each prime number to generate.</param>
        /// <returns>An array of prime numbers.</returns>
        public static BigInteger[] GeneratePrimes(int[] bits)
        {
            var primes = new BigInteger[bits.Length];

            // Create a thread for each bit to generate
            Parallel.For(0, bits.Length, (i, _) =>
            {
                var prime = GeneratePrime(bits[i], CancellationToken.None);
                primes[i] = prime;
            });

            return primes;
        }

        /// <summary>
        /// Generates a prime number.
        /// </summary>
        /// <param name="bits">The number of bits in the prime number.</param>
        /// <param name="token">A cancellation token used to cancel the generation.</param>
        /// <returns>A random prime number, or zero if cancelled.</returns>
        private static BigInteger GeneratePrime(int bits, CancellationToken token)
        {
            Guard.IsEqualTo(bits % 8, 0);

            BigInteger prime;
            do
            {
                if (token.IsCancellationRequested)
                {
                    return BigInteger.Zero;
                }

                prime = RandomBigInteger(bits / 8, 2);
            } while (!IsProbablyPrime(prime));
            return prime;
        }

        /// <summary>
        /// Uses the Miller-Rabin test to check if a number is prime.
        /// </summary>
        /// <param name="n">The integer to test for primality.</param>
        /// <param name="k">The number of witness loops to check.</param>
        /// <returns>True if <paramref name="n"/> is probably prime.</returns>
        public static bool IsProbablyPrime(BigInteger n, int k = 10)
        {
            if (n == 2 || n == 3) return true;
            if (Prune(n, PrimesTo100)) return false;

            var byteCount = n.ToByteArray().Length;

            // Split n into 2^s⋅d + 1
            var d = (n - 1).FactorPow2(out var s);

            for (; k > 0; k--)
            {
                var a = RandomBigInteger(byteCount, 2, n - 2);
                if (n.IsSurelyComposite(d, s, a)) return false;
            }

            return true;
        }

        /// <summary>
        /// Generates a random <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="bytes">The number of bytes in the <see cref="BigInteger"/>.</param>
        /// <returns>A random <see cref="BigInteger"/> with <paramref name="bytes"/> bytes.</returns>
        public static BigInteger RandomBigInteger(int bytes)
        {
            var byteArray = new byte[bytes];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(byteArray);

            return BigInteger.Abs(new(byteArray));
        }

        /// <summary>
        /// Generates a random <see cref="BigInteger"/> larger than <paramref name="min"/>.
        /// </summary>
        /// <remarks>
        /// This function is awful if minimum is too large.
        /// </remarks>
        /// <param name="bytes">The number of bytes in the <see cref="BigInteger"/>.</param>
        /// <param name="min">The minimum value to return.</param>
        /// <returns>A random <see cref="BigInteger"/> larger than <paramref name="min"/>.</returns>
        public static BigInteger RandomBigInteger(int bytes, BigInteger min)
        {
            BigInteger result;
            do
            {
                result = RandomBigInteger(bytes);
            } while (result < min);
            return result;
        }

        private static BigInteger RandomBigInteger(int bytes, BigInteger min, BigInteger max)
        {
            Guard.IsGreaterThan(max, min);

            BigInteger result;
            do
            {
                result = RandomBigInteger(bytes);
            } while (result < min || result > max);
            return result;
        }

        private static BigInteger FactorPow2(this BigInteger value, out int s)
        {
            s = 1;
            while (value.IsEven)
            {
                value /= 2;
                s++;
            }

            return value;
        }

        private static bool IsSurelyComposite(this BigInteger n, BigInteger d, int s, BigInteger a)
        {
            var x = BigInteger.ModPow(a, d, n);
            if (x == BigInteger.Zero || x == n - 1) return false;

            for (var i = 0; i <= s; i++)
            {
                x = BigInteger.ModPow(x, 2, n);
                if (x == BigInteger.One) return true;
                if (x == n - 1) return false;
            }
            return true;
        }

        private static bool Prune(BigInteger n, int[] primes)
            => primes.Any(t => n % t == 0);
    }
}
