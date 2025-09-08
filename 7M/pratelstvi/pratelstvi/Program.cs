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
            for (int x = 0; x < n+1; x++) // n+1 je protože lidé jsou indexováni od 1 do n, nikoliv od 0 do n-1, jak to funguje v prog. jazycích
            {
                kamaradi[x] = new List<int>();
            }
            string[] pratelstvi = Console.ReadLine().Split(' ');
            foreach (string x in pratelstvi)
            {
                string[] spoj = x.Split('-');
                int i1 = Int32.Parse(spoj[0]);
                int i2 = Int32.Parse(spoj[1]);
                kamaradi[i1].Add(i2);
                kamaradi[i2].Add(i1);
            }
            string[] startcil = Console.ReadLine().Split(' ');
            int start = Int32.Parse(startcil[0]);
            int cil = Int32.Parse(startcil[1]);
            Dictionary<int,bool> projite = new Dictionary<int, bool>();
            List<int[]> cesty = new List<int[]>();
            int pocetcest = 0;
            projite.Add(start,true);
            foreach (int x in kamaradi[start])
            {
                projite.Add(x, true);
                int[] cesta = new int[2];
                cesta[0] = start;
                cesta[1] = x;
                cesty.Add(cesta);
                pocetcest++;
            }
            int i = 0;
            int[] konecnaCesta = null;
            while (i<pocetcest)
            {
                int[] cesta = cesty[i];
                int delka = cesta.Length;
                int prvek = cesta.Last();
                foreach (int x in kamaradi[prvek])
                {
                    if (!projite.ContainsKey(x))
                    {
                        projite.Add(x, true);
                        int[] novaCesta = new int[delka+1];
                        for (int y = 0; y < delka; y++)
                        {
                            novaCesta[y] = cesta[y];
                        }
                        novaCesta[delka] = x;
                        cesty.Add(novaCesta);
                        pocetcest++;
                        if (x == cil)
                        {
                            konecnaCesta = novaCesta;
                            break;
                        }
                    }
                }
                if (konecnaCesta != null)
                    break;
                i++;
            }
            if (konecnaCesta == null)
                Console.WriteLine("neexistuje");
            else
            {
                foreach (int x in konecnaCesta)
                {
                    Console.Write(x.ToString()+" ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        } // časová složitost O(n^2)
    }     // paměťová složitost string[] pratelstvi-O(m); List<int>[] kamaradi-O(2m); List<int> projite-O(n); List<int[]> cesty-O(n*n)
}         //                    celkem -> O(n^2+m)
