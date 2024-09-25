using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2
{
    internal class Frequency
    {
        public Frequency()
        {
            var freq = ReadFreqs();
            var text = ReadText();

            var freqText = Count(text);

            var subMap = CreateMap(freqText, freq);

            Console.WriteLine($"Зашифрованный текст: \n{text}");
            Console.WriteLine($"\nРасшифрованный текст: \n{Encode(text, subMap)}");
        }

        private string ReadText()
        {
            string text;
            using (var sr = new StreamReader("text.txt"))
                text = sr.ReadToEnd();
            return text;
        }

        private List<string> ReadFreqs()
        {
            var list = new List<string>();
            using (var sr = new StreamReader("freq.txt"))
                list = sr.ReadToEnd().Split(' ').ToList();
            return list;
        }

        private List<string> Count(string text)
        {
            var frequencyDict = new Dictionary<char, double>();
            int totalLetters = 0;

            foreach (char character in text)
            {
                if (frequencyDict.ContainsKey(character))
                    frequencyDict[character]++;
                else
                    frequencyDict[character] = 1;

                totalLetters++;
            }

            var result = frequencyDict.OrderByDescending(pair => pair.Value).Select(x => x.Key.ToString()).ToList();

            return result;
        }

        private Dictionary<string, string> CreateMap(List<string> cipherFreq, List<string> rusFreq)
        {
            var res = new Dictionary<string, string>();
            for (int i = 0; i < cipherFreq.Count && i < rusFreq.Count; i++)
                res.Add(cipherFreq[i], rusFreq[i]);
            return res;
        }

        private string Encode(string text, Dictionary<string, string> map)
        {
            string res = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                string cur = text[i].ToString();

                if (map.ContainsKey(cur))
                    res += map[cur];
                else
                    res += cur;
            }
            return res;
        }
    }
}
