using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Задание_1;

namespace Задание_1
{
    public class Person
    {
        protected string Name;
        protected string Surname;
        protected string Gender;
        protected int Age;
        public Person(string name_imput, string surname_input, string Gender_input, int age_input){
            Name = name_imput;
            Surname = surname_input;
            Gender = Gender_input;
            Age = age_input;
            }
        public void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class employee : Person
    {
        public employee(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

        public class worker : Person
        {
        public worker(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }
        public class ingener : Person
        {
        public ingener(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        Person[] p = new Person[] { new employee("Tom", "Helt", "M", 20), new ingener("Adam", "Lopepr", "M", 23), new worker("Emver", "Hoeten", "W", 30) };
        foreach (var v in p)
        {
            v.Print();
        }
        Console.Read();
        }
    }
}
