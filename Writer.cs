﻿using System;
using System.Threading;

namespace ReadWriter
{
    public class Writer
    {
        private readonly Management management;
        private readonly Random random = new Random();
        private readonly string name;

        public Writer(string name, Management management)
        {
            this.name = name;
            this.management = management;
        }

        public void Write()
        {
            try
            {
                while (true)
                {
                    management.OpenWriter(name);
                    Thread.Sleep(random.Next(3000));
                    management.CloseWriter(name);
                    Thread.Sleep(random.Next(6000));
                }
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}