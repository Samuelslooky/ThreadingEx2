using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace FindSmallest
{
    public static class Program
    {
        private static readonly int[][] Data = new int[][]{
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

        public static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                    
                }
            }

            return smallestSoFar;

        }


        static void Main()
        {
            List<int> smallestTotal = new List<int>(); 
            List<Task<int>> tasklist = new List<Task<int>>();

            Task<int>[] tasks = new Task<int>[Data.Length];

            foreach (int[] data in Data)
            {
                //Task<int> task = new Task<int>(() =>
                //{
                //    int result = FindSmallest(data);

                //});

                //Console.WriteLine("Det mindste tal: " + task.Result); 

                //Task t = Task.Run(() =>

                Task<int> task = new Task<int>(() =>
                {
                    int smallest = FindSmallest(data);
                    Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
                    return smallest;

                });

                tasklist.Add(task);

            }

            foreach (Task<int> task in tasklist)
            {
                task.Start();
            }


            Task.WaitAll(tasklist.ToArray());
            foreach (Task<int> task in tasklist)
            {
                Console.WriteLine(task.Id + " -- " + task.Status + " Result: " + task.Result);
                smallestTotal.Add(task.Result);
            }

            int[] array = smallestTotal.ToArray();
            int thesmallest = FindSmallest(array);
            Console.WriteLine("Det mindste tal er: " + thesmallest);

            Console.ReadLine();
        }
    }
}
