using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Node uzl = new Node(12);
        }
    }
    class Node
    {
        public Node(int value) // konstruktor
        {
            Value = value;
        }
        public int Value { get; }
        public Node Next { get; set; }
    }
    class LinkedList
    {
        public Node Head { get; set; }
        public void Add(int value) // Přidá prvek na začátek seznamu
        {
            if (Head==null)
            {
                Head = new Node(value);
            }
            else
            {
                Node newNode = new Node(value);
                newNode.Next = Head;
                Head = newNode;
            }
        } // složitost O(1)

        public bool Find(int value) // Vrátí true, pokud se v seznamu hledaný prvek nachází, jinak false
        {
            Node prvek = Head;
            while (prvek!=null)
            {
                if(prvek.Value == value)
                {
                    return true;
                }
                prvek = prvek.Next;
            }
            return false;
        } // složitost O(n)

        public int Min() // Najde minimum v seznamu, pokud je seznam prázdný vrátí null
        {
            int minimum = Head.Value;
            Node prvek = Head.Next;
            while (prvek != null)
            {
                if(prvek.Value < minimum)
                {
                    minimum = prvek.Value;
                }
                prvek = prvek.Next;
            }
            return minimum;
        } // složitost O(n)
    }
}
