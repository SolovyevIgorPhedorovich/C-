using System;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Project3
{
    class LPoint
    {
        public int key;
        public Person value;
        static Random rnd = new Random();

        public LPoint(Person p)
        {
            value = p;
            key = GetHashCode();
        }

        public override string ToString()
        {
            return key + ":" + value.surname.ToString();
        }

        public override int GetHashCode()
        {
            int code = 0;
            foreach (char c in value.name+value.surname)
                code += (int)c;
            return code;
        }
    }
    class HTable
    {
        public LPoint[] table;
        public int size;
        
        public HTable(int size_input = 10)
        {
            size = size_input;
            table = new LPoint[size];
        }

        public bool Add(Person s, int i)
        {
            LPoint point = new LPoint(s);
            if (s == null) return false;
            int index = Math.Abs(point.GetHashCode()+i)%size;
            if (table[index] == null) table[index] = point;
            else
            {
                LPoint cur = table[index];
                if (string.Compare (cur.value.ToString(), point.value.ToString()) == 0) return false;
                Add(s, ++i);
            }
            return true;
        }
        public void Print()
        {
            if (table == null) {Console.WriteLine("Table empty!"); return;}
            for (int i = 0; i < size; i++)
            {
                if (table[i] == null) Console.WriteLine(i + ":");
                else 
                {
                    Console.Write(i+":");
                    table[i].value.Print();
                    LPoint p = table[i];
                }
            }
        }
        
        public bool FindPoint(Person x, int i)
        {
            LPoint lp = new LPoint(x);
            int key = lp.key;
            int code = Math.Abs(lp.GetHashCode() + i)%size;
            if (string.Compare(table[code].value.ToString(), x.ToString()) == 0) return true;
            if (table[code] != null && (string.Compare(table[code].key.ToString(), lp.key.ToString())==0)) return true;
            else
            {
                if (i < 10)
                    FindPoint(x, ++i);
            }
            return false;
        }

        public Person DelPoint(Person x, int i)
        {
            LPoint lp = new LPoint(x);
            int code = Math.Abs(lp.GetHashCode() + i)%size;
            lp = table[code];
            if (table[code]==null) return null;
            if (table[code] != null && String.Compare(table[code].key.ToString(), lp.key.ToString()) == 0)
            {
                lp = table[code];
                table[code] = null;
                return lp.value;
            }
            else
            {
                DelPoint(x, ++i);
            }
            return null;
        }
    }
    public class Person:IComparer
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

        int IComparer.Compare(object? x, object? y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }
        public override string ToString()
        {
            return name + " " + surname + " " + gender + " " + age.ToString();
        }
        virtual public void Print()
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
        static void Main(string[] argv)
        {
            Console.Write("Col element: ");
            int size = Convert.ToInt32(Console.ReadLine());
            HTable ht = new HTable();
            for (int i = 0; i < size; i++)
            {
                ht.Add(GenericPerson(), 0);
            }
            ht.Print();
            do 
            {
                Console.Write("Введите имя:");
                string name = Console.ReadLine();
                Console.Write("Введите фамилию:");
                string surname = Console.ReadLine();
                Console.Write("Введите пол:");
                string gender= Console.ReadLine();
                Console.Write("Введите возраст:");
                int age = Convert.ToInt32(Console.ReadLine());
                if (name == "" && surname == "" && gender == "") break;
                Person FindPerson = new Person(name, surname, gender, age);
                if (ht.FindPoint(FindPerson, 0))
                {
                    Console.WriteLine("Строка найден");
                    Console.Write("Удалить данные из таблицы.[y/n]:");
                    string x = Console.ReadLine();
                    if (x == "y")
                    {
                        ht.DelPoint(FindPerson,0);
                    }
                }
                else Console.WriteLine("Строка не найдена.");
            }
            while(true);   
            ht.Print();
        }
    }
}