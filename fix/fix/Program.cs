using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace fix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fix fix = new Fix();
            Console.WriteLine(fix.Postfix(fix.NactiVstup()));
        }
    }
    class Fix
    {
        public void Main2()
        {
            Console.WriteLine(Postfix(NactiVstup()));
        }

        public string[] NactiVstup()
        {
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
                    zasobnik.Push(cislo);
                }
                catch
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
                        Console.WriteLine("Neplatný vstup!");
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
                Console.WriteLine("Neplatný vstup!");
                return null;
            }
        }

        //public float? Prefix(string[] list)
        //{
        //    Stack<float> zasobnik = new Stack<float>();
        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        try
        //        {
        //            float cislo = Convert.ToSingle(list[i]);
        //        }
        //        catch
        //        {
        //            string oper = list[i];
        //            zasobnik.Push(oper);
        //        }
        //    }
        //}
    }
}
