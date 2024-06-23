using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Project1
{
    // Базовый класс коллекции (однонапраленный список)
    class Collection<T> : ICloneable, IEnumerable<T>
    {
        public T value; // Элемент
        public Collection<T> next; // Сылка на соедующий элемент
        private int size; // Размер коллекции
        
        // Конструктор без параметров
        public Collection()
        {
            value = default(T);
            next = null;   
        }

        // Конструктор с параметрам (уставливает фиксированный размер коллекци)
        public Collection(int capacity)
        {
            value = default(T);
            next = null;
            size = capacity;
        }

        // Конструктор с параметрам (создает коллекию на основе уже созданной).
        public Collection(Collection<T> c)
        {
            value = c.value;
            next = c.next;
            size = c.size;
        }
        
        // Считает элементы в коллекции
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

        // Вставка элемента в начало коллекции
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

        // Вставка элемента в конец коллекции
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

        // Удаляет переданный элемент из коллекции, если он там есть
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

        // Удаляет элемент из колекции находящийся на переданной позиции
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

        // Метод поиска по элементу
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

        // Метод глубокого копирования коллекции
        public object Clone()
        {
            return new Collection<T>(this);
        }

        // Фунеция поверхностного копирования коллекции
        public object SurfaceCopy()
        {
            return this.MemberwiseClone();
        }

        // Удаялет коллецию из памяти
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

        // Печать коллекции
        public void Print (int c)
        {
            Collection<T> x = this;
            while (x != null)
            {
                if (c == 0)
                {
                    string[] data = (x.value.ToString()).Split(' ');
                    {Console.WriteLine("Имя:{0} Фамилия:{1} Пол:{2} Возраст:{3}", data[0],data[1],data[2],data[3]);}
                }
                x = x.next;
            }
        }
        
        // Реализация метода интерфейса IEnumerable.GetEnumerator()
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); // Возвращаем перечислитель, используя метод GetEnumerator() для обобщенного интерфейса
        }

        // Реализация метода интерфейса IEnumerable<T>.GetEnumerator()
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

    // Реализация собственного кдасса IEnumerator
    class MyEnumerator<T> : IEnumerator<T>
    {
        Collection<T> beg;
        Collection<T> current;

        // Конструктор класса, принимающий коллекцию
        public MyEnumerator(Collection<T> c)
        {
            beg = c;
            current = c;
        }

        // Свойство Current для реализации интерфейса IEnumerator (необобщенный)
        object IEnumerator.Current
        {
            get{ return Current;}
        }

        // Свойство Current для реализации интерфейса IEnumerator<T> (обобщенный)
        public T Current
        {
            get {return current.value;}
        }

        // Метод для сброса перечислителя в начальное состояние
        public void Reset()
        {
            current = this.beg;
        }

        public void Dispose(){}

        // Метод для перехода к следующему элементу коллекции
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

    // Класс коллекции, расширяющий базовый класс Collection<Person>
    class MyCollection:Collection<Person>
    {

        // Коснтрукторы соответствуют базовому классу
        public MyCollection():base(){} 
        public MyCollection(MyCollection c):base(c){}

        // Преопределенная функцию вставки элементв в начало
        public override Collection<Person> AddToBeg(Person x)
        {
            return base.AddToBeg(x);
        }

        // Преопределенная функцию вставки элементв в конец
        public override Collection<Person> AddToEnd(Person x)
        {
            return base.AddToEnd(x);
        }
        
        // Преопределенная функцию удаления элемента по значению
        public override Collection<Person> DelElement(Person f)
        {
            return base.DelElement(f);
        }

        // Преопределенная функцию удаления элемента по индексу
        public override Collection<Person> DelElementFromNumber(int j)
        {
            return base.DelElementFromNumber(j);
        }

        // Преопределенная функцию удаления коллекции
        public override void Del()
        {
            base.Del();
        }

        // Метод элементов по имени
        private MyCollection SortName()
        {
            MyCollection m = this; // Сылка на текующую коллекция
            MyCollection sorted = new MyCollection();  // Создаем новую коллекция для записи в нее отсортрованных данных
            MyCollection x = new MyCollection(m); /// Создаем копию текущего состояния коллекции
            while (x != null) // Итерируемся по элементам коллекции
            {
                Person min = null; // Сохраняем ссылку на минимальный элемент
                if (x.next == null){ min = x.value; x = null;} // если это послежний элемент коллекции, то едниственный элемент будет минимальным, и очищаем коллекцию
                else
                {
                    Collection<Person> c = new MyCollection(x); // Создаем копию текущего состояния коллекции
                    while (c.next != null)  // Итерируемся по элементам коллекции
                    {
                         // Находим минимальный элемент по имени
                        for (int i = 0; i < (c.value.name.Length > c.next.value.name.Length ? c.next.value.name.Length:c.value.name.Length);i++)
                        {
                            // Сравниваем символы имени текущего элемента и следующего элемента
                            if (c.value.name.ToCharArray()[i] < c.next.value.name.ToCharArray()[i]) 
                                if (min == null)                                                               // Если минимальный элемент еще не найден
                                    {min = c.value; break;}                                                    // Устанавливаем текущий элемент как минимальный, и прерываем цикл
                                else           
                                    if (c.value.name.ToCharArray()[i] < min.name.ToCharArray()[i])             // Если текущий элемент меньше текущего минимального элемента
                                        {min = c.value; break;}                                                // Устанавливаем текущий элемент как минимальный
                                    else break;        
                            if (c.value.name.ToCharArray()[i] > c.next.value.name.ToCharArray()[i])            // Если следующий элемент меньше текущего элемента
                            {      
                                if (min == null)                                                               // Если минимальный элемент еще не найден
                                    {min = c.next.value;break;}                                                // Устанавливаем следующий элемент как минимальный
                                else       
                                    if (c.next.value.name.ToCharArray()[i] < min.name.ToCharArray()[i])        // Если следующий элемент меньше текущего минимального элемента
                                        {min = c.next.value;break;}                                            // Устанавливаем следующий элемент как минимальный
                                    else break;
                            }
                            if (c.value.name.ToCharArray()[i] == c.next.value.name.ToCharArray()[i]) continue; // Если текущий элемент и следующий элемент равны, продолжаем сравнение следующего символа
                        }
                        c = c.next;                                                                            // Переходим к следующему элементу
                    }
                }
                sorted.AddToEnd(min);                                                                          // Добавляем минимальный элемент в отсортированную коллекцию
                 if (x != null)
                    x.DelElement(min);                                                                         // Удаляем минимальный элемент из текущей коллекции
            }
            return sorted;                                                                                     // Возвращает отсортированную коллекцию
        }

        // Метод элементов по фамилии, алгорит тотже самыя, что и для сортировки по имени, но вместо имени сравниваеться фамилии
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

        // Метод элементов по должности, алгорит тотже самыя, что и для сортировки по имени и фамилии, но вместо имени сравниваеться должность
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

        // Метод элементов по полу, алгорит тотже самыя, что и для сортировки по имени и фамилии, но вместо имени сравниваеться пол
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

        // Метод элементов по возрасту, алгорит тотже самыя, что и для сортировки по имени и фамилии, но вместо имени сравниваеться возраст
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

        // Выводи текстовое менб сортировки в консоль
        public MyCollection Sort(int i)
        {
            if (i == 1) return SortPosition();
            if (i == 2) return SortName();
            if (i == 3) return SortSurname();
            if (i == 4) return SortGender();
            if (i == 5) return SortAge();
            else {Console.WriteLine("Неверный параметр."); return this;}
        }

        // Возвращает длину коллекции
        public int Length
        {   
            get {return Count();}
        }
        
        // Реалищцаия интерфейса IEnumerator()
        public override IEnumerator<Person> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
    
    // Класс коллекции, расширяющий базовый класс MyCollection
    class NewMyCollection:MyCollection
    {
        public string NameCollection{get; set;}                                                 // Свойство для хранения имени коллекции
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args); // Делегат для обработки событий изменения коллекции
        public event CollectionHandler CollectionCountChanged;                                   // Событие, вызываемое при изменении количества элементов в коллекции
        public event CollectionHandler CollectionReferenceChanged;                              // Событие, вызываемое при изменении ссылки на элемент коллекции
        
        // Метод для вызова события изменения количества элементов
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)              // Проверяем, есть ли подписчики на событие
                CollectionCountChanged(source,args);         // Вызываем событие
        }
        
        // Метод для вызова события изменения ссылки на элемент
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)         // Проверяем, есть ли подписчики на событие
                CollectionReferenceChanged(source, args);   // Вызываем событие
        }

        // Переопределенный метод для добавления элемента в конец коллекции
        public override Collection<Person> AddToEnd(Person x)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "добавление в конец коллекции", x));  // Вызываем событие изменения количества элементов
            return base.AddToEnd(x);                                                                                            // Вызываем базовый метод для добавления элемента
        }

        // Переопределенный метод для добавления элемента в начало коллекции
        public override Collection<Person> AddToBeg(Person x)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "добавление в начало коллекции", x));// Вызываем событие изменения количества элементов
            return base.AddToBeg(x);                                                                                           // Вызываем базовый метод для добавления элемента
        }

        // Метод для удаления элемента по индексу
        public bool Remove(int j)
        {
            if (0 < j && j <= Length)                                                                                // Проверяем, находится ли индекс в пределах допустимого диапазона
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(NameCollection, "удаление", this[j])); // Вызываем событие изменения количества элементов
                DelElementFromNumber(j);                                                                             // Удаляем элемент по индексу
                return true;
            }
            return false;
        }

        // Индексатор для доступа к элементам коллекции по индексу
        public Person this[int index]
        {
            get 
            {
                Collection<Person> x = this;                        // Переменная для хранения текущего элемента
                for (int i = 0; i < Length &&  i != index; i++)     // Итерируемся по элементам коллекции до достижения заданного индекса
                {
                    x = x.next;
                }
                return x.value;                                     // Возвращаем значение элемента по заданному индексу
            }
            set 
            {   
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(NameCollection, "изменение", this[index]));    // Вызываем событие изменения ссылки на элемент
                Collection<Person> x = this;        
                for (int i = 0; i < Length &&  i == index; i++)
                {
                    x = x.next;
                }
                x.value = value;
            }
        }
    }

    // Класс события для коллекции
    class CollectionHandlerEventArgs:System.EventArgs
    {
        public string name_collection{get;set;}  // Свойство для хранения имени коллекции, в которой произошло событие
        public string event_type{get;set;}       // Свойство для хранения типа события (например, добавление или удаление элемента)
        public Person element{get;set;}          // Свойство для хранения элемента коллекции, с которым связано событие

         // Конструктор для инициализации аргументов события
        public CollectionHandlerEventArgs(string input_name_collection, string input_event_type, Person input_col)
        {
            name_collection = input_name_collection;
            event_type = input_event_type;
            element = input_col;
        }

    }

    // Класс журнал, для хранения событий
    class Journal
    {
        public string name_collection{get;set;}         // Свойство для хранения имени коллекции, в которой произошло событие
        public string event_type{get;set;}              // Свойство для хранения типа события (например, добавление или удаление элемента)
        public string data {get;set;}                   // Свойство для хранения элемента коллекции, с которым связано событие
        public List<Journal> journal { get; set; }      // Журнал событий коллекции

        // Конструктор по умолчанию инициализирует список журнал
        public Journal()
        {
            journal = new List<Journal>();
        }

        // Конструктор для инициализации события журнала
        public Journal(string input_name_collection, string input_event_type, string intput_data)
        {
            name_collection = input_name_collection;
            event_type = input_event_type;
            data = intput_data;
        }

         // Переопределенный метод ToString для форматированного вывода информации о событии
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

        // Метод для обработки события изменения количества элементов в коллекции
        public void CollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            Journal j = new Journal(args.name_collection,args.event_type, args.element.ToString());
            journal.Add(j);
        }

         // Метод для обработки события изменения ссылки на элемент в коллекции
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
