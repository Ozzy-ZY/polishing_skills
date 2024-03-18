using System;
using System.Text;

namespace polishing_skills
{
    internal class Encryption
    {
        private static StringBuilder key = new StringBuilder();
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
                                     "0123456789`~!@#$%^&*()-_=+{}[];:'',./|";

        public static void keyInit(int length)
        {
            for (int i = 0; i < length; i++)
            {
                key.Append(chars[random.Next(chars.Length)]);
            }
        }
        private static string roundKeyInit(int roundNumber)
        {
            char ch = (char)('a' + roundNumber);
            string roundKey = new string(ch, 4);
            return xorStrings(roundKey, key.ToString());
        }

        private static string xorStrings(string a, string b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Strings must be of equal length");
            }
            StringBuilder result = new StringBuilder(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                result.Append((char)(((a[i] ^ b[i]) % 95) + 33));
            }
            return result.ToString();
        }

        public static string encrypt(string block, int numOfRounds)
        {
            if (block.Length != 8)
            {
                throw new ArgumentException("Block must be 64 bits long");
            }
            if (key.Length == 0)
            {
                keyInit(4);
            }
            string left = block.Substring(0, 4);
            string right = block.Substring(4, 4);
            for (int i = 1; i <= numOfRounds; i++)
            {
                string temp = right;
                string roundkey = roundKeyInit(i);
                right = xorStrings(xorStrings(right, roundkey), left);
                left = temp;
            }
            return right + left;
        }
    }
}
