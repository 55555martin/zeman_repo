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
        int n {  get; }
        int?[] zadane { get; set; } // mají tam buď null, nebo index z druhé partity (řešíme zadání jen ve směru z první do druhé)
        Dictionary<int, bool>[] pozadani { get; set; } // list pro každého muže (objekty z druhé partity), kdo je požádal, dict pro rychlejší vyhledávání (hash je efektivnější než list)
        Graf() // Načte graf ze vstupu
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
        bool Propose()
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
            if (count == 0)
                return true;
            else
                return false;
        }
        void Reject()
        {
            for (int i = 0; i<n; i++)
            {

            }
        }
    }
}
