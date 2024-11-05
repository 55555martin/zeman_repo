using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            a.Add(1);
            a.Add(5);
            a.Add(6);
            LinkedList b = new LinkedList();
            b.Add(9);
            b.Add(9);
            b.Add(0);
            b.Add(1);
            a.PrintLinkedList();
            b.PrintLinkedList();
            Secti(a,b,10).PrintLinkedList();
            a.PrintLinkedList();
            Console.ReadLine();
        }

        static LinkedList Secti(LinkedList listA, LinkedList listB, int ciselnaSoustava) // Sečte dvě čísla interpretovaná spojovým seznamem
        // Head je číslice na místě jednotek (takže číslo je jakoby naopak)
        {
            LinkedList listC = new LinkedList();
            Node a = listA.Head;
            Node b = listB.Head;
            listC.Head = new Node(0);
            Node c = listC.Head;
            int prebytek = 0;
            while (a != null || b != null)
            {
                if (a == null)
                {
                    c.Next = new Node(b.Value+prebytek);
                    prebytek = 0;
                    b = b.Next;
                }
                else if (b == null)
                {
                    c.Next = new Node(a.Value+prebytek);
                    prebytek = 0;
                    a = a.Next;
                }
                else
                {
                    c.Next = new Node( (a.Value+b.Value) % ciselnaSoustava + prebytek);
                    prebytek = (a.Value + b.Value) / ciselnaSoustava; // mělo by to být 0 či 1, pokud prvky v seznamech korespondují se soustavou
                    a = a.Next; b = b.Next;
                }
                c = c.Next;
            }
            listC.Head = listC.Head.Next;
            return listC;
        } // složitost O(n)
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

        public int? Min() // Najde minimum v seznamu, pokud je seznam prázdný vrátí null
        {
            if (Head == null)
                return null;
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
            if (Head != null)
            {
                novylist.Head = new Node(Head.Value);
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
                    {
                        predchoziprvek = prvek;
                        if (prvek.Next != null)
                            prvek = prvek.Next;
                        else
                            break;
                    }
                    else
                        break;
                }
                Node newNode = new Node(value);
                if (predchoziprvek == null)
                {
                    Head = newNode;
                    newNode.Next = prvek;
                }
                else
                {
                    newNode.Next = predchoziprvek.Next;
                    predchoziprvek.Next = newNode;
                }
            }
        } // složitost O(n)

        public void DestruktivniPrunik(LinkedList list)
        {
            SortLinkedList();
            DestroyDuplicites();
            list.SortLinkedList();
            list.DestroyDuplicites();
            Node prvek = list.Head;
            while (prvek != null)
            {
                InsertSorted(prvek.Value);
                prvek = prvek.Next;
            }
            // ty prvky, které se zde nacházejí 2x, jsou průnikem
            Node predchoziprvek = null;
            prvek = Head;
            while (prvek != null)
            {
                if (prvek.Next == null || prvek.Value != prvek.Next.Value)
                {
                    if (predchoziprvek == null)
                        Head = prvek.Next;
                    else
                        predchoziprvek.Next = prvek.Next;
                }
                else if (prvek.Value == prvek.Next.Value)
                {
                    predchoziprvek = prvek;
                    prvek.Next = prvek.Next.Next;
                }
                prvek = prvek.Next;
            }
        } // složitost O(n^2)

        public void DestruktivniSjednoceni(LinkedList list)
        {
            SortLinkedList();
            Node prvek = list.Head;
            while (prvek != null)
            {
                InsertSorted(prvek.Value);
                prvek = prvek.Next;
            }
            DestroyDuplicites();
        } // složitost O(n^2)

        public void DestroyDuplicites() // Odstraní duplikáty v SEŘAZENÉM listu
        {
            Node prvek = Head;
            while (prvek != null)
            {
                if (prvek.Next == null)
                    prvek = prvek.Next;
                else
                {
                    if (prvek.Value == prvek.Next.Value)
                        prvek.Next = prvek.Next.Next;
                    else
                        prvek = prvek.Next;
                }
            }
        } // složitost O(n) (avšak musí být seřazený)
    }
}
