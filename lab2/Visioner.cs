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
                var sub1 = ReadSubs(1);
                var sub2 = ReadSubs(2);

                Console.Write($"Введите текст для шифрования: ");
                var text = Console.ReadLine();

                string encoded = EncryptWithSubstitution(text, sub1, sub2);
                Console.WriteLine($"Зашифрованный текст: {encoded}");

                Console.WriteLine($"Расшифрованный обратно текст: {DecryptWithSubstitution(encoded, sub1, sub2)}");
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

        private Dictionary<char, char> ReadSubs(int i)
        {
            Dictionary<char, char> res = new Dictionary<char, char>();
            using (var sr = new StreamReader($"subs{i}.txt"))
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
        public static string EncryptWithSubstitution(string text, Dictionary<char, char> sub1, Dictionary<char, char> sub2)
        {
            string result = string.Empty;

            int step = 0;
            foreach (char character in text)
            {
                if (step == 0)
                {
                    if (sub1.ContainsKey(character))
                    {
                        result += sub1[character];
                    }
                    else
                    {
                        result += character;
                    }
                }
                else if (step == 1)
                {
                    if (sub2.ContainsKey(character))
                    {
                        result += sub2[character];
                    }
                    else
                    {
                        result += character;
                    }
                }
                step = (step + 1) % 2;
            }
            return result;
        }

        // Функция для расшифровки текста с системой подстановок
        public static string DecryptWithSubstitution(string text, Dictionary<char, char> sub1, Dictionary<char, char> sub2)
        {
            string result = string.Empty;
            Dictionary<char, char> rSub1 = new Dictionary<char, char>();
            Dictionary<char, char> rSub2 = new Dictionary<char, char>();

            // Создаем обратную систему подстановок для расшифровки
            foreach (var pair in sub1)
            {
                rSub1[pair.Value] = pair.Key;
            }
            foreach (var pair in sub2)
            {
                rSub2[pair.Value] = pair.Key;
            }

            int step = 0;
            foreach (char character in text)
            {
                if (step == 0)
                {
                    if (rSub1.ContainsKey(character))
                    {
                        result += rSub1[character];
                    }
                    else
                    {
                        result += character;
                    }
                }
                else if (step == 1)
                {
                    if (rSub2.ContainsKey(character))
                    {
                        result += rSub2[character];
                    }
                    else
                    {
                        result += character;
                    }
                }
                step = (step + 1) % 2;
            }
            return result;
        }
    }
}
