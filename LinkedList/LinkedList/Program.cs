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
            LinkedList a = new LinkedList();
            a.Add(5);
            a.Add(4);
            a.Add(3);
            a.Add(12);
            a.Add(5);
            a.Add(6);
            a.PrintLinkedList();
            a.SortLinkedList();
            a.PrintLinkedList();
            Console.ReadLine();
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
        public LinkedList() // konstruktor
        {
            Length = 0;
        }
        public Node Head { get; set; }
        public int Length { get; set; }
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
            Length++;
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

        public void PrintLinkedList() // Vypíše list
        {
            Node prvek = Head;
            while (prvek != null)
            {
                Console.Write(prvek.Value);
                Console.Write("; ");
                prvek = prvek.Next;
            }
            Console.WriteLine();
        } // složitost O(n)

        public void SortLinkedList() // seřadí list
        {
            LinkedList novylist = new LinkedList();
            novylist.Head = new Node(Head.Value);
            if (Head != null)
            {
                Node prvek = Head.Next;
                while (prvek != null)
                {
                    novylist.InsertSorted(prvek.Value);
                    prvek = prvek.Next;
                }
            }
            Head = novylist.Head;
        } // složitost O(n^2)

        public void InsertSorted(int value) // přidá prvek do seřazeného listu na správné místo
        {
            if (Head == null)
                Head = new Node(value);
            else
            {
                Node prvek = Head;
                Node predchoziprvek = null;
                while (true)
                {
                    if (prvek.Value < value)
                        if (prvek.Next != null)
                        {
                            predchoziprvek = prvek;
                            prvek = prvek.Next;
                        }
                        else
                            break;
                    else
                        break;
                }
                Node newNode = new Node(value);
                newNode.Next = predchoziprvek.Next;
                predchoziprvek.Next = newNode;
            }
        } // složitost O(n)
    }
}
