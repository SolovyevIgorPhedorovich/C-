using System;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Project1
{
    class DoublyLinkedList
    {
        public Person data;
        public DoublyLinkedList next;
        public DoublyLinkedList prev;

        public DoublyLinkedList()
        {
            data = null;
            next = null;
            prev = null;
        }

        public DoublyLinkedList(Person h)
        {
            data = h;
            next = null;
            prev = null;
        }
        public int Count(DoublyLinkedList list)
        {
            int x = 0;
            while (list != null)
            {
                x++;
                list = list.next;
            }
            return x;
        }
        public void Del(ref DoublyLinkedList beg)
        {
            if (beg == null)
            {
                Console.WriteLine("Error! The List is empty");
                beg = null;
            }
            else{
                beg = beg.next;
            }
        }
        public Person ToPerson()
        {
            return data;
        }
    }
    public class Person
    {
        protected string name;
        protected string surname;
        protected string gender;
        protected int age;

        public Person(string name_input, string surname_input, string gender_input, int age_input)
        {
            name = name_input;
            surname = surname_input;
            gender = gender_input;
            age = age_input;
        }

        virtual public void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }
    public class Employee : Person
    {
        public Employee(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }
    public class Worker : Person{
            public Worker(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
            override public void Print()
            {
                Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
            }
        }
    public class Enginer : Person
    {
        public Enginer(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }
    internal class Program
    {
        static Person GenericPerson()
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
        static DoublyLinkedList MakeList(int size)
        {
            DoublyLinkedList beg = new DoublyLinkedList(GenericPerson());
            for (int i = 1; i < size; i++)
            {
                DoublyLinkedList p = new DoublyLinkedList(GenericPerson());
                p.next = beg;
                beg.prev = p;
                beg = p;
            }
            return beg;
        }
        static void PrintList(DoublyLinkedList list)
        {
            if (list.prev != null && list.next != null){
                DoublyLinkedList nextList = list.next;
                DoublyLinkedList prevList = list.prev;
                list.ToPerson().Print();
                while (nextList != null || prevList != null)
                {
                    if(nextList != null)
                    {
                        nextList.ToPerson().Print();
                        nextList = nextList.next;
                    }
                    if(prevList != null)
                    {
                        prevList.ToPerson().Print();
                        prevList = prevList.prev;
                    }
                }
            }
            else if (list.prev == null)
            {
                for (int i = 0, col = list.Count(list); i < col; i++)
                {
                    list.ToPerson().Print();
                    if (i != col - 1){
                        list = list.next;
                    }
                }
            }
            else if (list.next == null)
            {
                for (int i = 0, col = list.Count(list); i < col; i++)
                {
                    list.ToPerson().Print();
                    if (i != col - 1){
                        list = list.prev;
                    }
                }
            }
        }
        static DoublyLinkedList AddList(DoublyLinkedList list)
        {
            list = list.next;
            DoublyLinkedList addFirst = new DoublyLinkedList(list.data);
            if (list.Count(list) != 1){
                list = list.next;
                for (int i = 2, col = list.Count(list) + 2; i < col; i++ )
                {
                    if (i % 2 != 0)
                    {
                        DoublyLinkedList addNext = new DoublyLinkedList(list.data);
                        addNext.next = addFirst;
                        addFirst.prev = addNext;
                        addFirst = addNext;
                    }
                    if (i == col - 1){
                        list.next = addFirst;
                        break;
                    }
                    list = list.next;
                }
            }
            else
                list.next = addFirst;
            return list;
        }
        static void Main(string[] args)
        {
            DoublyLinkedList listPerson = new DoublyLinkedList();
            Console.Write("Количество элементов в списке:");
            int c = Convert.ToInt32(Console.ReadLine());
            listPerson = MakeList(c);
            PrintList(listPerson);
            listPerson = AddList(listPerson);
            Console.WriteLine("Список после добавления:");
            PrintList(listPerson);
            listPerson.Del(ref listPerson);
        }
    }
}