using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace manzelstvi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graf graf = new Graf();
            while(true)
            {
                bool vystup = graf.Propose();
                if (vystup)
                    break;
                graf.Reject();
            }
            for (int i = 0; i < graf.n; i++)
            {
                Console.WriteLine(graf.zadane[i]+1);
            }
            Console.ReadLine();
        }
    }
    public class Partita
    {
        public List<Queue<int>> preference {  get; }
        public Partita(List<Queue<int>> preference)
        {
            this.preference = preference;
        }
    }
    public class Graf
    {
        Partita[] partity {  get; }
        public int n {  get; }
        public int?[] zadane { get; set; } // mají tam buď null, nebo index z druhé partity (řešíme zadání jen ve směru z první do druhé)
        Dictionary<int, bool>[] pozadani { get; set; } // list pro každého muže (objekty z druhé partity), kdo je požádal, dict pro rychlejší vyhledávání (hash je efektivnější než list)
        
        public Graf() // Načte graf ze vstupu
        {
            this.n = Int32.Parse(Console.ReadLine());
            this.partity = new Partita[2];
            List<Queue<int>> list1 = new List<Queue<int>>();
            for (int i = 0; i < n; i++) // Načíst vstup jedné partity
            {
                Queue<int> que = new Queue<int>();
                string[] vstup = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++) 
                {
                    que.Enqueue(Int32.Parse(vstup[j]));
                }
                list1.Add(que);
            }
            List<Queue<int>> list2 = new List<Queue<int>>();
            for (int i = 0; i < n; i++) // načíst vstup druhé partity
            {
                Queue<int> que = new Queue<int>();
                string[] vstup = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                {
                    que.Enqueue(Int32.Parse(vstup[j]));
                }
                list2.Add(que);
            }
            this.partity[0] = new Partita(list1);
            this.partity[1] = new Partita(list2);
            this.zadane = new int?[n];
            this.pozadani = new Dictionary<int, bool>[n];
            for (int i = 0;i < n;i++)
            {
                this.pozadani[i] = new Dictionary<int, bool>();
            }
        }

        public bool Propose() // Všechny ženy, které jsou odmítnuté, tak požádají svoji nejlepší preferenci
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (zadane[i] == null)
                {
                    int vyvoleny = this.partity[0].preference[i].Dequeue() - 1;
                    pozadani[vyvoleny][i] = true;
                    zadane[i] = vyvoleny;
                }
                else
                    count++;
            }
            if (count == n) // Vrací true, pokavaď už jsou hotové preference a již není co měnit
                return true;
            else
                return false;
        }

        public void Reject() // Muži odmítnou ženy, které nechtějí
        {
            for (int i = 0; i < n; i++)
            {
                Queue<int> fronta = this.partity[1].preference[i];
                bool vybrano = false;
                for (int j = 0; j < n; j++) // Projde svoje preference od shora dolů, a jakmile potká nějakou, která ho požádala tak všechny ostatní bude odmítat
                {
                    int vyvolena = fronta.Dequeue() - 1;
                    fronta.Enqueue(vyvolena + 1); // Abych to tam později měl
                    if (pozadani[i].ContainsKey(vyvolena))
                    {
                        if (vybrano) // Odmítnout
                        {
                            pozadani[i].Remove(vyvolena);
                            zadane[vyvolena] = null;
                        }
                        else // Přijmout
                            vybrano = true;
                    }
                }
            }
        }
    }
}
