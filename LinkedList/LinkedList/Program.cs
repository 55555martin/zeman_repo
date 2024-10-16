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
        public void Add(int value)
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
        }
        public bool Find(int value)
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
        }
    }
}
