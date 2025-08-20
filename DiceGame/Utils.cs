using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class Utils
    {
        public static int SecureRandomInt(int minInclusive, int maxExclusive) {
            var rng = RandomNumberGenerator.Create();
            byte[] data = new byte[4];
            rng.GetBytes(data);
            int value = BitConverter.ToInt32(data, 0) & int.MaxValue;
            return value % (maxExclusive - minInclusive) + minInclusive;
        }

        public static int Modulo(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
