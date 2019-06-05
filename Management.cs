using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReadWriter
{
    public class Management
    {
        private int countReaders = 0;
        private int countWriter = 0;

        public void OpenReader(string name)
        {
            lock (this)
            {
                while (countWriter > 0)
                    Monitor.Wait(this);
                countReaders++;
                Console.WriteLine($"Reader '{name}' is reading");
            }
        }


        public void CloseReader(string name)
        {
            lock (this)
            {
                Console.WriteLine($"Reader '{name}' end of read.");
                countReaders--;
                if (countReaders == 0)
                    Monitor.PulseAll(this);
            }
        }


        public void OpenWriter(string name)
        {
            lock (this)
            {
                countWriter++;
                while (countReaders > 0)
                    Monitor.Wait(this);
                Console.WriteLine($"Writer '{name}' is writing");
            }
        }


        public void CloseWriter(string name)
        {
            lock (this)
            {
                countWriter--;
                Console.WriteLine($"Writer '{name}' end of write.");
                Monitor.PulseAll(this);
            }
        }
    }
}