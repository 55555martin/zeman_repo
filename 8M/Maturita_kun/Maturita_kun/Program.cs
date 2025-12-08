namespace Maturita_kun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cesta = "../../../vstupni_soubory/1.txt"; // Cesta ke vstupnímu souboru
            using (StreamReader sr = new StreamReader(cesta))
            {
                // Načtení vstupu
                int pocet_prekazek = Int32.Parse(sr.ReadLine());
                List<Tuple<int,int>> prekazky = new List<Tuple<int,int>>();
                // Načte překážky i start a cíl
                for (int i = 0; i < pocet_prekazek+2; i++)
                {
                    string[] prekazka = sr.ReadLine().Split();
                    Tuple<int,int> pozice_prekazky = new Tuple<int, int>(Int32.Parse(prekazka[0]), Int32.Parse(prekazka[1]));
                    prekazky.Add(pozice_prekazky);
                }
                // Oddělí start a cíl z listu překážek
                Tuple<int, int> cil = prekazky.Last(); prekazky.RemoveAt(pocet_prekazek+1);
                Tuple<int, int> start = prekazky.Last(); prekazky.RemoveAt(pocet_prekazek);
                // Vytvoření šachovnice
                Sachovnice sachovnice = new Sachovnice(start, cil, prekazky);
                // Vypočítání a vypsání výsledku
                int vysledek = sachovnice.NajdiNejkratsiCestu();
                if (vysledek >= 0)
                    Console.WriteLine(vysledek);
                else
                    Console.WriteLine("Do cíle se koněm nejde dostat.");
            }
        }
    }

    public class Sachovnice
    {
        public Policko[,] Policka { get; } // Pole 8x8 s políčky
        private Queue<int[]> fronta_pozic { get; } // Fronta, ve které mám možné pozice koně, obsahuje int[3]: [0] je počet tahů ze startu, [1] a [2] je pozice
        private Tuple<int,int> cil { get; } // Souřadnice cíle
        public Sachovnice(Tuple<int,int> start_pozice, Tuple<int, int> cil_pozice, List<Tuple<int,int>> prekazky)
        {
            fronta_pozic = new Queue<int[]>();
            // Vytvoření políček v šachovnici
            Policka = new Policko[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Policka[i, j] = new Policko();
                }
            }
            // Umístění překážek
            foreach (Tuple<int,int> p in prekazky)
            {
                Policka[p.Item1, p.Item2].Prekazka = true;
            }
            // Přidání startovního políčka do fronty
            int[] prvni_prvek_fronty = new int[3];
            prvni_prvek_fronty[0] = 0;
            prvni_prvek_fronty[1] = start_pozice.Item1;
            prvni_prvek_fronty[2] = start_pozice.Item2;
            fronta_pozic.Enqueue(prvni_prvek_fronty);
            cil = cil_pozice;
        }

        public int NajdiNejkratsiCestu()
        {
            while (fronta_pozic.Count > 0)
            {
                // Načtení pozice z fronty
                int[] aktualni = fronta_pozic.Dequeue();
                int pocet_tahu = aktualni[0];
                Tuple<int, int> souradnice = new Tuple<int, int>(aktualni[1], aktualni[2]);
                Policko policko = Policka[aktualni[1], aktualni[2]];
                if (souradnice.Item1 == cil.Item1 && souradnice.Item2 == cil.Item2) // Cíl dosažen
                    return pocet_tahu;
                else if (policko.Navstiveno) // Už jsem z tohoto políčka vycházel dříve
                    continue;
                policko.Navstiveno = true;
                // Projití možných pohybů a přidání do fronty, pokud je pohyb validní
                int[,] mozne_pohyby = new int[8, 2] { { -2, -1 }, { -2, 1 }, { -1, -2 }, { -1, 2 }, { 1, -2 }, { 1, 2 }, { 2, -1 }, { 2, 1 } };
                for (int i = 0; i < 8; i++)
                {
                    // Nová pozice
                    int x = souradnice.Item1 + mozne_pohyby[i, 0];
                    int y = souradnice.Item2 + mozne_pohyby[i, 1];
                    // Pokud je [x,y] mimo šachovnici
                    if (!(0 <= x && x < 8 && 0 <= y && y < 8))
                        continue;
                    // Přidat políčko do fronty, pokud tam není překážka a ještě jsme neprošli přes něj
                    Policko nove_policko = Policka[x, y];
                    if (nove_policko.Navstiveno || nove_policko.Prekazka)
                        continue;
                    else
                    {
                        int[] novy_prvek_fronty = new int[3] { pocet_tahu + 1, x, y };
                        fronta_pozic.Enqueue(novy_prvek_fronty);
                    }
                }
            }
            return -1; // Nelze se dostat do cíle
        }
    }

    public class Policko
    {
        public bool Prekazka { get; set; } // Je na políčku překážka?
        public bool Navstiveno { get; set; } // Vyšel jsem již z tohoto políčka?
        public Policko()
        {
            Prekazka = false;
            Navstiveno = false;
        }
    }
}
