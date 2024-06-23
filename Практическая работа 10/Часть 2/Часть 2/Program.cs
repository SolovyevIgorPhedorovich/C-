using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Часть_2
{
    public class Person
    {
        public string Name;
        public string Surname;
        public string Gender;
        public int Age;
        public Person(string name_imput, string surname_input, string Gender_input, int age_input)
        {
            Name = name_imput;
            Surname = surname_input;
            Gender = Gender_input;
            Age = age_input;
        }
        public virtual void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class employee : Person
    {
        public employee(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public override void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class worker : Person
    {
        public int Staje;
        public worker(string name_imput, string surname_input, string Gender_input, int age_input, int staje_input) : base(name_imput, surname_input, Gender_input, age_input) 
        {
            Staje = staje_input;
        }
        public override void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}\t Стаж: {4}", Name, Surname, Gender, Age, Staje);
        }
    }
    public class ingener : Person
    {
        public ingener(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public override void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    internal class Program
    {
        static void FindMenu(Person[]p)
        {
            int m = 0;
            while (m != 4)
            {
                Console.WriteLine(@"1. Количество рабочих со стажем не менее заданного.
2. Имена служащих мужского пола.
3. Коллтчество инженеров старше заданного возраста.");
                m = Convert.ToInt32(Console.ReadLine());
                if (m == 1) FindWorker(p);
                if (m == 2) FindEmploee(p);
                if (m == 3) FindIngener(p);
            }
        }

        static void FindWorker(Person[]p)
        {
            int n = 0;
            Console.Write("Стаж: ");
            int s = Convert.ToInt32(Console.ReadLine());
            foreach(var d in p)
            {
                if (d is worker e)
                {
                    if (e.Staje >= s) n++;
                }
            }
            Console.WriteLine(n);
        }

        static void FindEmploee(Person[]p)
        {
            foreach (var d in p)
            {
                if (d is employee g)
                {
                    if (g.Gender == "M") Console.WriteLine(g.Name);
                }
            }
        }

        static void FindIngener(Person[] p)
        {
            int n = 0, x;
            Console.Write("Возраст:");
            x = Convert.ToInt32(Console.ReadLine());
            foreach (var d in p)
            {
                if (d is ingener i)
                {
                    if (x < i.Age) n++;
                }
            }
            Console.WriteLine(n);
        }


        static void Main(string[] args)
        {
            Person[] p = new Person[] { new employee("Tom", "Helt", "M", 20), new employee("Amber", "Hoeten", "W", 30), new employee("Adam", "Helt", "M", 25), new ingener("Adam", "Lopepr", "M", 23), new ingener("Tom", "Holgen", "M", 30), new ingener("Deiv", "Homan", "M", 25), new worker("Emver", "Hoeten", "W", 30, 6), new worker("Don", "Loppe", "M", 33, 3), new worker("Engil", "Zolte", "M", 35, 6) };
            foreach (var v in p)
            {
                v.Print();
            }
            FindMenu(p);
        }
    }
}
