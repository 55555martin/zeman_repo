using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uvodni_hodina
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Film film1 = new Film("Ten nejlepší film pod sluncem", "Martin", "Zeman", 2025);
            Film film2 = new Film("Taky dobrej film", "Pepa", "Omáčka", 2007);
            Film film3 = new Film("Filmíček", "Jan", "Novák", 2031);
            List<Film> filmy = new List<Film>() {film1, film2, film3};
            Random random = new Random();
            for (int i = 0; i < filmy.Count; i++)
            {
                Film film = filmy[i];
                for (int j = 0; j < 15; j++)
                {
                    double cislo = random.NextDouble()*5;
                    Console.WriteLine(cislo);
                    film.PridatHodnoceni(cislo);
                }
            }
            Film nejHodnoceni = null;
            Film nejdNazev = null;
            foreach (Film film in filmy)
            {
                if (nejHodnoceni == null)
                {
                    nejHodnoceni = film;
                    nejdNazev = film;
                }
                else if (film.Hodnoceni > nejHodnoceni.Hodnoceni)
                {
                    nejHodnoceni = film;
                }
                if (film.Nazev.Length > nejdNazev.Nazev.Length)
                {
                    nejdNazev = film;
                }
                if (film.Hodnoceni < 3)
                {
                    Console.WriteLine($"{film.Nazev} je odpad! Má hodnocení jen {film.Hodnoceni}.");
                }
            }
            Console.WriteLine("Film s nejlepším průměrným hodnocením: " + nejHodnoceni.ToString());
            Console.WriteLine("Film s nejdelším názvem: " + nejdNazev.ToString());
            Console.WriteLine("Výpis filmů:");
            foreach (Film film in filmy)
            {
                Console.WriteLine(film.ToString());
            }
            Console.ReadLine();
        }
    }

    public class Film
    {
        public string Nazev { get; }
        public string JmenoRezisera { get; }
        public string PrijmeniRezisera { get; }
        public int RokVzniku { get; }
        public double Hodnoceni { get; private set; }
        public List<double> SeznamHodnoceni { get; private set; }
        public Film(string n, string jr, string pr, int rv)
        {
            Nazev = n;
            JmenoRezisera = jr;
            PrijmeniRezisera = pr;
            RokVzniku = rv;
            Hodnoceni = 0;
            SeznamHodnoceni = new List<double>();
        }
        public void PridatHodnoceni(double h)
        {
            int pocet = SeznamHodnoceni.Count;
            SeznamHodnoceni.Add(h);
            Hodnoceni = (pocet * Hodnoceni + h) / (pocet + 1);
        }
        public void VypsatHodnoceni()
        {
            string vypis = String.Join("⭐\n", SeznamHodnoceni.ToArray());
            Console.WriteLine("Seznam hodnocení:\n" + vypis + "⭐");
        }
        public override string ToString()
        {
            char inicial = JmenoRezisera[0];
            return $"{Nazev} ({RokVzniku}; {PrijmeniRezisera}, {inicial}): {Hodnoceni} ⭐";
        }
    }
}
