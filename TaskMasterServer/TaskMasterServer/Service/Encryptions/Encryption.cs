using System.Text;

namespace TaskMasterServer.Service.Encryptions
{
    /// <summary>
    /// Класс работы с шифрами
    /// Планируются глобальное изменения
    /// </summary>
    internal class Encryption
    {
        /// <summary>
        /// Ключ по умолчанию
        /// </summary>
        private readonly static int _key = 5;

        /// <summary>
        /// Метод шифрующие строку
        /// </summary>
        /// <param name="word">Строка для шифровавания</param>
        /// <returns>Возвращает зашифрованную строку</returns>
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

        /// <summary>
        /// Метод расшифровки строки
        /// </summary>
        /// <param name="word">Строка для расшифровки</param>
        /// <returns>Возвращает расшифрованную строку</returns>
        internal static string DecryptString(string word) // Расшифровываем данные
        {
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(word);

            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ KeyEncription());
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        /// <summary>
        /// Метод меняющий ключ шифрования каждый день
        /// </summary>
        /// <returns>Возвращает актуальный ключ</returns>
        private static int KeyEncription()
        {
            int key = _key;
            key += (int)DateTime.Now.DayOfWeek;
            return key;
        }
    }
}
