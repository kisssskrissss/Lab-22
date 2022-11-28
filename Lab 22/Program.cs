using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> funk1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(funk1, n);

            Func<Task<int[]>, int> funk2 = new Func<Task<int[]>, int>(Max);
            Task<int> task2 = task1.ContinueWith<int>(funk2);

            Func<Task<int[]>, int> funk3 = new Func<Task<int[]>, int>(Sum);
            Task<int> task3 = task1.ContinueWith<int>(funk3);

            task1.Start();
            Console.ReadKey();
        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
             
                Console.Write("{0} ", array[i]);
               
            }
            return array;
        }

        static int Max(Task<int[]> task)
        {
            int[] array = task.Result;

            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
            }
            Console.WriteLine();
            Console.WriteLine(max);
            return max;
        }

        static int Sum(Task<int[]> task)
        {
            int[] array = task.Result;

            int sum = 0;
            foreach (int a in array)
            {
                sum += a;
            }
            Console.WriteLine();
            Console.WriteLine(sum);
            return sum;
        }

        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }
    }
}
