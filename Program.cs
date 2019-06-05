using System;
using System.Threading;

namespace ReadWriter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Management management = new Management();
            Console.WriteLine("Ingrese Cantidad de Lectores");
            string nReaders = Console.ReadLine();

            //Console.WriteLine("Ingrese Cantidad de Escritores");
            //string nWriters = Console.ReadLine();
            Thread threadReader = null;
            Reader[] readers = new Reader[Convert.ToInt32(nReaders)];
            for (int i = 0; i < readers.Length; i++)
            {
                readers[i] = new Reader((i + 1).ToString(), management);
                threadReader = new Thread(new ThreadStart(readers[i].Read));
                threadReader.Start();
            }

            Writer[] writers = new Writer[3];
            Thread threadWriter = null;
            for (int i = 0; i < writers.Length; i++)
            {
                writers[i] = new Writer((i + 1).ToString(), management);
                threadWriter = new Thread(new ThreadStart(writers[i].Write));
                threadWriter.Start();
            }

            threadReader.Join();
            threadWriter.Join();
            Console.ReadKey();
        }
    }
}