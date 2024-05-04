using System.Text;

namespace TaskMasterServer.Service.Encryption
{
    internal static class Encryption
    {
        private static int _key = 5;
        internal static string EncryptString(string word, int key) // Шифруем данные
        {
            byte[] wordBytes = Encoding.UTF8.GetBytes(word);
            byte[] encryptedBytes = new byte[wordBytes.Length];

            for (int i = 0; i < wordBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(wordBytes[i] ^ key);
            }

            string result = Encoding.UTF8.GetString(encryptedBytes);

            return result;
        }
        internal static string DecryptString(string word, int key) // Расшифровываем данные
        {
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(word);

            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ key);
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        //private int Key()
        //{
        //    int key = _key;
        //    key += DateTime.Now.DayOfWeek();
        //    return _key;
        //}
    }
}
