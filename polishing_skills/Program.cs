namespace polishing_skills
{
    class Program
    {
        static void Main(string[] args)
        {
            Encryption.keyInit(4);
            string block = "ozzyozzy";
            string encrypted = Encryption.encrypt(block, 166);
            System.Console.WriteLine(encrypted);

        }
    }
}