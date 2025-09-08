using System;
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
            Console.WriteLine(strom.Show());
            Console.WriteLine(strom.Find(1).value);
            Console.ReadLine();
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

        public void Insert(int key, T value)
        {
            Node<T> node = koren;
            Node<T> parent = null;
            while (node != null)
            {
                if (key < node.key)
                {
                    parent = node;
                    node = node.left;
                }
                else
                {
                    parent = node;
                    node = node.right;
                }
            }
            if (parent == null)
                koren = new Node<T>(key, value);
            else if (key < parent.key)
                parent.left = new Node<T>(key, value);
            else
                parent.right = new Node<T>(key, value);
        }

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
                    vysledek += node.key.ToString() + " ";
                    show_(node.right);
                }
            }
            show_(koren);
            return vysledek;
        }

        public Node<T> Find(int key)
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
            return vysledek;
        }

        public int? FindMin()
        {
            Node<T> node = koren;
            while (node != null)
            {
                if (node.left == null)
                    return node.key;
                else
                    node = node.left;
            }
            return null;
        }

        public void Remove(int key)
        {
            Node<T> remove_(Node<T> root)
            {
                if (root == null)
                    return null;
                Node<T> newRoot = null;
                if (key < root.key)
                    newRoot = remove_(root.left);
                if (key > root.key)
                    newRoot = remove_(root.right);
                if (key == root.key)
                {
                    if (root.left == null && root.right == null)
                        newRoot = null;
                    else if (root.left == null)
                        newRoot = root.right;
                    else if (root.right == null)
                        newRoot = root.left; // zde pokračovat
                }
            }
            koren = remove_(koren);
        }
    }
    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string ClassName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }
}
