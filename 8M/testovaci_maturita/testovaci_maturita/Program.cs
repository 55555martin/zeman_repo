using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testovaci_maturita
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Graf // Třída s údaji o grafu
    {
        List<Node> vrcholy { get; } // List s jednotlivými vrcholy (seřazené podle indexu)

        PriorityQueue<List<Node>, int> fronta { get; }
    }


    class Node // Vrchol v grafu
    {
        bool otevreny { get; set; } // True pokud jsem z vrcholu ještě nevyšel, False pokud jsem již vyšel
        Dictionary<Node, int[]> sousedi { get; } // Slovník s vrcholy ke kterým vede z tohoto vrcholu hrana, value je pak dvojice hodnot - délka hrany, 0 nebo 1 dle toho zda je silnice placená
        
        Node()
        {
            this.otevreny = true;
            this.sousedi = new Dictionary<Node, int[]>();
        }

        public void PridejSouseda(Node node, int delka, int placeno)
        {
            int[] array = new int[2] {delka,placeno};
            this.sousedi.Add(node, array);
        }
    }
}
