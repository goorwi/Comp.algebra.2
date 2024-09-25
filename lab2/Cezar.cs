using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2
{
    internal class Cezar
    {
        private List<string> alpha = new List<string>();
        private int shift;

        public Cezar()
        {
            Console.WriteLine($"1. Ввести вручную\n" +
                $"2. Считать из файла");
            var read = int.Parse(Console.ReadLine());

            if (read == 1) GetAlphabet();
            else if (read == 2) ReadData();
        }

        private void GetAlphabet()
        {
            Console.WriteLine("Введите алфавит через пробел");
            var alphas = Console.ReadLine().Split(' ');
            foreach (var a in alphas)
                alpha.Add(a);

            Console.Write("Введите сдвиг: ");
            shift = int.Parse(Console.ReadLine());
        }

        private void ReadData()
        {
            using (var sr = new StreamReader("cezar.txt"))
            {
                var alphas = sr.ReadLine().Split(' ');
                foreach (var a in alphas)
                    alpha.Add(a);

                shift = int.Parse(sr.ReadLine());
            }
        }

        public string Encode(string word)
        {
            var wordList = word.Select(x => x.ToString()).ToList();

            for (var i = 0; i < wordList.Count; i++)
            {
                if (alpha.Contains(wordList[i]))
                {
                    var index = alpha.IndexOf(wordList[i]);
                    index += shift;
                    index %= alpha.Count;
                    wordList[i] = alpha[index];
                }
            }

            string res = "";
            wordList.ForEach(x => res += x);
            return res;
        }

        public string Decode(string word)
        {
            var wordList = word.Select(x => x.ToString()).ToList();

            for (var i = 0; i < wordList.Count; i++)
            {
                if (alpha.Contains(wordList[i]))
                {
                    var index = alpha.IndexOf(wordList[i].ToString());
                    index -= shift;
                    index %= alpha.Count;
                    while (index < 0) index += alpha.Count;
                    wordList[i] = alpha[index];
                }
            }

            string res = "";
            wordList.ForEach(x => res += x);
            return res;
        }
    }
    internal class GeneralizedCezar
    {
        private List<string> alpha = new List<string>();
        private int a, b, m;

        public GeneralizedCezar()
        {
            Console.WriteLine($"1. Ввести вручную\n" +
                $"2. Считать из файла");
            var read = int.Parse(Console.ReadLine());

            if (read == 1) GetAlphabet();
            else if (read == 2) ReadData();
        }

        private void GetAlphabet()
        {
            Console.WriteLine("Введите алфавит через пробел");
            var alphas = Console.ReadLine().Split(' ');
            foreach (var a in alphas)
                alpha.Add(a);

            Console.Write("Введите a: ");
            a = int.Parse(Console.ReadLine());

            Console.Write("Введите b: ");
            b = int.Parse(Console.ReadLine());

            m = alpha.Count;

            if (NOD(a, m) != 1)
                throw new Exception("Параметры a и m не взаимно просты");
        }

        private void ReadData()
        {
            using (var sr = new StreamReader("cezar2.txt"))
            {
                alpha = sr.ReadLine().Split(' ').ToList();

                var nums = sr.ReadLine().Split(' ');
                a = int.Parse(nums[0]);
                b = int.Parse(nums[1]);
            }

            m = alpha.Count;

            if (NOD(a, m) != 1)
                throw new Exception("Параметры a и m не взаимно просты");
        }

        public string Encode(string word)
        {
            var wordList = word.Select(x => x.ToString()).ToList();

            for (var i = 0; i < wordList.Count; i++)
                if (alpha.Contains(wordList[i]))
                    wordList[i] = alpha[(a * alpha.IndexOf(wordList[i]) + b) % m];

            var res = "";
            wordList.ForEach(x => res += x);

            return res;
        }

        public string Decode(string word)
        {
            var wordList = word.Select(x => x.ToString()).ToList();

            //по условию a * a^-1 сравнима с 1 по модулю m
            double a_1 = (m + 1) / a;

            for (var i = 0; i < wordList.Count; i++)
                if (alpha.Contains(wordList[i]))
                    wordList[i] = alpha[(int)Math.Round(a_1 * (alpha.IndexOf(wordList[i]) + m - b) % m)];

            string res = "";
            wordList.ForEach(x => res += x);
            return res;
        }

        public static int NOD(int a, int b)
        {
            if (a == b)
                return a;
            else
                if (a > b)
                return NOD(a - b, b);
            else
                return NOD(b - a, a);
        }
    }
}
