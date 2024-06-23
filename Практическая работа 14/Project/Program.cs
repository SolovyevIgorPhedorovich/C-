using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
    class MyCollection<T> : ICloneable, IEnumerable<T>
    {
        public T value;
        public MyCollection<T> next;
        private int size;

        public MyCollection()
        {
            value = default(T);
            next = null;   
        }
        public MyCollection(int capacity)
        {
            value = default(T);
            next = null;
            size = capacity;
        }
        public MyCollection(MyCollection<T> c)
        {
            value = c.value;
            next = c.next;
            size = c.size;
        }
        public int Count()
        {
            int count = 0;
            MyCollection<T> c = this;
            while (c != null){
                count++;
                c = c.next;
            }
            return count;
        }
        public MyCollection<T> AddToBeg(T x)
        {
            if (this.size != 0 && this.size == Count())
            {
                Console.WriteLine("Переполнение коллекции.");
                return this;
            }
            MyCollection<T> c = new MyCollection<T>();
            if(value != null)
            {
                c.next = this;
                c.value = x;
            }
            else c.value = x;
            return c;
        }
        public MyCollection<T> AddToEnd(T x)
        {
            if (this.size != 0 && this.size == Count())
            {
                Console.WriteLine("Переполнение коллекции.");
                return this;
            }
            if(this.value == null)
            {
                MyCollection<T> first = this;
                first.value = x;
                return first;
            }
            MyCollection<T> c = new MyCollection<T>();
            MyCollection<T> beg = this;
            MyCollection<T> r = beg;
            c.value = x;
            while (r != null)
            { 
                if (r.next == null)
                {
                    r.next = c;
                    return beg;
                }
                r = r.next;
            }
            return beg;
        }
        public MyCollection<T> DelElement(T f)
        {
            MyCollection<T> a = new MyCollection<T>(1);
            a.value = f;
            MyCollection<T> beg = this;
            MyCollection<T> r = beg;
            while (r != null)
            {
                if (string.Compare(r.ToString(), a.ToString()) == 0)
                {
                    if (string.Compare(r.value.ToString(), a.value.ToString()) == 0) 
                    {
                        r.next = r.next.next; 
                        return beg;
                    }
                }
                r = r.next;
            }
            return beg;
        }
        public MyCollection<T> DelElementFromNumber(int d)
        {
            if (d <= Count())
            {
                MyCollection<T> beg = this;
                MyCollection<T> r = beg;
                for (int i = 0; i < Count(); i++)
                {
                    if (i == d-1)
                    {
                        r.next = r.next.next;
                        return beg;
                    }
                    r = r.next;
                }
            }
            else
            {
                Console.WriteLine("ВЫход за пределы коллекции.");
                return this;
            }
            return this;
        }
        public bool Find(T f)
        {
            MyCollection<T> a = new MyCollection<T>(1);
            a.value = f;
            MyCollection<T> x = this;
            while (x != null)
            {
                if (string.Compare(x.ToString(), a.ToString()) == 0)
                {
                    if (string.Compare(x.value.ToString(), x.value.ToString()) == 0) return true;
                }
                x = x.next;
            }
            return false;
        }
        public object Clone()
        {
            return new MyCollection<T>(this);
        }
        public object SurfaceCopy()
        {
            return this.MemberwiseClone();
        }
        public void Del()
        {
            this.next = null;
            this.value = default;
        }
        public void Print(MyCollection<T> c)
        {
            while (c != null)
            {
                string[] data = (c.value.ToString()).Split(' ');
                {Console.WriteLine("Имя:{0} Фамилия:{1} Пол:{2} Возраст:{3}", data[0],data[1],data[2],data[3]);}
                c = c.next;
            }
        }
        public void Print (int c)
        {
            MyCollection<T> x = this;
            while (c != null)
            {
                if (c == 0)
                {
                    string[] data = (x.value.ToString()).Split(' ');
                    {Console.WriteLine("Имя:{0} Фамилия:{1} Пол:{2} Возраст:{3}", data[0],data[1],data[2],data[3]);}
                }
                x = x.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }
    }
    class MyEnumerator<T> : IEnumerator<T>
    {
        MyCollection<T> beg;
        MyCollection<T> current;

        public MyEnumerator(MyCollection<T> c)
        {
            beg = c;
            current = c;
        }
        object IEnumerator.Current
        {
            get{ return Current;}
        }
        public T Current
        {
            get {return current.value;}
        }
        public void Reset()
        {
            current = this.beg;
        }
        public void Dispose(){}
        public bool MoveNext()
        {
            if (current.next == null)
            {
                Reset();
                return false;
            }
            else
            {
                current = current.next;
                return true;
            }
        }
    }
    static class Query
    {
        public static void Selection(this MyCollection<Person> collection,Func<Person, bool> predicate)
        {
            Console.WriteLine();
            var select = collection.Where(predicate).Select(p=>p);
            foreach(var x in select)
            {
                x.Print();
            }
        }
        public static void CountElement(this MyCollection<Person> collection, Func<Person, bool> predicate)
        {
            int count = collection.Where(predicate).Count();
            Console.WriteLine("Элементов в коллекции:" + count);
        }
        public static MyCollection<Person> Sorted(this MyCollection<Person> collection, Func<Person, String> predicate)
        {
            var sort = collection.OrderBy(predicate).Select(p=>p);
            MyCollection<Person> sorted = new MyCollection<Person>();
            foreach(Person x in sort)
                sorted.AddToEnd(x);
            return sorted;
        }
        public  static MyCollection<Person> SortedDescending(this MyCollection<Person> collection, Func<Person, String> predicate)
        {
            var sort = collection.OrderByDescending(predicate).Select(p=>p);
            MyCollection<Person> sorted = new MyCollection<Person>();
            foreach(Person x in sort)
                sorted.AddToEnd(x);
            return sorted;
        }
        public static void Grouping (this MyCollection<Person> collection, Func<Person,String> predocate)
        {
            var group = collection.GroupBy(predocate);
            foreach (var p in group)
                {
                    Console.WriteLine(p.Key);
                    foreach(var person in p)
                    {
                        Console.WriteLine(person.ToString());
                    }
                    Console.WriteLine();
                }
        }
    }
    class Person
    {
        public string? name;
        public string? surname;
        public string? gender;
        public int age;
        public string? position;

        public Person()
        {
            name = null;
            surname = null;
            gender = null;
            age = 0;
        }

        public Person(string name_input, string surname_input, string gender_input, int age_input)
        {
            name = name_input;
            surname = surname_input;
            gender = gender_input;
            age = age_input;
        }

        public Person GenericPerson()
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
                    break;
            }
            return this;
        }

        virtual public void Print()
        {
            Console.WriteLine("Имя:{0}      Фамилия:{1}     Пол:{2}     Возраст:{3}", name, surname, gender, age);
        }

        public override string ToString()
        {
            return name + " " + surname + " " + gender + " " + age.ToString();
        }
    }
    class Employee:Person
    {
        public Employee():base(){position = "Служащий";}
        public Employee(string name_input, string surname_input, string gender_input, int age_input):base(name_input, surname_input, gender_input, age_input){position = "Служащий";}

        public override void Print()
        {
            Console.WriteLine("{0}: Имя:{1}      Фамилия:{2}     Пол:{3}     Возраст:{4}", position, name, surname, gender, age);
        }
    }
    class Worker:Person
    {
        public Worker():base(){position = "Рабочий";}
        public Worker(string name_input, string surname_input, string gender_input, int age_input):base(name_input, surname_input, gender_input, age_input){position = "Рабочий";}

        public override void Print()
        {
            Console.WriteLine("{0}: Имя:{1}      Фамилия:{2}     Пол:{3}     Возраст:{4}", position, name, surname, gender, age);
        }
    }
    class Enginer:Person
    {
        public Enginer():base(){position = "Инженер";}
        public Enginer(string name_input, string surname_input, string gender_input, int age_input):base(name_input, surname_input, gender_input, age_input){position = "Инженер";}

        public override void Print()
        {
            Console.WriteLine("{0}: Имя:{1}      Фамилия:{2}     Пол:{3}     Возраст:{4}", position, name, surname, gender, age);
        }
    }
    internal class Program //Вариант 2
    {
        static void Menu(Queue<Person> factory, List<Person> shop, MyCollection<Person> myCollecton)
        {
            bool stat = true;
            while(stat)
            {
                Console.WriteLine("1.Выбор данных.");
                Console.WriteLine("2.Счетчик.");
                Console.WriteLine("3.Операия над множествами.");
                Console.WriteLine("4.Агрегирование.");
                Console.WriteLine("5.Группирование.");
                Console.WriteLine("6.Сортировка по возрастанию.");
                Console.WriteLine("7.Сортировка по убыванию.");
                Console.WriteLine("8.Выход.");
                int m = Convert.ToInt32(Console.ReadLine());
                switch(m)
                {
                    case 1:
                        Console.Write("Поиск:");
                        string? param = Console.ReadLine();
                        myCollecton.Selection(person=>person.ToString().Contains(param));
                        Sample(param, factory);
                        Sample(param, shop);
                        break;
                    case 2:
                        Console.Write("Поиск:");
                        string? request = Console.ReadLine();
                        myCollecton.CountElement(person=>person.ToString().Contains(request));
                        Counter(request, factory);
                        Counter(request, shop);
                        break;
                    case 3:
                        Exceptions(factory, shop);
                        break;
                    case 4:
                        Aggregatio(factory);
                        Aggregatio(shop);
                        break;
                    case 5:
                        myCollecton.Grouping(person=>person.position);
                        Grouping(factory);
                        Grouping(shop);
                        break;
                    case 6:
                        myCollecton = myCollecton.Sorted(p=>p.name);
                        Print(myCollecton);
                        break;
                    case 7:
                        myCollecton = myCollecton.SortedDescending(p=>p.name);
                        Print(myCollecton);
                        break;
                    case 8:
                        stat = false;
                        break;
                    default:
                        Console.WriteLine("Неверный параметр.");
                        break;
                }
            }
        }
        static void CreatCollection(ref Queue<Person> factory, ref List<Person> shop, ref MyCollection<Person> myCollection)
        {
            Person p = new Person();
            Console.Write("Количество элементов:");
            int size = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < size; i++)
            {
                myCollection.AddToEnd(p.GenericPerson());
                p = p.GenericPerson();
                factory.Enqueue(p);
                if (p is Enginer e) shop.Add(e);
                if (p is Worker w) shop.Add(w);
            }
        }
        static void Sample(string? param, object data)
        {
            if (param == "W" || param == "M") param = " "+param+" ";
            if(data is List<Person> shop)
            {
                var subset = from personal in shop where personal.ToString().Contains(param) orderby personal.name select personal;
                Console.WriteLine("Метод операций с запросом:");
                foreach (Person p in subset)
                    p.Print();
            }
            else if (data is Queue<Person> factory)
            {
                var subset = factory.Where(person => person.ToString().Contains(param)).OrderBy(person => person.name).Select(person => person);
                Console.WriteLine("Запрос расширяющихся методов:");
                foreach (Person p in subset)
                    p.Print();
            }
        }
        static void Counter(string? request,object data)
        {
            int numb = 0;
            if (data is List<Person> shop)
            {
                Console.WriteLine("Метод операций с запросом:");
                numb = (from person in shop where person.position == request select person).Count<Person>();
            }
            else if (data is Queue<Person> factory)
            {
                Console.WriteLine("Запрос расширяющихся методов:");
                numb = factory.Where(person=>person.position == request).Select(person=>person).Count<Person>();
            }
            Console.WriteLine("Колличество {0} в коллекции:{1}", request, numb.ToString());
        }
        static void Exceptions(Queue<Person> factory, List<Person> shop)
        {
            Console.WriteLine("Метод операций с запросом:");
            var personDiff = (from a in factory select a).Except(from b in shop select b);
            Console.WriteLine("Разность множеств:");
            foreach (Person p in personDiff)
                p.Print();
            Console.WriteLine("Запрос расширяющихся методов:");
            personDiff = (factory.Select(a=>a)).Except(shop.Select(b=>b));
            Console.WriteLine("Разность множеств:");
            foreach (Person p in personDiff)
                p.Print();
        }
        static void Aggregatio(object data)
        {
            if (data is List<Person> shop)
            {
                Console.WriteLine("Метод операций с запросом:");
                Console.WriteLine("Max age = " + (from age in shop select age.age).Max());
                Console.WriteLine("Min age = " + (from age in shop select age.age).Min());
            }
            if (data is Queue<Person> factory)
            {
                Console.WriteLine("Запрос расширяющихся методов:");
                Console.WriteLine("Max age = " + (factory.Select(age=>age.age).Max()));
                Console.WriteLine("Min age = " + (factory.Select(age=>age.age).Min()));
            }
        }
        static void Grouping(object data)
        {
            if (data is List<Person> shop)
            {
                Console.WriteLine("Метод операций с запросом:");
                var post = from person in shop group person by person.position;
                foreach (var p in post)
                {
                    Console.WriteLine(p.Key);
                    foreach(var person in p)
                    {
                        Console.WriteLine(person.ToString());
                    }
                    Console.WriteLine();
                }
            }
            if (data is Queue<Person> factory)
            {
                Console.WriteLine("Запрос расширяющихся методов:");
                var post = factory.GroupBy(person => person.position);
                foreach (var p in post)
                {
                    Console.WriteLine(p.Key);
                    foreach(var person in p)
                    {
                        Console.WriteLine(person.ToString());
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Print(MyCollection<Person> myCollection)
        {
            foreach(Person x in myCollection)
                x.Print();
        }
        static void Main(string[] argv)
        {
            MyCollection<Person> myCollection = new MyCollection<Person>();
            var shop = new List<Person>();
            var factory = new Queue<Person>();
            CreatCollection(ref factory, ref shop, ref myCollection);
            Menu(factory, shop, myCollection);
        }
    }
}