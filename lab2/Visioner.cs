using System;
using System.Collections.Generic;
using System.IO;

namespace lab2
{
    internal class Visioner
    {
        public Visioner(int type)
        {
            if (type == 1)
            {
                Console.Write($"Введите текст для шифрования: ");
                var text = Console.ReadLine();
                Console.Write($"Введите ключевую фразу: ");
                var key = Console.ReadLine();

                var encoded = Encrypt(text, key);
                Console.WriteLine($"Зашифрованный текст: {encoded}");

                Console.WriteLine($"Расшифрованный обратно текст: {Decrypt(encoded, key)}");
            }
            else if (type == 2)
            {
                var subs = ReadSubs();

                Console.Write($"Введите текст для шифрования: ");
                var text = Console.ReadLine();

                string encoded = EncryptWithSubstitution(text, subs);
                Console.WriteLine($"Зашифрованный текст: {encoded}");

                Console.WriteLine($"Расшифрованный обратно текст: {DecryptWithSubstitution(encoded, subs)}");
            }
        }

        // Функция для шифрования текста с ключевой фразой
        public static string Encrypt(string text, string key)
        {
            string result = string.Empty;
            key = key.ToLower();
            int keyIndex = 0;

            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    int shift = key[keyIndex] - 'a';

                    if (char.IsLower(character))
                    {
                        result += (char)((character - 'a' + shift) % 26 + 'a');
                    }
                    else if (char.IsUpper(character))
                    {
                        result += (char)((character - 'A' + shift) % 26 + 'A');
                    }

                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    result += character;  // Не шифруем не-буквы
                }
            }
            return result;
        }

        // Функция для расшифровки текста с ключевой фразой
        public static string Decrypt(string text, string key)
        {
            string result = string.Empty;
            key = key.ToLower();
            int keyIndex = 0;

            foreach (char character in text)
            {
                if (char.IsLetter(character))
                {
                    int shift = key[keyIndex] - 'a';

                    if (char.IsLower(character))
                    {
                        result += (char)((character - 'a' - shift + 26) % 26 + 'a');
                    }
                    else if (char.IsUpper(character))
                    {
                        result += (char)((character - 'A' - shift + 26) % 26 + 'A');
                    }

                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    result += character;  // Не расшифровываем не-буквы
                }
            }
            return result;
        }

        private Dictionary<char, char> ReadSubs()
        {
            Dictionary<char, char> res = new Dictionary<char, char>();
            using (var sr = new StreamReader("subs.txt"))
            {
                while (!sr.EndOfStream)
                {
                    var a = sr.ReadLine().Split(' ');
                    res.Add(a[0].ToCharArray()[0], a[1].ToCharArray()[0]);
                }
            }
            return res;
        }

        // Функция для шифрования текста с системой подстановок
        public static string EncryptWithSubstitution(string text, Dictionary<char, char> substitutions)
        {
            string result = string.Empty;

            foreach (char character in text)
            {
                if (substitutions.ContainsKey(character))
                {
                    result += substitutions[character];
                }
                else
                {
                    result += character;
                }
            }
            return result;
        }

        // Функция для расшифровки текста с системой подстановок
        public static string DecryptWithSubstitution(string text, Dictionary<char, char> substitutions)
        {
            string result = string.Empty;
            Dictionary<char, char> reverseSubstitutions = new Dictionary<char, char>();

            // Создаем обратную систему подстановок для расшифровки
            foreach (var pair in substitutions)
            {
                reverseSubstitutions[pair.Value] = pair.Key;
            }

            foreach (char character in text)
            {
                if (reverseSubstitutions.ContainsKey(character))
                {
                    result += reverseSubstitutions[character];
                }
                else
                {
                    result += character;
                }
            }
            return result;
        }
    }
}
