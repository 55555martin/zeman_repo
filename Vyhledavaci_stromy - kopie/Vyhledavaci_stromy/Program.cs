﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vyhledavaci_stromy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node<string> node1 = new Node<string>(1, "x");
            Node<string> node2 = new Node<string>(2, "y");
            Node<string> node3 = new Node<string>(5, "z");
            VyhledavaciStrom<string> strom = new VyhledavaciStrom<string>();
            strom.koren = node2;
            node2.left = node1;
            node2.right = node3;
            //Console.WriteLine(strom.Show());
            Console.WriteLine(strom.Find(1));
        }
    }
    
    class Node<T>
    {
        public T value { get; set; }
        public int key { get; set; }
        public Node<T> left { get; set; }
        public Node<T> right { get; set; }
        public Node(int k, T v)
        {
            key = k;
            value = v;
        }

        
    }
    class VyhledavaciStrom<T>
    {
        public Node<T> koren { get; set; }

        public string Show()
        {
            string vysledek = "";
            void show_(Node<T> node)
            {
                if (node == null)
                {
                    return;
                }
                else
                {
                    show_(node.left);
                    vysledek += node.value.ToString() + " ";
                    show_(node.right);
                }
            }
            show_(koren);
            return vysledek;
        }

        public void Print()
        {
            double l = numbersLength(Max().key); // kolika ciferné je největší key
            List<List<Node<T>>> vrstvy = new List<List<Node<T>>>();
        }

        public Node<T> Max()
        {
            Node<T> node = koren;
            while (node != null)
            {
                if (node.right == null)
                {
                    return node;
                }
                else
                {
                    node = node.right;
                }
            }
            return null;
        }

        public T Find(int key)
        {
            Node<T> find_(Node<T> node, int key_)
            {
                if (node == null)
                    return null;
                if (node.key == key_)
                    return node;
                if (key_ < node.key)
                    return find_(node.left, key_);
                else
                    return find_(node.right, key_);
            }
            Node<T> vysledek = find_(koren, key);
            if (vysledek == null)
                return default(T);
            return vysledek.value;
        }
        public int Hloubka(Node<T> koren_)
        {
            int hloubka_(Node<T> k)
            {
                if (k != null)
                    return 1 + Math.Max(hloubka_(k.left), hloubka_(k.right));
                else
                    return 0;
            }
            int hloubka = hloubka_(koren_);
            return hloubka;
        }

        public List<List<Node<T>>> rozdelNaVrstvy() // vrátí list s jednotlivými vrstvami stromu
        {
            List<List<Node<T>>> vrstvy = new List<List<Node<T>>>();
            int hloubka = Hloubka(koren);
            for (int i = 0; i < hloubka; i++)
            {
                vrstvy.Add(new List<Node<T>>());
            }
            void rekurze(Node<T> node, int vrstva)
            {
                if (node != null)
                {
                    vrstvy[vrstva].Add(node);
                    rekurze(node.left, vrstva+1);
                    rekurze(node.right, vrstva+1);
                }
            }
            rekurze(koren, 0);
            return vrstvy;
        }
        public double numbersLength(int n) // Zjistí kolika ciferné je číslo
        {
            return Math.Floor(Math.Log10(n) + 1);
        }
    }
}
