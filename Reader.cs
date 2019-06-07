using System;
using System.Threading;

namespace ReadWriter
{
    public class Reader
    {
        private readonly string name;
        private readonly Management management;
        private readonly Random random = new Random();

        public Reader(string name, Management management)
        {
            this.name = name;
            this.management = management;
        }

        public void Read()
        {
            try
            {
                while (true)
                {
                    management.OpenReader(name);
                    Thread.Sleep(random.Next(5000));
                    management.CloseReader(name);
                }
            }
            catch (ThreadInterruptedException ex) { Console.WriteLine(ex.Message); }
        }
    }
}