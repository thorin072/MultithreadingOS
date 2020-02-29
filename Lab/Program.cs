using System;
using System.Threading;

namespace Lab
{
    class Program
    {
        static string nameThread = "ABC";
        static object locker = new object();
        static int[] M1;
        static int[] M2;

        static void Main(string[] args)
        {
            int n = 2;
            M1 = new int[n];
            M2 = new int[n];
            for (int i = 0; i < nameThread.Length; i++)
            { 
                Thread myThread = new Thread(new ParameterizedThreadStart(GenerationArray));
                myThread.Name = "Поток "+ nameThread[i];
                myThread.Start(n);
            }
            Console.WriteLine("Главный поток");

        }
        public static void GenerationArray(object n)
        {
            bool acqiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acqiredLock);
                Random rand = new Random();
                int size = (int)n;
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine("{0}:", Thread.CurrentThread.Name);
                    M1[i] = rand.Next(100);
                    M2[i] = rand.Next(100);
                    Thread.Sleep(200);
                }
            }
            finally
            {
                if (acqiredLock) Monitor.Exit(locker);
            }
        }
    }
}
