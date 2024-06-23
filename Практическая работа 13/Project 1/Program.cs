using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Project1
{
    class Collection<T> : ICloneable, IEnumerable<T>
    {
        public T value;
        public Collection<T> next;
        private int size;

        public Collection()
        {
            value = default(T);
            next = null;   
        }
        public Collection(int capacity)
        {
            value = default(T);
            next = null;
            size = capacity;
        }
        public Collection(Collection<T> c)
        {
            value = c.value;
            next = c.next;
            size = c.size;
        }
        virtual public int Count()
        {
            int count = 0;
            Collection<T> c = this;
            while (c != null){
                count++;
                c = c.next;
            }
            return count;
        }
        public virtual Collection<T> AddToBeg(T x)
        {
            if (this.size != 0 && this.size == Count())
            {
                Console.WriteLine("Переполнение коллекции.");
                return this;
            }
            Collection<T> c = new Collection<T>();
            if(value != null)
            {
                c.next = this;
                c.value = x;
            }
            else c.value = x;
            return c;
        }
        public virtual Collection<T> AddToEnd(T x)
        {
            if (this.size != 0 && this.size == Count())
            {
                Console.WriteLine("Переполнение коллекции.");
                return this;
            }
            if(this.value == null)
            {
                Collection<T> first = this;
                first.value = x;
                return first;
            }
            Collection<T> c = new Collection<T>();
            Collection<T> beg = this;
            Collection<T> r = beg;
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
        public virtual Collection<T> DelElement(T f)
        {
            Collection<T> a = new Collection<T>(1);
            a.value = f;
            Collection<T> beg = this;
            if (beg.next == null)
            {
                beg = null;
                return beg;
            }   
            Collection<T> r = beg;
            if (string.Compare(beg.value.ToString(), a.value.ToString()) == 0)
            {
                value = beg.next.value;
                next = beg.next.next;
                return this; 
            }
            while (r.next != null)
            {
                if (string.Compare(r.next.value.ToString(), a.value.ToString()) == 0) 
                {
                    if (r.next != null) break;
                    else
                    {
                        beg.next = null;
                        return beg; 
                    }
                }
                r = r.next;
            }
            r.next = r.next.next;
            return beg;
        }
        virtual public Collection<T> DelElementFromNumber(int d)
        {
            if (d <= Count())
            {
                Collection<T> beg = this;
                Collection<T> r = beg;
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
            Collection<T> a = new Collection<T>(1);
            a.value = f;
            Collection<T> x = this;
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
            return new Collection<T>(this);
        }
        public object SurfaceCopy()
        {
            return this.MemberwiseClone();
        }
        public virtual void Del()
        {
            this.next = null;
            this.value = default;
        }
        public void Print(Collection<T> c)
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
            Collection<T> x = this;
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
        virtual public IEnumerator<T> GetEnumerator()
        {
            Collection<T> current = this;
            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }
    }
    class MyEnumerator<T> : IEnumerator<T>
    {
        Collection<T> beg;
        Collection<T> current;

        public MyEnumerator(Collection<T> c)
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
    class MyCollection:Collection<Person>
    {
        public MyCollection():base(){}
        public MyCollection(MyCollection c):base(c){}

        public override Collection<Person> AddToBeg(Person x)
        {
            return base.AddToBeg(x);
        }
        public override Collection<Person> AddToEnd(Person x)
        {
            return base.AddToEnd(x);
        }
        public override Collection<Person> DelElement(Person f)
        {
            return base.DelElement(f);
        }
        public override Collection<Person> DelElementFromNumber(int j)
        {
            return base.DelElementFromNumber(j);
        }
        public override void Del()
        {
            base.Del();
        }
        private MyCollection SortName()
        {
            MyCollection m = this;
            MyCollection sorted = new MyCollection(); 
            MyCollection x = new MyCollection(m);
            while (x != null)
            {
                Person min = null;
                if (x.next == null){ min = x.value; x = null;}
                else
                {
                    Collection<Person> c = new MyCollection(x);
                    while (c.next != null)
                    {
                        for (int i = 0; i < (c.value.name.Length > c.next.value.name.Length ? c.next.value.name.Length:c.value.name.Length);i++)
                        {
                            if (c.value.name.ToCharArray()[i] < c.next.value.name.ToCharArray()[i])
                                if (min == null)
                                    {min = c.value; break;}
                                else 
                                    if (c.value.name.ToCharArray()[i] < min.name.ToCharArray()[i])
                                        {min = c.value; break;}
                                    else break;
                            if (c.value.name.ToCharArray()[i] > c.next.value.name.ToCharArray()[i])
                            {
                                if (min == null)
                                    {min = c.next.value;break;}
                                else
                                    if (c.next.value.name.ToCharArray()[i] < min.name.ToCharArray()[i])
                                        {min = c.next.value;break;}
                                    else break;
                            }
                            if (c.value.name.ToCharArray()[i] == c.next.value.name.ToCharArray()[i]) continue;
                        }
                        c = c.next;
                    }
                }
                sorted.AddToEnd(min);
                 if (x != null)
                    x.DelElement(min);
            }
            return sorted;
        }
        private MyCollection SortSurname()
        {
            MyCollection m = this;
            MyCollection sorted = new MyCollection(); 
            MyCollection x = new MyCollection(m);
            while (x != null)
            {
                Person min = null;
                if (x.next == null){ min = x.value; x = null;}
                else
                {
                    Collection<Person> c = new MyCollection(x);
                    while (c.next != null)
                    {
                        for (int i = 0; i < (c.value.surname.Length < c.next.value.surname.Length ? c.next.value.surname.Length:c.value.surname.Length);i++)
                        {
                            if (c.value.surname.ToCharArray()[i] < c.next.value.surname.ToCharArray()[i])
                                if (min == null)
                                    {min = c.value; break;}
                                else 
                                    if (c.value.surname.ToCharArray()[i] < min.surname.ToCharArray()[i])
                                        {min = c.value; break;}
                                    else break;
                            if (c.value.surname.ToCharArray()[i] > c.next.value.surname.ToCharArray()[i])
                            {
                                if (min == null)
                                    {min = c.next.value;break;}
                                else
                                    if (c.next.value.surname.ToCharArray()[i] < min.surname.ToCharArray()[i])
                                        {min = c.next.value;break;}
                                    else break;
                            }
                            if (c.value.surname.ToCharArray()[i] == c.next.value.surname.ToCharArray()[i]) continue;
                        }
                        c = c.next;
                    }
                }
                sorted.AddToEnd(min);
                 if (x != null)
                    x.DelElement(min);
            }
            return sorted;
        }
        private MyCollection SortPosition()
        {
            MyCollection m = this;
            MyCollection sorted = new MyCollection(); 
            MyCollection x = new MyCollection(m);
            while (x != null)
            {
                Person min = null;
                if (x.next == null){ min = x.value; x = null;}
                else
                {
                    Collection<Person> c = new MyCollection(x);
                    while (c.next != null)
                    {
                        for (int i = 0; i < (c.value.position.Length > c.next.value.position.Length ? c.next.value.position.Length:c.value.surname.Length);i++)
                        {
                            if (c.value.position.ToCharArray()[i] <= c.next.value.position.ToCharArray()[i])
                                if (min == null)
                                    {min = c.value; break;}
                                else 
                                    if (c.value.position.ToCharArray()[i] < min.position.ToCharArray()[i])
                                        {min = c.value; break;}
                                    else break;
                            if (c.value.position.ToCharArray()[i] > c.next.value.position.ToCharArray()[i])
                            {
                                if (min == null)
                                    {min = c.next.value;break;}
                                else
                                    if (c.next.value.position.ToCharArray()[i] < min.position.ToCharArray()[i])
                                        {min = c.next.value;break;}
                                    else break;
                            }
                            if (c.value.position.ToCharArray()[i] == c.next.value.position.ToCharArray()[i]) continue;
                        }
                        c = c.next;
                    }
                }
                sorted.AddToEnd(min);
                if (x != null)
                    x.DelElement(min);
            }
            return sorted;
        }
        private MyCollection SortGender()
        {
            MyCollection m = this;
            MyCollection sorted = new MyCollection(); 
            MyCollection x = new MyCollection(m);
            while (x != null)
            {
                Person min = null;
                if (x.next == null){ min = x.value; x = null;}
                else
                {
                    Collection<Person> c = new MyCollection(x);
                    while (c.next != null)
                    {
                        if (c.value.gender.ToCharArray()[0] <= c.next.value.gender.ToCharArray()[0])
                            if (min == null)
                                min = c.value;
                            else 
                                if (c.value.gender.ToCharArray()[0] < min.gender.ToCharArray()[0])
                                    min = c.value;
                        if (c.value.gender.ToCharArray()[0] > c.next.value.gender.ToCharArray()[0])
                        {
                            if (min == null)
                                min = c.next.value;
                            else
                                if (c.next.value.gender.ToCharArray()[0] < min.gender.ToCharArray()[0])
                                    min = c.next.value;
                        }
                        c = c.next;
                    }
                }
                sorted.AddToEnd(min);
                 if (x != null)
                    x.DelElement(min);
            }
            return sorted;
        }
        private MyCollection SortAge()
        {
            MyCollection m = this;
            MyCollection sorted = new MyCollection(); 
            MyCollection x = new MyCollection(m);
            while (x != null)
            {
                Person min = null;
                if (x.next == null){ min = x.value; x = null;}
                else
                {
                    Collection<Person> c = new MyCollection(x);
                    while(c.next != null)
                    {
                        if (c.value.age < c.next.value.age)
                        {
                            if (min == null)
                                min = c.value;
                            else 
                                if (c.value.age < min.age)
                                    min = c.value;
                        }
                        else 
                        {
                            if (min == null)
                                min = c.next.value;
                            else
                                if (c.next.value.age < min.age)
                                    min = c.next.value;
                        }
                        c = c.next;
                    }
                }
                sorted.AddToEnd(min);
                if (x != null)
                    x.DelElement(min);
            }
            return sorted;
        }
        public MyCollection Sort(int i)
        {
            if (i == 1) return SortPosition();
            if (i == 2) return SortName();
            if (i == 3) return SortSurname();
            if (i == 4) return SortGender();
            if (i == 5) return SortAge();
            else {Console.WriteLine("Неверный параметр."); return this;}
        }
        public int Length
        {   
            get {return Count();}
        }
        public override IEnumerator<Person> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
    class NewMyCollection:MyCollection
    {
        public string NameCollection{get; set;}
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source,args);
        }
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }
        public override Collection<Person> AddToEnd(Person x)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "добавление в конец коллекции", x));
            return base.AddToEnd(x);
        }
        public override Collection<Person> AddToBeg(Person x)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "добавление в начало коллекции", x));
            return base.AddToBeg(x);
        }
        public bool Remove(int j)
        {
            if (0 < j && j <= Length)
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "удаление", this[j]));
                DelElementFromNumber(j);
                return true;
            }
            return false;
        }
        public Person this[int index]
        {
            get 
            {
                Collection<Person> x = this;
                for (int i = 0; i < Length &&  i != index; i++)
                {
                    x = x.next;
                }
                return x.value;
            }
            set 
            {   
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(NameCollection, "изменение", this[index]));
                Collection<Person> x = this;
                for (int i = 0; i < Length &&  i == index; i++)
                {
                    x = x.next;
                }
                x.value = value;
            }
        }
    }
    class CollectionHandlerEventArgs:System.EventArgs
    {
        public string name_collection{get;set;}
        public string event_type{get;set;}
        public Person element{get;set;}

        public CollectionHandlerEventArgs(string input_name_collection, string input_event_type, Person input_col)
        {
            name_collection = input_name_collection;
            event_type = input_event_type;
            element = input_col;
        }

    }
    class Journal
    {
        public string name_collection{get;set;}
        public string event_type{get;set;}
        public string data {get;set;}
        public List<Journal> journal { get; set; }

        public Journal()
        {
            journal = new List<Journal>();
        }
        public Journal(string input_name_collection, string input_event_type, string intput_data)
        {
            name_collection = input_name_collection;
            event_type = input_event_type;
            data = intput_data;
        }
        public override string ToString()
        {
            string from;
            if (event_type.Contains("добавление"))
                from = " в коллекцию ";
            else if (event_type.Contains("изменение"))
                from = " в коллекции ";
            else
                from = " из коллекции ";
            return event_type + " " + data + from + name_collection;
        }

        public void CollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            Journal j = new Journal(args.name_collection,args.event_type, args.element.ToString());
            journal.Add(j);
        }
        public void CollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            Journal j = new Journal(args.name_collection,args.event_type, args.element.ToString());
            journal.Add(j);
        }
    }
    class Person
    {
        public string name;
        public string surname;
        public string gender;
        public int age;
        public string position;

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
                    return null;
            }
            return null;
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

    internal class Program
    {
        static int Menu(int m)
        {
            if (m == 0)
            {
                Console.WriteLine("1. Печать коллекции.");
                Console.WriteLine("2. Добавить элемент в коллекциию.");
                Console.WriteLine("3. Удалить элемент из коллекии.");
                Console.WriteLine("4. Изменить элемент в коллекции.");
                Console.WriteLine("5. Выход.");
            }
            if (1 <= m && m <= 4) {Console.Write("Введите имя коллекции:"); return m = 0;}
            return m = Convert.ToInt32(Console.ReadLine());
        }
        static void Creat(ref NewMyCollection col)
        {
            Person a = new Person();
            Console.Write("Количество элементов в коллекции:");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введеите имя коллекции:");
            col.NameCollection = Console.ReadLine();
            for (int i = 0; i < size; i++)
            {
                col.AddToEnd(a.GenericPerson());
            }
        }
        void CreatJornal()
        {

        }
        static void PrintCollection(NewMyCollection col)
        {
            Console.WriteLine("Содержание коллекции {0}:",col.NameCollection);
            foreach (Person x in col)
            {
                x.Print();
            }
        }
        static void AddCollection(ref NewMyCollection col)
        {

        }
        static void DelElementFromCollection(ref NewMyCollection col)
        {
            Console.Write("Введите номер для удаления:");
            int j = Convert.ToInt32(Console.ReadLine());
            if (col.Remove(j))
                Console.WriteLine("Элемент удален.");
            else
                Console.WriteLine("Ошибка. Элемент не найден.");
        }
        static void TransformDataCollection(ref NewMyCollection col)
        {
            Console.Write("Введите номер для изменения:");
            int j = Convert.ToInt32(Console.ReadLine());
            if (0 <= j && j < col.Length)
            {
                Person a = new Person();
                col[j] = a.GenericPerson();
            }
            else Console.WriteLine("Выход за пределы коллекции.");
        }
        static void PrintJournal (Journal j)
        {
            foreach(Journal x in j.journal)
                Console.WriteLine(x.ToString());
        }
        static void Main (string[] args)
        {
            bool status = true;
            NewMyCollection col1 = new NewMyCollection();
            NewMyCollection col2 = new NewMyCollection();
            Creat(ref col1);
            Creat(ref col2);
            Journal j1 = new Journal();
            col1.CollectionCountChanged += new NewMyCollection.CollectionHandler(j1.CollectionCountChanged);
            col1.CollectionReferenceChanged += new NewMyCollection.CollectionHandler(j1.CollectionReferenceChanged);
            Journal j2 = new Journal();
            col1.CollectionReferenceChanged += new NewMyCollection.CollectionHandler(j2.CollectionReferenceChanged);
            col2.CollectionReferenceChanged += new NewMyCollection.CollectionHandler(j2.CollectionReferenceChanged);
            while (status)
            {
                int m = 0;  
                m = Menu(m);
                switch (m)
                {
                    case 1:
                        Console.WriteLine("Коллекции: "+ col1.NameCollection + ", " + col2.NameCollection);
                        Menu(m);
                        string name = Console.ReadLine();
                        if (name == col1.NameCollection)
                            PrintCollection(col1);
                        else if (name == col2.NameCollection)
                            PrintCollection(col2);
                        else
                            Console.WriteLine("Коллекция не найдена");
                        break;
                    case 2:
                        Console.WriteLine("Коллекции: "+ col1.NameCollection + ", " + col2.NameCollection);
                        Menu(m);
                        name = Console.ReadLine();
                        if (name == col1.NameCollection)
                        {
                            Person a = new Person();
                            col1.AddToEnd(a.GenericPerson());
                        }
                        else if (name == col2.NameCollection)
                        {
                            Person a = new Person();
                            col2.AddToEnd(a.GenericPerson());
                        }
                        else
                        {
                            Console.WriteLine("Коллекция не найдена");
                        }    
                        break;
                    case 3:
                        Console.WriteLine("Коллекции: "+ col1.NameCollection + ", " + col2.NameCollection);
                        Menu(m);
                        name = Console.ReadLine();
                        if (name == col1.NameCollection)
                            DelElementFromCollection(ref col1);
                        else if (name == col2.NameCollection)
                            DelElementFromCollection(ref col2);
                        else
                            Console.WriteLine("Коллекция не найдена");
                        break;
                    case 4:
                        Console.WriteLine("Коллекции: "+ col1.NameCollection + ", " + col2.NameCollection);
                        Menu(m);
                        name = Console.ReadLine();
                        if (name == col1.NameCollection)
                            TransformDataCollection(ref col1);
                        else if (name == col2.NameCollection)
                            TransformDataCollection(ref col2);
                        else
                        {
                            Console.WriteLine("Коллекция не найдена");
                        }    
                        break;
                        case 5:
                            status = false;
                            break;
                        default:
                            Console.WriteLine("Неверный параметр.");
                            break;
                } 
            }
            Console.WriteLine("Журнал №1:");
            if(j1.journal.Count() != 0)
                PrintJournal(j1);
            else
                System.Console.WriteLine("Empty");
            Console.WriteLine("Журнал №2:");
            if(j2.journal.Count() !=0)
                PrintJournal(j2);
            else
                System.Console.WriteLine("Empty");

            Console.Read();
        }
    }
}