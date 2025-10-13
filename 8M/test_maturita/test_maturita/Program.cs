<<<<<<< Updated upstream
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class Graf // Třída s údaji o grafu
    {
        int m { get; } // Počet měst
        int s { get; } // Počet silnic
        public Node[] vrcholy { get; } // Array s jednotlivými vrcholy (seřazené podle indexu)
        public PriorityQueue<List<Node>, int> fronta { get; set; } // Fronta s jednotlivými cestami (list vrcholů přes které vede cesta) s prioritou délky cesty
        public int cil { get; } // Kam se chci dostat?

        public Graf() // konstruktor načte vstup z konzole, volání konstruktoru je očekáváno v bloku try, chybný vstup vyhodí chybu
        {
            string[] vstup1 = Console.ReadLine().Split();
            if (vstup1.Length != 2) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
            this.m = Convert.ToInt32(vstup1[0]);
            this.s = Convert.ToInt32(vstup1[1]);
            if (m <= 0 || s < 0) { throw new Exception("Počet měst a silnic musí být kladný"); } // pokud je chybný vstup vyhodí chybu
            this.vrcholy = new Node[this.m];
            for (int i = 0; i < this.m; i++)
            {
                vrcholy[i] = new Node();
            }
            for (int i = 0; i < this.s; i++)
            {
                string[] vstup2 = Console.ReadLine().Split();
                if (vstup2.Length != 4) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
                int m1 = Convert.ToInt32(vstup2[0]); // výchozí město
                int m2 = Convert.ToInt32(vstup2[1]); // cílové město
                int d = Convert.ToInt32(vstup2[2]); // délka silnice
                int p = Convert.ToInt32(vstup2[3]); // placeno?
                if((p != 0 && p != 1) || d <= 0) { throw new Exception("Nesprávně zadaná délka silnice, nebo informace o tom, zdali je silnice placená"); } // pokud je chybný vstup vyhodí chybu
                // Propojí sousedy
                this.vrcholy[m1].PridejSouseda(this.vrcholy[m2], d, p);
                this.vrcholy[m2].PridejSouseda(this.vrcholy[m1], d, p);
            }
            string[] vstup3 = Console.ReadLine().Split();
            if (vstup3.Length != 2) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
            int start = Convert.ToInt32(vstup3[0]); // Odkud se chci dostat rovnou zapíšu do prioritní fronty
            List<Node> list = new List<Node>();
            list.Add(this.vrcholy[start]);
            fronta.Enqueue(list, 0);
            this.cil = Convert.ToInt32(vstup3[1]);
        }
    }


    public class Node // Vrchol v grafu
    {
        bool otevreny { get; set; } // True pokud jsem z vrcholu ještě nevyšel, False pokud jsem již vyšel
        Dictionary<Node, int[]> sousedi { get; } // Slovník s vrcholy ke kterým vede z tohoto vrcholu hrana, value je pak dvojice hodnot - délka hrany, 0 nebo 1 dle toho zda je silnice placená

        public Node() // konstruktor
        {
            this.otevreny = true;
            this.sousedi = new Dictionary<Node, int[]>();
        }

        public void PridejSouseda(Node node, int delka, int placeno)
        {
            int[] array = new int[2] { delka, placeno };
            this.sousedi.Add(node, array);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class Graf // Třída s údaji o grafu
    {
        int m { get; } // Počet měst
        int s { get; } // Počet silnic
        public Node[] vrcholy { get; } // Array s jednotlivými vrcholy (seřazené podle indexu)
        public PriorityQueue<List<Node>, int> fronta { get; set; } // Fronta s jednotlivými cestami (list vrcholů přes které vede cesta) s prioritou délky cesty
        public int cil { get; } // Kam se chci dostat?

        public Graf() // konstruktor načte vstup z konzole, volání konstruktoru je očekáváno v bloku try, chybný vstup vyhodí chybu
        {
            string[] vstup1 = Console.ReadLine().Split();
            if (vstup1.Length != 2) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
            this.m = Convert.ToInt32(vstup1[0]);
            this.s = Convert.ToInt32(vstup1[1]);
            if (m <= 0 || s < 0) { throw new Exception("Počet měst a silnic musí být kladný"); } // pokud je chybný vstup vyhodí chybu
            this.vrcholy = new Node[this.m];
            for (int i = 0; i < this.m; i++)
            {
                vrcholy[i] = new Node();
            }
            for (int i = 0; i < this.s; i++)
            {
                string[] vstup2 = Console.ReadLine().Split();
                if (vstup2.Length != 4) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
                int m1 = Convert.ToInt32(vstup2[0]); // výchozí město
                int m2 = Convert.ToInt32(vstup2[1]); // cílové město
                int d = Convert.ToInt32(vstup2[2]); // délka silnice
                int p = Convert.ToInt32(vstup2[3]); // placeno?
                if((p != 0 && p != 1) || d <= 0) { throw new Exception("Nesprávně zadaná délka silnice, nebo informace o tom, zdali je silnice placená"); } // pokud je chybný vstup vyhodí chybu
                // Propojí sousedy
                this.vrcholy[m1].PridejSouseda(this.vrcholy[m2], d, p);
                this.vrcholy[m2].PridejSouseda(this.vrcholy[m1], d, p);
            }
            string[] vstup3 = Console.ReadLine().Split();
            if (vstup3.Length != 2) { throw new Exception("Nesprávný počet objektů ve vstupu"); } // pokud je chybný vstup vyhodí chybu
            int start = Convert.ToInt32(vstup3[0]); // Odkud se chci dostat rovnou zapíšu do prioritní fronty
            List<Node> list = new List<Node>();
            list.Add(this.vrcholy[start]);
            fronta.Enqueue(list, 0);
            this.cil = Convert.ToInt32(vstup3[1]);
        }
    }


    public class Node // Vrchol v grafu
    {
        bool otevreny { get; set; } // True pokud jsem z vrcholu ještě nevyšel, False pokud jsem již vyšel
        Dictionary<Node, int[]> sousedi { get; } // Slovník s vrcholy ke kterým vede z tohoto vrcholu hrana, value je pak dvojice hodnot - délka hrany, 0 nebo 1 dle toho zda je silnice placená

        public Node() // konstruktor
        {
            this.otevreny = true;
            this.sousedi = new Dictionary<Node, int[]>();
        }

        public void PridejSouseda(Node node, int delka, int placeno)
        {
            int[] array = new int[2] { delka, placeno };
            this.sousedi.Add(node, array);
        }
    }
}
>>>>>>> Stashed changes
