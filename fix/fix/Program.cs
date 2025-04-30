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
            //Fix fix = new Fix();
            //string[] vstup = fix.NactiVstup();
            //float? vysledek = null;
            //if (vstup.Length > 0)
            //{
            //    try
            //    {
            //        // testuju jestli je první prvek číslo -> Jedná se o prefix či postfix
            //        float test = float.Parse(vstup[0], CultureInfo.InvariantCulture.NumberFormat);
            //        // POSTFIX
            //        vysledek = fix.Postfix(vstup);
            //    }
            //    catch
            //    {
            //        // PREFIX
            //        vysledek = fix.Prefix(vstup);
            //    }
            //}
            //if (vysledek != null)
            //{
            //    Console.WriteLine($"{fix.ToInfix(vstup)} = {vysledek}");
            //}
            //Console.ReadLine();
            FixTree strom = new FixTree(Console.ReadLine());
            Console.WriteLine(strom.Vypis(TypFixu.Postfix));
            Console.WriteLine(strom.Vypis(TypFixu.Prefix));
            Console.WriteLine(strom.Vypis(TypFixu.Infix));
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

    class Node
    {
        public Node left { get; set; }
        public Node right { get; set; }
        public Node parent { get; set; }
        public string valueOper { get; set; }
        public float valueCislo { get; set; }
        public bool isCislo { get; set; }
        public Node(string value) // konstruktor
        {
            try // Číslo
            {
                valueCislo = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                isCislo = true;
            }
            catch // Operátor
            {
                valueOper = value;
                isCislo = false;
            }
        }
    }
    public enum TypFixu
    {
        Postfix,
        Prefix,
        Infix
    }

    class FixTree
    {
        Node root {  get; }
        public FixTree(string postfix)
        {
            Stack<Node> stack = new Stack<Node>();
            string[] list = postfix.Split(' ');
            try
            {
                for (int i = 0; i < list.Length; i++)
                {
                    Node node = new Node(list[i]);
                    if (node.isCislo)
                    {
                        stack.Push(node);
                    }
                    else
                    {
                        Node b = stack.Pop();
                        Node a = stack.Pop();
                        node.left = a;
                        node.right = b;
                        a.parent = node;
                        b.parent = node;
                        stack.Push(node);
                    }
                }
                if (stack.Count == 1)
                {
                    root = stack.Pop();
                }
                else
                {
                    root = null; // Je tam toho asi moc - vadný vstup
                }
            }
            catch
            {
                root = null; // Nějak je vstup vadný
            }
            
        }
        public string Vypis(TypFixu typFixu)
        {
            if (root == null)
            {
                return null;
            }
            string Projdi(Node node)
            { 
                if (node.isCislo)
                {
                    return node.valueCislo.ToString();
                }
                else
                {
                    string a = Projdi(node.left);
                    string b = Projdi(node.right);
                    if (typFixu is TypFixu.Postfix)
                    {
                        return $"{a} {b} {node.valueOper}";
                    }
                    else if (typFixu is TypFixu.Prefix)
                    {
                        return $"{node.valueOper} {a} {b}";
                    }
                    else // Infix
                    {
                        return $"({a} {node.valueOper} {b})";
                    }
                }
            }
            return Projdi(root);
        }
    }
}
