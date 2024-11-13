using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pratelstvi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Int32.Parse(Console.ReadLine()); // Počet lidí
            List<int>[] kamaradi = new List<int>[n+1];
            for (int i = 0; i < n+1; i++)
            {
                kamaradi[i] = new List<int>();
            }
            string[] pratelstvi = Console.ReadLine().Split(' ');
            foreach (string i in pratelstvi)
            {
                string[] spoj = i.Split('-');
                int i1 = Int32.Parse(spoj[0]);
                int i2 = Int32.Parse(spoj[1]);
                kamaradi[i1].Add(i2);
                kamaradi[i2].Add(i1);
            }
            string[] startcil = Console.ReadLine().Split(' ');
            int start = Int32.Parse(startcil[0]);
            int cil = Int32.Parse(startcil[1]);
            List<int> projite = new List<int>();
            List<int[]> cesty = new List<int[]>();
            int pocetcest = 0;
            projite.Add(start);
            foreach (int i in kamaradi[start])
            {
                projite.Add(i);
                int[] cesta = new int[2];
                cesta[0] = start;
                cesta[1] = i;
                cesty.Add(cesta);
            }
            int i = 0;
            while (i<cesty.Count())
            {
                int[] cesta = cesty[i];
                int prvek = cesta.Last();

            }
        }
    }
}
