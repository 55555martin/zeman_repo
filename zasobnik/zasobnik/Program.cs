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
            Console.WriteLine(ControlBrackets(Console.ReadLine()));
            Console.ReadLine();
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
