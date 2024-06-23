using System;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Project2
{
    class Tree
    {
        public Person data;
        public Tree left, right;
        public int count;

        public Tree()
        {
            data = null;
            left = null;
            right = null;
        }

        public Tree(Person d)
        {
            data = d;
            left = null;
            right = null;
        }
        
        public void ShowTree(Tree p, int c)
        {
            if (p!=null)
            {
                ShowTree(p.left, c+3);
                for (int i = 0; i < c; i++) Console.Write(" ");
                p.data.Print();
                ShowTree(p.right, c+3);
            }
        }

        private Person GenericPerson()
        {
            string[] male_name_list = new string[10] {"Liam","Noah","Oliver","Elijah","Jamse","William","Benjamin","Lucas","Henry","Theodore"};
            string[] femaly_name_list = new string[10] {"Olivia","Emma","Charlotte","Amelia","Ava","Sophia","Isabella","Mia","Evelyn","Harper"};
            string[] surname_list = new string[10] {"Smith","Johnson","Williams","Brown","Jones","Garcia","Miller","Davis","Rodriguez","Martinez"};
            Random rnd = new Random();
            int info = rnd.Next(1,4);
            switch (info)
            {
                case 1:
                    info = rnd.Next(1,3);
                    if (info == 1)
                    {
                        return new Employee(male_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "M", rnd.Next(20,50));
                    }
                    else if (info == 2)
                    {
                        return new Employee(femaly_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "W", rnd.Next(20,50));
                    }
                    break;
                case 2:
                    info = rnd.Next(1,3);
                    if (info == 1)
                    {
                        return new Worker(male_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "M", rnd.Next(20,50));
                    }
                    else if (info == 2)
                    {
                        return new Worker(femaly_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "W", rnd.Next(20,50));
                    }
                    break;
                case 3:
                    info = rnd.Next(1,3);
                    if (info == 1)
                    {
                        return new Enginer(male_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "M", rnd.Next(20,50));
                    }
                    else if (info == 2)
                    {
                        return new Enginer(femaly_name_list[rnd.Next(0,10)],surname_list[rnd.Next(0,10)], "W", rnd.Next(20,50));
                    }
                    break;
                default:
                    return null;
            }
            return null;
        }

        public Tree IdealTree(Tree t, int size)
        {
            Tree r;
            count = size;
            int nl, nr;
            if (size == 0) {t=null; return t;}
            nl = size/2;
            nr = size-nl-1;
            r = new Tree(GenericPerson());
            r.left = IdealTree(r.left, nl);
            r.right = IdealTree(r.right, nr);
            return r;
        }
        public void del()
        {
            data = null;
            left = null;
            right = null;
        }
        private Tree RotateLeft(ref Tree t)
        {
            Tree p = t.right;
            t.right = p.left;
            p.left = t;
            return p;
        }
        private Tree RotateRight(ref Tree t)
        {
            Tree p = t.left;
            t.left = p.right;
            p.right = t;
            return p;
        }
    }
    public class Person
    {
        public string name;
        public string surname;
        public string gender;
        public int age;

        public Person(string input_name, string input_surname, string input_gender, int input_age)
        {
            name = input_name;
            surname = input_surname;
            gender = input_gender;
            age = input_age;
        }

        public Person()
        {
            name = null;
            surname = null;
            gender = null;
            age = 0;
        }

        public void Print()
        {
            Console.WriteLine("Имя:{0}  Фамилия:{1}  Пол:{2} Возраст:{3}", name, surname, gender, age);
        }
    }
    public class Employee:Person
    {
        public Employee(string input_name, string input_surname, string input_gender, int input_age):base(input_name, input_surname, input_gender, input_age){}
    }
    public class Worker:Person
    {
        public Worker(string input_name, string input_surname, string input_gender, int input_age):base(input_name, input_surname, input_gender, input_age){}
    }
    public class Enginer:Person
    {
        public Enginer(string input_name, string input_surname, string input_gender, int input_age):base(input_name, input_surname, input_gender, input_age){}
    }
    internal class Program
    {
        static Person MaxAge(Tree t, Person x)
        {
            if (t != null)
            {
                if (x.age < t.data.age) x = t.data;
                x = MaxAge(t.left, x);
                x = MaxAge(t.right, x);
            }
            return x;
        }
        static Tree Add(Tree root, Person x)
        {
            Tree r = null;
            if (root == null)
            {
                return r = new Tree(x);
            }
            while(root!=null)
            {
                r = root;
                if (x.age < root.data.age) root = root.left;
                else root = root.right;
            }
            Tree NewTree = new Tree(x);
            if (x.age < r.data.age) r.left = NewTree;
            else r.right = NewTree;
            return NewTree;
        }
        static Tree ConvertToFindTree (Tree t, Tree root)
        {
            List<Person> PersonList = new List<Person>();
            if(t != null)
            {
                root = Add(root, t.data);
                ConvertToFindTree(t.left, root);
                ConvertToFindTree(t.right, root);
            }
            return root;
        }
        
        static void Main(string[] argv)
        {
            Console.Write("Количесвот элементов:");
            int size = Convert.ToInt32(Console.ReadLine());
            if (size > 0){
                Tree test = new Tree();
                test = test.IdealTree(test, size);
                test.ShowTree(test, size);
                MaxAge(test, test.data).Print();
                test = ConvertToFindTree(test, test=null);
                test.ShowTree(test, size);
                test.del();
            }
            else if (size == 0) Console.WriteLine("Количевстов элементов 0.");
            else Console.WriteLine("Количество элементов не может быть меньше 0.");
        }
    }
}