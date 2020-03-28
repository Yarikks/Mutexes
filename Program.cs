using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutexes
{
    class Program
    {
        static int result = 0;
        static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            Thread firstThread = new Thread(Increment);
            Thread secondThread = new Thread(Decrement);

            firstThread.Start();
            secondThread.Start();

            firstThread.Join();
            secondThread.Join();

            Console.WriteLine($"Result after two threads is {result}");

            Console.ReadLine();
        }

        private static void Increment()
        {
            int n = 0;
            Console.WriteLine("Thread 1 is waiting for mutex.");
            mutex.WaitOne();
            Console.WriteLine("Thread 1 is getting mutex.");

            do
            {
                n++;
                result = n;
                Console.WriteLine($"Result is {result}");
            } while (n < 5);

            Console.WriteLine("Thread 1 frees mutex.\n");
            mutex.ReleaseMutex();
        }

        private static void Decrement()
        {
            int n = 5;
            Console.WriteLine("Thread 2 is waiting for mutex.");
            mutex.WaitOne();
            Console.WriteLine("Thread 2 is getting mutex.");

            do
            {
                n--;
                result = n;
                Console.WriteLine($"Result is {result}");
            } while (n > 0);

            Console.WriteLine("Thread 2 frees mutex.\n");
            mutex.ReleaseMutex();
        }
    }
}
