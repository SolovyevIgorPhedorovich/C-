using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Project4
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
        static void Menu(int m)
        {
            if (m == 0){
            Console.WriteLine(@"
1.Создать коллекцию.
2.Добавить элемент в коллекцияю.
3.Поиск.
4.Удалить элемент.
5.Клонировать коллекцию.
6.Копировать коллекцию.
7.Печать колекции.
8.Удалть колекцию.
9.Выход.");
            }
            else if (m == 1)
            {
                Console.WriteLine(@"
Создать:
1.Пустую колецию.
2.Создать пустую колецию с фиксированным размером.
3.Создать колекцию на основе готовой колекции.
4.Отменить.");
            }
            else if (m == 2)
            {
                Console.WriteLine(@"1.Сгенерировать данные.
                2.Создать вручную.
                3.Отмена.");
            }
            else if (m == 3)
            {
                Console.WriteLine(@"
Поиск:
1. Рабочего.
2. Инженера.
3. Служащего.
4. Отмена.");
            }
            else if (m == 4)
            {
                Console.WriteLine(@"
1.Удалить по номеру.
2. Удалить по содержани.
3. Выход.");
            }
            else if (m == 7)
            {
                Console.WriteLine(@"
1.Печать всей колеция при помощи функции.
2. Печать всей коллекции при помощи 'foreach'.
3. Пеачать по номеру.
4. Отмена.");
            }
        }
        static void MenuPersonal()
        {
            Console.WriteLine("1. Инженер.");
            Console.WriteLine("2. Рабочий.");
            Console.WriteLine("3. Служайщий.");
        }
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
        static Person ElementForDelite()
        {
            MenuPersonal();
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Имя:");
            string name = Console.ReadLine();
            Console.Write("Фамилия:");
            string surname = Console.ReadLine();
            Console.Write("Пол:");
            string gender = Console.ReadLine();
            Console.Write("Возраст:");
            int age = Convert.ToInt32(Console.ReadLine());
            if (m == 1) return new Enginer(name, surname, gender, age);
            if (m == 2) return new Worker(name, surname, gender, age);
            if (m == 3) return new Employee(name, surname, gender, age);
            else return null;
        }
        static void Main (string[] argv)
        {
            bool run = true;
            MyCollection<Person> collect= null;
            while (run)
            {
                int m = 0; 
                Menu(m);
                m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1:
                        Menu(m);
                        m = Convert.ToInt32(Console.ReadLine());
                        switch(m)
                        {
                            case 1:
                                collect = new MyCollection<Person>();
                                break;
                            case 2:
                                Console.Write("Введите размер колекции:");
                                int size = Convert.ToInt32(Console.ReadLine());
                                collect = new MyCollection<Person>(size);
                                break;
                            case 3:
                                if (collect != null)
                                    collect = new MyCollection<Person>(collect);
                                break;
                            case 4:
                                break;
                            default:
                                Console.WriteLine("Неверный параметр.");
                                break;
                        }
                        break;
                    case 2:
                        Menu(m);
                        while (true)
                        {
                            m = Convert.ToInt32(Console.ReadLine());
                            if (m == 1) 
                            {
                                Console.WriteLine("Колличество генерируемых элементов:");
                                int x = Convert.ToInt32(Console.ReadLine());
                                for (int i = 0; i < x; i++)
                                {
                                    Person a = GenericPerson();
                                    a.Print();
                                    Console.WriteLine("1.Вставить в начало.");
                                    Console.WriteLine("2.Вставить в конец.");
                                    m = Convert.ToInt32(Console.ReadLine());
                                    if (m == 1) collect = collect.AddToBeg(a);
                                    else if (m == 2) collect = collect.AddToEnd(a);
                                    else {Console.WriteLine("Неверный параметр."); --i;}
                                }
                                break;
                            }
                            if (m == 2)
                            {
                                Console.WriteLine("Колличество элементов:");
                                int x = Convert.ToInt32(Console.ReadLine());
                                for (int i = 0; i < x; i++)
                                {
                                    Person a = null;
                                    Console.Write("Имя:");
                                    string name = Console.ReadLine();
                                    Console.Write("Фамилия:");
                                    string surname = Console.ReadLine();
                                    Console.Write("Пол:");
                                    string gender = Console.ReadLine();
                                    Console.Write("Возраст:");
                                    int age = Convert.ToInt32(Console.ReadLine());
                                    MenuPersonal();
                                    m = Convert.ToInt32(Console.ReadLine());
                                    if (m == 1) {a = new Enginer(name, surname, gender, age); a.Print();}
                                    if (m == 2) {a = new Worker(name, surname, gender, age); a.Print();}
                                    if (m == 3) {a = new Employee(name, surname, gender, age); a.Print();}
                                    Console.WriteLine("1.Вставить в начало.");
                                    Console.WriteLine("2.Вставить в конец.");
                                    m = Convert.ToInt32(Console.ReadLine());
                                    if (m == 1) collect.AddToBeg(a);
                                    if (m == 2) collect.AddToEnd(a);
                                    else {Console.WriteLine("Неверный параметр."); --i;}
                                }
                            }
                            if (m == 3) break;
                        }
                        break;
                    case 3:
                        Menu(m);
                        m = Convert.ToInt32(Console.ReadLine()); 
                        switch (m)
                        {
                            case 1:
                                Console.Write("Имя:");
                                string name = Console.ReadLine();
                                Console.Write("Фамилия:");
                                string surname = Console.ReadLine();
                                Console.Write("Пол:");
                                string gender = Console.ReadLine();
                                Console.Write("Возраст:");
                                int age = Convert.ToInt32(Console.ReadLine());
                                if (collect.Find(new Worker(name, surname, gender, age)))
                                    Console.WriteLine("Элемент найден.");
                                else Console.WriteLine("Элемент не найден.");
                                break;
                            case 2:
                                Console.Write("Имя:");
                                name = Console.ReadLine();
                                Console.Write("Фамилия:");
                                surname = Console.ReadLine();
                                Console.Write("Пол:");
                                gender = Console.ReadLine();
                                Console.Write("Возраст:");
                                age = Convert.ToInt32(Console.ReadLine());
                                if (collect.Find(new Enginer(name, surname, gender, age)))
                                    Console.WriteLine("Элемент найден.");
                                else Console.WriteLine("Элемент не найден.");
                                break;
                            case 3:
                                Console.Write("Имя:");
                                name = Console.ReadLine();
                                Console.Write("Фамилия:");
                                surname = Console.ReadLine();
                                Console.Write("Пол:");
                                gender = Console.ReadLine();
                                Console.Write("Возраст:");
                                age = Convert.ToInt32(Console.ReadLine());
                                if (collect.Find(new Employee(name, surname, gender, age)))
                                    Console.WriteLine("Элемент найден.");
                                else Console.WriteLine("Элемент не найден.");
                                break;
                            case 4:
                                break;
                            default:
                                Console.WriteLine("Неверный параметр.");
                                break;
                        }
                        break;
                    case 4:
                        Menu(m);
                        m = Convert.ToInt32(Console.ReadLine());
                        switch (m)
                        {
                            case 1:
                            Console.WriteLine("Номер для удланения:");
                            int d = Convert.ToInt32(Console.ReadLine());
                            collect. DelElementFromNumber(d);
                            break;
                            case 2:
                                Person el = ElementForDelite();
                                collect.DelElement(el);
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Неверный параметр.");
                                break;
                        }
                        break;
                    case 5:
                        object cloneCollect = collect.Clone();
                        Console.WriteLine("Клон:");
                        break;
                    case 6:
                        object copyCollect = collect.SurfaceCopy();
                        Console.WriteLine("Клон:", copyCollect);
                        break;
                    case 7:
                        Menu(m);
                        m = Convert.ToInt32(Console.ReadLine());
                        switch(m)
                        {
                            case 1:
                                collect.Print(collect);
                                break;
                            case 2:
                                foreach (Person v in collect)
                                {
                                    v.Print();
                                }
                                break;
                            case 3:
                                Console.Write("Номер элемента:");
                                int z = Convert.ToInt32(Console.ReadLine());
                                collect.Print(z);
                                break;
                            case 4: 
                                break;
                            default:
                                Console.WriteLine("Неверный параметр.");
                                break;
                        }
                        break;
                    case 8:
                        collect.Del();
                        break;
                    case 9:
                        run = false;
                        break;      
                    default:
                        Console.WriteLine("Неверный параметр.");
                        break;
                }
            }
            MyCollection<Person> test = new MyCollection<Person>();
            Person p = new Person("sd", "sd", "N", 24);
            test = test.AddToBeg(p);
            test = test.AddToEnd(p);
            test = test.AddToEnd(p);
            Console.WriteLine(test.Count());
            test.Find(p);
            test = test.DelElement(p);
        }
    }
}