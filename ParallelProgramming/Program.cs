using System;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите номер лабораторной работы(1,3,4,5): ");
            string numLab = Console.ReadLine();
            switch (numLab)
            {
                case "1":
                    Console.Write("Введите число: ");
                    int str = Convert.ToInt32(Console.ReadLine());
                    new Thread(() => { Lab1(str); }).Start(); break;
                case "3":
                    new TaskLab().Lab3(); break;
                case "4":
                    Lab4(); break;
                case "5":
                    Lab5(); break;
                default:
                    Console.WriteLine("Не верно введен номер лаб. работы!!!"); break;
            }
            Console.WriteLine($"Конец");
            Console.ReadKey();
        }
        
        static void Lab1(int num)
        {
            try
            {
                if (num > 0)
                {
                    int max = num;
                    for (int i = 0; i < max; i++)
                    {
                        num += i;
                    }
                    Console.WriteLine($"Результат : {num}");
                }
                else if (num < 0)
                {
                    int min = num;
                    for (int i = min; i < 0; i++)
                    {
                        num += i;
                    }
                    Console.WriteLine($"Результат : {num}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                Console.Read();
            }
        }

        static void Lab4()
        {
            List<int> array = new List<int>();

            for (int i = 1; i < 64; i++)
            {
                array.Add(i);
            }

            array.AsParallel().ForAll(x =>
            {
                double value = Math.Pow(x, 2);
                Console.WriteLine($"{x}^2 = {value}");
            });
        }

        static async Task Lab5()
        {
            var result = await MethodAsync(25);

            foreach (var i in result)
            {
                Console.WriteLine(i);
            }
        }

        static async Task<int[]> MethodAsync(int n)
        {
            return await Task.Run(() =>
            {
                var range = Enumerable.Range(0, n);
                return range.ToArray();
            });
        }
    }

    public class TaskLab
    {
        public void Lab3()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            new Task(() =>
            {
                int i = 0;
                while (!token.IsCancellationRequested)
                {
                    i++;
                    Console.WriteLine($"Ожидание {i}!!!");
                    Thread.Sleep(100);
                }
            }).Start();
            Thread.Sleep(1000);
            tokenSource.Cancel();
            Console.WriteLine("Ожидание окончено!!");

        }
    }
}
