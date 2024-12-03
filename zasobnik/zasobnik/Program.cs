using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace zasobnik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(ControlBrackets(Console.ReadLine()));
            VypsatRozklad(RozkladNaScitance(Int32.Parse(Console.ReadLine())));
            Console.ReadLine();
        }

        static List<List<int>> RozkladNaScitance(int n)
        {
            List<List<int>> vysledek = new List<List<int>>{new List<int>{n}};
            Stack<List<int>> zasobnik = new Stack<List<int>>();
            zasobnik.Push(new List<int>{n});
            while (zasobnik.Any())
            {
                List<int> aktualni = zasobnik.Pop();
                int delka = aktualni.Count();
                // Rozdělím poslední číslo na všechny součty dvou čísel a přidám to do zásobníku i výsledku
                // (minimální hodnota čísla bude předposlední číslo, abych tam neměl duplicity)
                int x = 1;
                if (delka != 1)
                {
                    x = aktualni[delka - 2];
                }
                for (int i = x; i < aktualni[delka-1]; i++)
                {
                    int a = i; int b = aktualni[delka-1] - i;
                    List<int> novy = new List<int>(aktualni);
                    novy.RemoveAt(delka - 1);
                    if (b >= a)
                    {
                        novy.Add(a);
                        novy.Add(b);
                        zasobnik.Push(novy);
                        vysledek.Add(novy);
                    }
                }
            }
            return vysledek;
        }

        static void VypsatRozklad(List<List<int>> rozklad)
        {
            foreach (List<int> i in rozklad)
            {
                Console.WriteLine(string.Join("+", i.Select(a => a.ToString()).ToArray()));
            }
        }

        static bool ControlBrackets(string text)
        {
            Stack<char> list = new Stack<char>();
            foreach (char c in text)
            {
                if (c == '('| c == '[' | c == '{' )
                {
                    list.Push(c);
                }
                else
                {
                    if (list.Any())
                    {
                        char last = list.Pop();
                        if (last == InvertBrackets(c))
                        {
                            continue;
                        }
                    }
                    return false;
                }
            }
            if (!list.Any())
                return true;
            return false;
        }

        static char InvertBrackets(char m)
        {
            switch (m)
            {
                case '(':
                    return ')';
                case ')':
                    return '(';
                case '[':
                    return ']';
                case ']':
                    return '[';
                case '{':
                    return '}';
                case '}':
                    return '{';
                default:
                    return m;
            }
        }
    }
}
