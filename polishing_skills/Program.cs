namespace polishing_skills
{
    class Program
    {
        static void Main(string[] args)
        {
            Encryption.keyInit(4);
            string block = "ozzyozzy";
            string encrypted = Encryption.encrypt(block, 16);
            System.Console.WriteLine(encrypted);
            string[] blocks = { "ozzyozzy", "12345678", "abcdefgh", "zyxwvuts" };
            Encryption.encryptBlocksInParallel(blocks, 16);
            for(int i = 0; i < blocks.Length; i++) {
                Console.WriteLine(blocks[i]);
            }
        }
    }
}