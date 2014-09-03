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
            List<Task> tasklist = new List<Task>();

            foreach (int[] data in Data)
            {
                Task t = Task.Run(() =>
                {
                int smallest = FindSmallest(data);
                Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest);
                smallestTotal.Add(smallest);
                });
                
                tasklist.Add(t);
                
            }

            Task.WaitAll(tasklist.ToArray());
            foreach (Task t in tasklist)
            {
                Console.WriteLine(t.Id + " -- " + t.Status);
            }

            int[] array = smallestTotal.ToArray();
            int thesmallest = FindSmallest(array);
            Console.WriteLine("Det mindste tal er: " + thesmallest);

            Console.ReadLine();
        }
    }
}
