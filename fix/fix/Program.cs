using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace fix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fix fix = new Fix();
            string[] vstup = fix.NactiVstup();
            float? vysledek = null;
            if (vstup.Length > 0)
            {
                try
                {
                    // testuju jestli je první prvek číslo -> Jedná se o prefix či postfix
                    float test = float.Parse(vstup[0], CultureInfo.InvariantCulture.NumberFormat);
                    // POSTFIX
                    vysledek = fix.Postfix(vstup);
                }
                catch
                {
                    // PREFIX
                    vysledek = fix.Prefix(vstup);
                }
            }
            if (vysledek != null)
            {
                Console.WriteLine($"{fix.ToInfix(vstup)} = {vysledek}");
            }
            Console.ReadLine();
        }
    }
    class Fix
    {
        public string[] NactiVstup()
        {
            Console.WriteLine("Co chceš spočítat?");
            string[] list = Console.ReadLine().Split(' ');
            return list;
        }

        public float? Postfix(string[] list)
        {
            Stack<float> zasobnik = new Stack<float>();
            for (int i = 0; i < list.Length; i++)
            {
                try
                {
                    float cislo = float.Parse(list[i], CultureInfo.InvariantCulture.NumberFormat);
                    zasobnik.Push(cislo); // Je to číslo
                }
                catch // Je to operátor
                {
                    char oper = Convert.ToChar(list[i]);
                    float b;
                    float a;
                    try
                    {
                        b = zasobnik.Pop();
                        a = zasobnik.Pop();
                    }
                    catch
                    {
                        Console.WriteLine("Neplatný vstup!"); // Nedostatek prvků v zásobníku
                        return null;
                    }
                    switch (oper)
                    {
                        case '+':
                            zasobnik.Push(a + b);
                            break;
                        case '-':
                            zasobnik.Push(a - b);
                            break;
                        case '*':
                            zasobnik.Push(a * b);
                            break;
                        case '/':
                            if (b == 0)
                            {
                                Console.WriteLine("Neděl nulou!!!!!!!!");
                                return null;
                            }
                            else
                            {
                                zasobnik.Push(a / b);
                            }
                            break;
                        default:
                            Console.WriteLine("Neznámý znak:", oper);
                            return null;
                    }
                }
            }
            if(zasobnik.Count == 1)
            {
                return zasobnik.Pop();
            }
            else
            {
                Console.WriteLine("Neplatný vstup!"); // Moc prvků v zásobníku
                return null;
            }
        }

        public float? Prefix(string[] list)
        {
            Stack<float> zasobnik = new Stack<float>();
            for (int i = list.Length-1; i >= 0; i--) // První změna od postfixu (bere to odzadu)
            {
                try
                {
                    float cislo = float.Parse(list[i], CultureInfo.InvariantCulture.NumberFormat);
                    zasobnik.Push(cislo); // Je to číslo
                }
                catch // Je to operátor
                {
                    char oper = Convert.ToChar(list[i]);
                    float b;
                    float a;
                    try
                    {
                        a = zasobnik.Pop();
                        b = zasobnik.Pop(); // musí být obráceně než u postfixu, když už jsem to jednou otočil
                    }
                    catch
                    {
                        Console.WriteLine("Neplatný vstup!"); // Nedostatek prvků v zásobníku
                        return null;
                    }
                    switch (oper)
                    {
                        case '+':
                            zasobnik.Push(a + b);
                            break;
                        case '-':
                            zasobnik.Push(a - b);
                            break;
                        case '*':
                            zasobnik.Push(a * b);
                            break;
                        case '/':
                            if (b == 0)
                            {
                                Console.WriteLine("Neděl nulou!!!!!!!!");
                                return null;
                            }
                            else
                            {
                                zasobnik.Push(a / b);
                            }
                            break;
                        default:
                            Console.WriteLine("Neznámý znak:", oper);
                            return null;
                    }
                }
            }
            if (zasobnik.Count == 1)
            {
                return zasobnik.Pop();
            }
            else
            {
                Console.WriteLine("Neplatný vstup!"); // Moc prvků v zásobníku
                return null;
            }
        }

        public string ToInfix(string[] list)
        {
            string chyba = "Chyba při převádění do infixu - vadný vstup !!!";
            try
            {
                // testuju jestli je první prvek číslo -> Jedná se o prefix či postfix
                float test = float.Parse(list[0], CultureInfo.InvariantCulture.NumberFormat);
                // POSTFIX
                Stack<string> stack = new Stack<string>();
                string[] operators = new string[4] { "+", "-", "*", "/" };
                try
                {
                    foreach (string prvek in list)
                    {
                        if (operators.Contains(prvek))
                        {
                            string b = stack.Pop();
                            string a = stack.Pop();
                            stack.Push($"({a} {prvek} {b})");
                        }
                        else
                        {
                            // testuju jestli je to platné číslo
                            test = float.Parse(prvek, CultureInfo.InvariantCulture.NumberFormat);
                            stack.Push(prvek);
                        }
                    }
                }
                catch
                {
                    return chyba;
                }
                if (stack.Count == 1)
                {
                    return stack.Pop();
                }
                else
                {
                    return chyba;
                }
            }
            catch
            {
                // PREFIX
                Stack<string> stack = new Stack<string>();
                string[] operators = new string[4] { "+", "-", "*", "/" };
                try
                {
                    for (int i = list.Length - 1; i >= 0; i--)
                    {
                        string prvek = list[i];
                        if (operators.Contains(prvek))
                        {
                            string a = stack.Pop();
                            string b = stack.Pop(); // musí být obráceně než u postfixu, když už jsem to jednou otočil
                            stack.Push($"({a} {prvek} {b})");
                        }
                        else
                        {
                            // testuju jestli je to platné číslo
                            float test = float.Parse(prvek, CultureInfo.InvariantCulture.NumberFormat);
                            stack.Push(prvek);
                        }
                    }
                }
                catch
                {
                    return chyba;
                }
                if (stack.Count == 1)
                {
                    return stack.Pop();
                }
                else
                {
                    return chyba;
                }
            }
        }
    }
}
