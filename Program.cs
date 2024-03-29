﻿using System;
using System.Threading;

namespace ReadWriter
{
    internal static class Program
    {
        private static readonly Management management = new Management();

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter the number of readers: ");
            ValidInputReaders(out string nReaders);
            Console.Write("Enter the number of writers: ");
            ValidInputWriters(out string nWriters);
            CreateWriters(nWriters, out Thread threadWriter);
            CreateReaders(nReaders, out Thread threadReader);
            threadWriter.Join();
            threadReader.Join();
            Console.ReadKey();
        }

        private static void ValidInputReaders(out string nReaders)
        {
            nReaders = Console.ReadLine();
            while (!int.TryParse(nReaders, out _))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>> ERROR!! Please enter a valid number <<<");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the number of readers: ");
                nReaders = Console.ReadLine();
            }
            while (Convert.ToInt32(nReaders) < 1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>> ERROR!! Please enter a number greater than 0 <<<");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the number of readers: ");
                nReaders = Console.ReadLine();
            }
            Console.WriteLine();
        }

        private static void ValidInputWriters(out string nWriters)
        {
            nWriters = Console.ReadLine();
            while (!int.TryParse(nWriters, out _))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>> ERROR!! Please enter a valid number <<<");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the number of writers: ");
                nWriters = Console.ReadLine();
            }
            while (Convert.ToInt32(nWriters) < 1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">>> ERROR!! Please enter a number greater than 0 <<<");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the number of writers: ");
                nWriters = Console.ReadLine();
            }
            Console.WriteLine();
        }

        private static void CreateReaders(string nReaders, out Thread threadReader)
        {
            threadReader = null;
            Reader[] readers = new Reader[Convert.ToInt32(nReaders)];
            for (int i = 0; i < readers.Length; i++)
            {
                readers[i] = new Reader((i + 1).ToString(), management);
                threadReader = new Thread(new ThreadStart(readers[i].Read));
                threadReader.Start();
            }
        }

        private static void CreateWriters(string nWriters, out Thread threadWriter)
        {
            threadWriter = null;
            Writer[] writers = new Writer[Convert.ToInt32(nWriters)];
            for (int i = 0; i < writers.Length; i++)
            {
                writers[i] = new Writer((i + 1).ToString(), management);
                threadWriter = new Thread(new ThreadStart(writers[i].Write));
                threadWriter.Start();
            }
        }
    }
}