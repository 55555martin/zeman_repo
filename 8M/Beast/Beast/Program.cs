using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Beast
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sirka = Int32.Parse(Console.ReadLine());
            int vyska = Int32.Parse(Console.ReadLine());
            string[,] pole = new string[vyska,sirka];
            for (int i = 0; i < vyska; i++)
            {
                char[] vstup = Console.ReadLine().ToCharArray();
                for (int j = 0; j < sirka; j++)
                {
                    pole[i, j] = vstup[j].ToString();
                }
            }
            Labyrinth mapa = new Labyrinth(sirka, vyska, pole);
            for (int i = 0; i < 20; i++)
            {
                mapa.Turn();
            }
        }
    }

    public class Labyrinth
    {
        public int sirka { get; }
        public int vyska { get; }
        public string[,] pole { get; set; }
        private List<Beast> prisery { get; }
        public Labyrinth(int sirka, int vyska, string[,] pole)
        {
            this.sirka = sirka;
            this.vyska = vyska;
            this.pole = pole;
            this.prisery = new List<Beast>();
            // Najít příšery
            for (int i = 0; i < vyska; i++)
            {
                for (int j = 0; j < sirka; j++)
                {
                    int[] pozice = new int[2] { i, j };
                    int[] smer = new int[2];
                    switch (pole[i, j])
                    {
                        case "<":
                            smer[0] = 0; smer[1] = -1;
                            break;
                        case "^":
                            smer[0] = -1; smer[1] = 0;
                            break;
                        case ">":
                            smer[0] = 0; smer[1] = 1;
                            break;
                        case "v":
                            smer[0] = 1; smer[1] = 0;
                            break;
                    }
                    if (pole[i, j] != "." && pole[i, j] != "X")
                    {
                        prisery.Add(new Beast(this, pozice, smer));
                        pole[i, j] = ".";
                    }
                }
            }
        }

        public void Turn()
        {
            foreach(Beast prisera in prisery)
            {
                prisera.Tahni();
            }
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            string[,] print_pole = new string[vyska,sirka];
            Array.Copy(pole, print_pole, vyska*sirka);
            foreach (Beast prisera in prisery)
            {
                string smer;
                switch (prisera.smer)
                {
                    case [0,-1]:
                        smer = "<";
                        break;
                    case [-1, 0]:
                        smer = "^";
                        break;
                    case [0, 1]:
                        smer = ">";
                        break;
                    case [1, 0]:
                        smer = "v";
                        break;
                    default:
                        smer = ".";
                        break;
                }
                print_pole[prisera.pozice[0], prisera.pozice[1]] = smer;
            }
            string vysledek = "";
            for (int i = 0; i < print_pole.GetLength(0); i++)
            {
                for (int j = 0; j < print_pole.GetLength(1); j++)
                {
                    vysledek += print_pole[i, j];
                }
                vysledek += "\n";
            }
            return vysledek;
        }
    }

    public class Beast
    {
        private Labyrinth labyrinth { get; }
        public int[] pozice { get; set; }
        public int[] smer { get; set; }
        private Tah posledni { get; set; }
        public Beast(Labyrinth labyrinth, int[] pozice, int[] smer)
        {
            this.labyrinth = labyrinth;
            this.pozice = pozice;
            this.smer = smer;
            this.posledni = Tah.Nic;
        }

        public void Tahni()
        {
            string nasledujici = labyrinth.pole[pozice[0] + smer[0], pozice[1] + smer[1]];
            string napravo = labyrinth.pole[pozice[0] + smer[1], pozice[1] - smer[0]];
            if (posledni is Tah.Doprava)
                Pohyb();
            else if (napravo == "X" && nasledujici == "X")
                Doleva();
            else if (napravo == "X" && nasledujici == ".")
                Pohyb();
            else
                Doprava();
        }

        private void Pohyb()
        {
            pozice[0] += smer[0];
            pozice[1] += smer[1];
            posledni = Tah.Pohyb;
        }

        private void Doleva()
        {
            smer = [-smer[1], smer[0]];
            posledni = Tah.Doleva;
        }

        private void Doprava()
        {
            smer = [smer[1], -smer[0]];
            posledni = Tah.Doprava;
        }
    }

    public enum Tah
    {
        Nic,
        Doleva,
        Doprava,
        Pohyb
    }
}
