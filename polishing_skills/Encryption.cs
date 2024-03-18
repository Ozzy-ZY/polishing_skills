using System.Text;

namespace polishing_skills
{
    internal class Encryption
    {
        private static string key = "";
        public static void keyInit(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i = 0; i < length; i++)
            {
                key += chars[random.Next(chars.Length)];
            }
        }
        private static string roundKeyInit(int roundNumber)
        {
            char ch = (char)('a' + roundNumber);
            string roundKey = "";
            roundKey += ch;
            roundKey += ch;
            roundKey += ch;
            roundKey += ch;
            return xorStrings(roundKey, key);
        }
        private static string xorStrings(string a, string b)
        {
            if(a.Length != b.Length)
            {
                throw new ArgumentException("Strings must be of equal length");
            }
            string result = "";
            for (int i = 0; i < a.Length; i++)
            {
                result += (char)(((a[i] ^ b[i])%95)+33);
            }
            return result;
        }
        // the block must be 64 bits long
        public static string encrypt(string block, int numOfRounds)
        {
            if(block.Length != 8)
            {
                throw new ArgumentException("Block must be 64 bits long");
            }
            if (key == "")
            {
                keyInit(4);
            }
            string left = block.Substring(0, 4);
            string right = block[4..];
            for (int i = 1; i <= numOfRounds; i++)
            {
                string temp = right;
                string roundkey = roundKeyInit(i);
                right = xorStrings(xorStrings(right,roundkey),left);
                left = temp;
            }
            return right + left;
        }
    }
}
