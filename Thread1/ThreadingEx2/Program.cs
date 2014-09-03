using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace FindSmallest
{
    class Program
    {
        List<int> smallestTotal = new List<int>(); 
        
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
            smallestTotal.Add(smallestSoFar);
            return smallestSoFar;
            
        }


        void Main()
        {
            int[] array = smallestTotal.ToArray();
            foreach (int[] data in Data)
            {
                Thread t = new Thread(() =>
                {
                int smallest = FindSmallest(data);
                Console.WriteLine("\t" + String.Join(", ", data) + "\n-> " + smallest); 
                });
                t.Start();
    
            }

            Thread t2 = new Thread(() =>
            {
                int thesmallest = FindSmallest(array);
                Console.WriteLine("Det mindste nummer er: " + thesmallest);
            });
            t2.Start();
        }
    }
}
