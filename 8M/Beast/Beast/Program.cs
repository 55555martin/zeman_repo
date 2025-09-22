using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beast
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Labyrinth
    {
        public int sirka { get; }
        public int vyska { get; }
        public string[,] pole { get; set; }
        public Labyrinth(int sirka, int vyska, string[,] pole)
        {
            this.sirka = sirka;
            this.vyska = vyska;
            this.pole = pole;
        }
        public string Next(int x, int y, string smer)
        {
            switch (smer)
            {
                case ">":

            }
        }
    }

    public class Beast
    {
        public int[] pozice { get; set; }

    }
}
