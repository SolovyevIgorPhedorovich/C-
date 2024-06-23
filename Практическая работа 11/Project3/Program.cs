using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class TestCollections
    {
        private Queue<Person> Collections1 = new Queue<Person>();
        private Queue<string> Collections2 = new Queue<string>();
        public SortedDictionary<Person, Worker> Collections3 = new SortedDictionary<Person, Worker>();
        public SortedDictionary<string, Worker> Collections4 = new SortedDictionary<string, Worker>();

        private static Worker MethodGenerate()
        {
            Console.Write("Имя:");
            string name = Console.ReadLine();
            Console.Write("Фамилия:");
            string surname = Console.ReadLine();
            Console.Write("Пол:");
            string gender = Console.ReadLine();
            Console.Write("Возраст:");
            int age = int.Parse(Console.ReadLine());
            return new Worker(name, surname, gender, age);
        }

        public TestCollections(int quantity){
            for (int i = 0; i < quantity; i++){
                Worker human = MethodGenerate();
                Collections1.Enqueue(human.BasePerson);
                Collections2.Enqueue(human.ToString());
                Collections3.Add(human.BasePerson, human);
                Collections4.Add(human.ToString(), human);
            }
        }
    }
    public class Person
    {
        protected string name;
        protected string surname;
        protected string gender;
        protected int age;

        public Person(string name_input, string surname_input, string gender_input, int age_input){
            name = name_input;
            surname = surname_input;
            gender = gender_input;
            age = age_input;
        }
        virtual public void Print(){
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    } 
    public class Employee:Person
    {
        public Employee(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
        public Person BasePerson
        {
            get{
                return new Person(name,surname, gender,age);
            }
        }
        override public void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }
    public class Worker : Person
    {
        public Worker(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public Person BasePerson
        {
            get{
                return new Person(name,surname, gender,age);
            }
        }
        override public void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }
    public class Enginer : Person
    {
        public Enginer(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        public Person BasePerson
        {
            get{
                return new Person(name,surname, gender,age);
            }
        }
        override public void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }

    internal class Program
    {
        static void Main(string[] args){
            Console.Write("Колличесвто элементов в коллекии:");
            int x = Convert.ToInt32(Console.ReadLine());
            TestCollections test = new TestCollections(x);
            Console.WriteLine(test);
        }
    }
}