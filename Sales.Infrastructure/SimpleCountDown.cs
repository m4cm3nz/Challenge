using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Infrastructure
{
    public class SimpleCountDown
    {
        static object @lock = new object();
        int countDown;

        public int CurrentCount => countDown;

        public void AddCount()
        {
            lock(@lock) { countDown++; }
            
        }

        public void Decrement()
        {
            lock (@lock) { countDown--; }
        }
    }
}
