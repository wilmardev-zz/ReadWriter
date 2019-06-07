using System;
using System.Threading;

namespace ReadWriter
{
    public class Management
    {
        private int countReaders = 0;
        private int countWriter = 0;
        private bool existWriter = false;

        public void OpenReader(string name)
        {
            Management management = this;
            lock (management)
            {
                while (existWriter || countWriter > 0)
                    Monitor.Wait(this);
                countReaders++;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Reader '{name}' is reading");
            }
        }

        public void CloseReader(string name)
        {
            Management management = this;
            lock (management)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Reader '{name}' end of read.");
                countReaders--;
                if (countReaders == 0)
                    Monitor.PulseAll(this);
            }
        }

        public void OpenWriter(string name)
        {
            Management management = this;
            lock (management)
            {
                countWriter++;
                while (existWriter || countReaders > 0)
                    Monitor.Wait(this);
                existWriter = true;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Writer '{name}' is writing");
            }
        }

        public void CloseWriter(string name)
        {
            Management management = this;
            lock (management)
            {
                countWriter--;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Writer '{name}' end of write.");
                existWriter = false;
                Monitor.PulseAll(this);
            }
        }
    }
}