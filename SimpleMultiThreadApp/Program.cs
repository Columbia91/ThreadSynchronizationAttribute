using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace SimpleMultiThreadApp
{
    [Synchronization]
    class Printer : ContextBoundObject 
    {
        // даже если для какого то метода не требуется синхронизация вышеперечисленный атрибут будет блокировать потоки
        public void AddOne(object data)
        {
            // увеличить значение переменной на 1
            int value = (int)data;
            Interlocked.Increment(ref value);
            Console.WriteLine(value);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            int numb = 10;

            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(printer.AddOne));
                Console.WriteLine("Поток {0}", threads[i].ManagedThreadId);
            }

            foreach (var thread in threads)
                thread.Start(numb);

            Console.ReadLine();
        }
    }
}
