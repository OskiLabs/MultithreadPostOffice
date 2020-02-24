using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsFormsApplication1
{

    class Okienko
    {
        public bool typOkienka;
        public int liczbaOsob;
        public int polozenie;
        public Semaphore pierwszySem = new Semaphore(1, 1);
        public Semaphore drugiSem = new Semaphore(1, 1);
        public Semaphore trzeciSem = new Semaphore(1, 1);
        public Semaphore czwartySem = new Semaphore(1, 1);
        public Semaphore piatySem = new Semaphore(1, 1);
        public Semaphore szostySem = new Semaphore(1, 1);
        public Semaphore siodmySem = new Semaphore(1, 1);
        public Semaphore osmySem = new Semaphore(1, 1);
        public int flaga1 = 1;
        public int flaga2 = 1;
        public int flaga3 = 1;
        public int flaga4 = 1;
        public int flaga5 = 1;
        public int flaga6 = 1;
        public int flaga7 = 1;
        public int flaga8 = 1;
    }
}
