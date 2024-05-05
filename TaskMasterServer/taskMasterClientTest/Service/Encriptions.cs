using System.Text;


namespace taskMasterClientTest.Service
{
    internal class Encriptions
    {

        private readonly static int _key = 5;

        internal static string EncryptString(string word)
        {
            byte[] wordBytes = Encoding.UTF8.GetBytes(word);
            byte[] encryptedBytes = new byte[wordBytes.Length];

            for (int i = 0; i < wordBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(wordBytes[i] ^ KeyEncription());
            }

            string result = Encoding.UTF8.GetString(encryptedBytes);

            return result;
        }

        internal static string DecryptString(string word)
        {
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(word);

            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ KeyEncription());
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private static int KeyEncription()
        {
            int key = _key;
            key += (int)DateTime.Now.DayOfWeek;
            return key;
        }
    }
}
