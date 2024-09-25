using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class Permutation
    {
        public Permutation() { }

        public List<string> permutation = new List<string>();

        public void Add(string x) => permutation.Add(x);

        public string Get(int i) => permutation[i];

        public void Change(int i, string y) => permutation[i] = y;

        public bool HasValue(string x) => permutation.Contains(x);

        public int Size => permutation.Count;

        public Permutation Copy()
        {
            var p = new Permutation();
            for (int i = 0; i < Size; i++)
                p.Add(permutation[i]);
            return p;
        }

        public override bool Equals(object obj)
        {
            Permutation other = obj as Permutation;
            if (this == null && other == null) return false;
            else if (this == null || other == null) return false;
            else if (this.Size != other.Size) return false;

            for (int i = 0; i < this.Size; i++)
            {
                if (!this.Get(i).Equals(other.Get(i))) return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < permutation.Count; i++)
                sb.Append($"{i + 1}\t");
            sb.Append("\n");
            for (var i = 0; i < permutation.Count; i++)
                sb.Append($"{permutation[i]}\t");
            return sb.ToString();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var choice = -1;
            do
            {
                Console.Clear();
                Console.Write($"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                    $"1. Шифр Цезаря\n" +
                    $"2. Аффинный шифр\n" +
                    $"3. Шифр Виженера с ключевой фразой\n" +
                    $"4. Шифр Виженера с системой подстановок\n" +
                    $"5. Расшифровать текст при помощи частотного словаря\n" +
                    $"0. Выход из программы\n" +
                    $"Задание: ");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                switch (choice)
                {
                    case 1:
                        {
                            var c = new Cezar();

                            Console.Write("Введите слово для кодирования: ");
                            Console.WriteLine($"Код: {c.Encode(Console.ReadLine())}");

                            Console.Write("Введите слово для декодирования: ");
                            Console.WriteLine($"Код: {c.Decode(Console.ReadLine())}");
                            break;
                        }
                    case 2:
                        {
                            var c = new GeneralizedCezar();

                            Console.Write("Введите слово для кодирования: ");
                            Console.WriteLine($"Код: {c.Encode(Console.ReadLine())}");

                            Console.Write("Введите слово для декодирования: ");
                            Console.WriteLine($"Код: {c.Decode(Console.ReadLine())}");
                            break;
                        }
                    case 3:
                        {
                            new Visioner(1);
                            break;
                        }
                    case 4:
                        {
                            new Visioner(2);
                            break;
                        }
                    case 5:
                        {
                            new Frequency();
                            break;
                        }
                }

                Console.WriteLine($"\n\nДля продолжения нажмите любую клавишу...");
                Console.ReadKey();
            } while (choice != 0);
        }
    }
}
