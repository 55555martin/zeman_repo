using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Union_find
{
    internal class Program
    {
        public class UnionFind
        {
            private int[] parent;
            private int[] hloubka;
            public UnionFind(int n)
            {

                parent = new int[n];
                hloubka = new int[n];
                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    hloubka[i] = 0;
                }
            }

            public int Koren(int vrchol)
            {
                while (parent[vrchol] != vrchol)
                {
                    vrchol = parent[vrchol];
                }
                return vrchol;
            }

            public bool Find(int u, int v)
            {
                if (Koren(u) == Koren(v))
                    return true;
                return false;
            }

            public void Union(int u, int v)
            {
                int a = Koren(u);
                int b = Koren(v);
                if (a != b)
                {
                    if (hloubka[a] < hloubka[b])
                        parent[a] = b;
                    else if (hloubka[b] < hloubka[a])
                        parent[b] = a;
                    else
                    {
                        parent[b] = a;
                        hloubka[a]++;
                    }
                }
            }
        }
        
        static void Main(string[] args)
        {

        }

        static int?[,] Kruskal(List<int[]> hrany, int n)
        {
            int?[,] kostra = new int?[n, n];
            UnionFind komponenty = new UnionFind(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    kostra[i, j] = null;
                }
            }
            hrany.Sort((sa1, sa2) => sa1[2].CompareTo(sa2[2]));
            foreach (int[] hrana in hrany)
            {
                if (komponenty.Find(hrana[0], hrana[1]))
                {
                    komponenty.Union(hrana[0], hrana[1]);
                    kostra[hrana[0], hrana[1]] = hrana[2];
                    //kostra[hrana[1], hrana[0]] = hrana[2]; => toto pouze pokud jsou hrany neorientované
                }
            }
            return kostra;
        }
    }
}
