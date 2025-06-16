using System;
using System.Collections.Generic;
using System.Linq;

namespace Topo_usp_Abeceda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadej lexikograficky seřazená slova oddělená mezerami:");
            string[] vstup = Console.ReadLine().Split(' ');
            Graph graf = new Graph(vstup);
            Console.ReadLine();
            //Bonusová zamyšlení:
            //(+10b) Bude uspořádání abecedy vždy jednoznačné? Odpověď napište do komentáře v programu.
            // Ne, např. zde "xyz xz" není jasný vztah x s ostatními
            //(+10b) Uveďte příklad vstupu, pro který takové uspořádání ani neexistuje.
            // např. zde "aa ab ac ca ba" - obsahuje cyklus (a<c<b<c)
        }

    }

    class Graph
    {
        private Dictionary<char, Node> nodes { get; set; }

        public Graph(string[] slova)
        {
            nodes = new Dictionary<char, Node>();

            // Zjistíme jaká písmena se v jazyku vyskytují
            foreach (var slovo in slova)
            {
                foreach (char ch in slovo)
                {
                    if (!nodes.ContainsKey(ch))
                        nodes[ch] = new Node(ch);
                }
            }

            // Určíme závislosti podle sousedních slov
            for (int i = 0; i < slova.Length - 1; i++)
            {
                string slovo1 = slova[i];
                string slovo2 = slova[i + 1];
                int len = Math.Min(slovo1.Length, slovo2.Length);

                for (int j = 0; j < len; j++)
                {
                    if (slovo1[j] != slovo2[j])
                    {
                        Node mensi = nodes[slovo1[j]];
                        Node vetsi = nodes[slovo2[j]];

                        if (!mensi.Succesors.Contains(vetsi)) // Chci se vyvarovat duplicitám, stačí checknout jen jedno, jsou na sobě závislé
                        {
                            mensi.Succesors.Add(vetsi);
                            vetsi.Predecesors.Add(mensi);
                        }
                        break; // první rozdílný znak určuje vztah
                    }
                }
            }

            // Setřídíme
            Result vysledek = UsporadejPismena();
            if (vysledek.cyklus)
                Console.WriteLine("obsahuje cyklus => nejde");
            else if (vysledek.viceznacnost)
                Console.WriteLine("například " + string.Join(" -> ", vysledek.poradi.Select(n => n.Name)) + ", ale víceznačné");
            else
                Console.WriteLine(string.Join(" -> ", vysledek.poradi.Select(n => n.Name)));
        }

        class Result
        {
            public Result()
            {
                poradi = new List<Node>();
                cyklus = false;
                viceznacnost = false;
            }
            public List<Node> poradi { get; set; }
            public bool cyklus { get; set; }
            public bool viceznacnost { get; set; }
        }

        private Result UsporadejPismena()
        {
            Result vysledek = new Result();

            void Visit(Node node)
            {
                if (vysledek.cyklus)
                    return;
                
                node.NodeState = Node.State.Open;
                foreach (Node succ in node.Succesors)
                {
                    if (succ.NodeState == Node.State.Unvisited)
                    {
                        Visit(succ);
                    }
                    else if (succ.NodeState == Node.State.Open)
                    {
                        vysledek.cyklus = true; // nalezen cyklus
                        return;
                    }
                }
                node.NodeState = Node.State.Closed;
                vysledek.poradi.Add(node); // výstupní pořadí v opačném směru
            }

            int pocet = 0; // Počet podzávislých částí, pokud jich bude více jak jedna, bude jazyk víceznačný
            foreach (Node node in nodes.Values)
            {
                if (vysledek.cyklus)
                    return vysledek;
                if (node.NodeState == Node.State.Unvisited & node.Predecesors.Count == 0) // Chci kvůli víceznačnosti pouze začátky (bez predecesorů)
                {
                    Visit(node);
                    pocet++;
                }
            }
            if (pocet > 1)
                vysledek.viceznacnost = true;

            vysledek.poradi.Reverse();
            return vysledek;
        }

        private Node DFS(Node initialNode) // vrací buď stok nebo null
        {
            initialNode.NodeState = Node.State.Open;
            int Time = 0;
            Node stok = null;
            DFS2(initialNode);
            return stok;

            void DFS2(Node node)
            {
                node.NodeState = Node.State.Open;
                Time += 1;
                node.InTime = Time;
                if (node.Succesors.Count > 0) // je kam pokračovat
                {
                    foreach (Node successor in node.Succesors)
                    {
                        if (successor.NodeState == Node.State.Unvisited)
                        {
                            DFS2(successor);
                            if (stok != null) // pokud se našel stok
                                return;
                        }
                    }
                    node.NodeState = Node.State.Closed;
                    Time += 1;
                    node.OutTime = Time;
                }
                else // našli jsme stok
                {
                    stok = node;
                    return;
                }

            }
        }
    }



    class Node
    {

        public Node(char letter)
        {
            Name = letter;

            Succesors = new List<Node>();
            Predecesors = new List<Node>();

            NodeState = State.Unvisited;
            InTime = null;
            OutTime = null;
        }
        public char Name { get; }

        public List<Node> Succesors { get; set; }
        public List<Node> Predecesors { get; set; }

        public enum State { Open, Unvisited, Closed }
        public State NodeState { get; set; }

        public int? InTime { get; set; }
        public int? OutTime { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

    }
}