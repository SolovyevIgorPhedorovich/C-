using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Person
    {
        public string name;
        public string surname;
        public string gender;
        protected int age;
        
        public Person(string nameInput, string surnameInput, string genderInput, int ageInput)
        {
            name = nameInput;
            surname = surnameInput;
            gender = genderInput;
            age = ageInput;
        }

        public void Find(string x){
            string[] param = x.Split();
            if ((name == param[0] || "" == param[0]) && (surname == param[1] || "" == param[1]) && (gender == param[2] || "" == param[2]) && (age == Convert.ToInt32(param[3]) || "" == param[3])){
                Print();
            }
            else
                Console.WriteLine("Персонал не найден.");
        }

        virtual public void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }

    public class Employee:Person
    {
        public Employee(string nameInput, string surnameInput, string genderInput, int ageInput):base(nameInput, surnameInput, genderInput, ageInput){}
        override public void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }

    public class Worker:Person
    {
        public Worker(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }

    public class Enginer:Person
    {
        public Enginer(string name_imput, string surname_input, string gender_input, int age_input) : base(name_imput, surname_input, gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", name, surname, gender, age);
        }
    }

    class Program
    {

        static void Menu(int x)
        {
            if (x == 1){
               Console.WriteLine(@"1. Добавить служащего.
2. Добавить инженера.
3. Добавить рабочего.
4. Назад."); 
            }
            else if (x == 3){
                Console.WriteLine(@"1. Количество элементов определенного вида.
2. Печать элементов определенного вида .
3. Удаление элементов определенного вида.
4. Назад."); 
            }
            else if (x == 6){
                Console.WriteLine(@"1. Поиск инженеров.
2. Поиск рабочих.
3. Поиск служащих.
4. Общий поиск.
5. Назад.");
            }
            else {
                Console.WriteLine(@"1. Добавить объект в коллекцию.
2. Удалить объект из колекции.
3. Запросы.
4.Клонирование коллекции.
5.Сортировака коллекции.
6. Поиск заданного элемента.
7. Заверишить работу.");
            }
        }
        static int ColElementCollection(string type, Hashtable PersonGroup)
        {
            int x = 0;
            foreach (var person in PersonGroup.Values)
            {
                if (Convert.ToString(person) == "Project1." + type)
                    x++;
            }
            return x;
        }

    static void PrintElementColletion(string type, Hashtable PersonGroup)
        {
            foreach (var person in PersonGroup.Values)
            {
                if (Convert.ToString(person) == "Project1." + type)
                    ((Person)person).Print();
            }
            Console.ReadLine();
        }

    static void DelElementCollection(string type, ref Hashtable PersonGroup)
        {
            var keysToRemove = new ArrayList();
            foreach (DictionaryEntry entry in PersonGroup)
            {
                if (Convert.ToString(entry.Value) == "Project1." + type)
                    keysToRemove.Add(entry.Key);
            }

            foreach (var key in keysToRemove)
            {
                PersonGroup.Remove(key);
            }
        }

    static void FindElement(int m, Hashtable PersonGroup)
    {
        Console.WriteLine("Не нужные фильтры оставить пустыми.");
        Console.Write("Имя:");
        string name = Console.ReadLine();
        Console.Write("Фамилия:");
        string surname = Console.ReadLine();
        Console.Write("Пол:");
        string gender = Console.ReadLine();
        Console.Write("Возраст:");
        string age = Console.ReadLine();
        string param = name + " " + surname + " " + gender + " " + age;

        switch (m)
        {
            case 1:
                foreach (var person in PersonGroup.Values)
                {
                    if (person is Enginer e)
                    {
                        e.Find(param);
                    }
                }
                break;
            case 2:
                foreach (var person in PersonGroup.Values)
                {
                    if (Convert.ToString(person) == "Project1.Worker")
                    {
                        ((Person)person).Find(param);
                    }
                }
                break;
            case 3:
                foreach (var person in PersonGroup.Values)
                {
                    if (Convert.ToString(person) == "Project1.Employee")
                    {
                        ((Person)person).Find(param);
                    }
                }
                break;
            case 4:
                foreach (var person in PersonGroup.Values)
                {
                    ((Person)person).Find(param);
                }
                break;
            case 5:
                break;
            default:
                Console.WriteLine("Неверный параметр.");
                break;
        }
    }

    static void Main(string[] args)
    {
        Hashtable PersonGroup = new Hashtable();

        PersonGroup.Add(1, new Employee("Tom", "Helt", "M", 20));
        PersonGroup.Add(2, new Enginer("Adam", "Lopepr", "M", 23));
        PersonGroup.Add(3, new Worker("Emver", "Hoeten", "W", 30));
        PersonGroup.Add(4, new Person("Daive", "Mains", "M", 45));

        bool sorting = false;

        while (true)
        {
            Console.Clear();
            foreach (Person x in PersonGroup.Values)
            {
                x.Print();
            }

            bool proces = true;
            int m = 0;
            Menu(m);
            m = Convert.ToInt32(Console.ReadLine());

            switch (m)
            {
                case 1:
                    Console.Clear();
                    Menu(m);
                    int m1 = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    switch (m1)
                    {
                        case 1:
                            Console.Write("Имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Фамилия: ");
                            string surname = Console.ReadLine();
                            Console.Write("Пол: ");
                            string gender = Console.ReadLine();
                            Console.Write("Возраст: ");
                            int age = Convert.ToInt32(Console.ReadLine());
                            PersonGroup.Add(PersonGroup.Count + 1, new Employee(name, surname, gender, age));
                            break;
                        case 2:
                            Console.Write("Имя: ");
                            name = Console.ReadLine();
                            Console.Write("Фамилия: ");
                            surname = Console.ReadLine();
                            Console.Write("Пол: ");
                            gender = Console.ReadLine();
                            Console.Write("Возраст: ");
                            age = Convert.ToInt32(Console.ReadLine());
                            PersonGroup.Add(PersonGroup.Count + 1, new Enginer(name, surname, gender, age));
                            break;
                        case 3:
                            Console.Write("Имя: ");
                            name = Console.ReadLine();
                            Console.Write("Фамилия: ");
                            surname = Console.ReadLine();
                            Console.Write("Пол: ");
                            gender = Console.ReadLine();
                            Console.Write("Возраст: ");
                            age = Convert.ToInt32(Console.ReadLine());
                            PersonGroup.Add(PersonGroup.Count + 1, new Worker(name, surname, gender, age));
                            break;
                        case 4:
                            m = 0;
                            break;
                        default:
                            Console.WriteLine("Неверный параметр");
                            break;
                    }
                    break;
                case 2:
                    Console.Write("Введите номер удаляемого элемента: ");
                    int x = Convert.ToInt32(Console.ReadLine());
                    PersonGroup.Remove(x);
                    break;
                case 3:
                    Console.Clear();
                    Menu(m);
                    m1 = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    switch (m1)
                    {
                        case 1:
                            Console.WriteLine("Вид элемента:");
                            Console.WriteLine("1. Инженер");
                            Console.WriteLine("2. Рабочий");
                            Console.WriteLine("3. Служащий");
                            int a = Convert.ToInt32(Console.ReadLine());
                            string type = "";
                            if (a == 1)
                                type = "Enginer";
                            else if (a == 2)
                                type = "Worker";
                            else if (a == 3)
                                type = "Employee";
                            else
                            {
                                Console.WriteLine("Неверный параметр");
                                break;
                            }

                            Console.WriteLine("Количество элементов {0}: {1}", type, ColElementCollection(type, PersonGroup));
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Вид элемента:");
                            Console.WriteLine("1. Инженер");
                            Console.WriteLine("2. Рабочий");
                            Console.WriteLine("3. Служащий");
                            a = Convert.ToInt32(Console.ReadLine());
                            type = "";
                            if (a == 1)
                                type = "Enginer";
                            else if (a == 2)
                                type = "Worker";
                            else if (a == 3)
                                type = "Employee";
                            else
                            {
                                Console.WriteLine("Неверный параметр");
                                break;
                            }
                            PrintElementColletion(type, PersonGroup);
                            break;
                        case 3:
                            Console.WriteLine("Вид элемента:");
                            Console.WriteLine("1. Инженер");
                            Console.WriteLine("2. Рабочий");
                            Console.WriteLine("3. Служащий");
                            a = Convert.ToInt32(Console.ReadLine());
                            type = "";
                            if (a == 1)
                                type = "Enginer";
                            else if (a == 2)
                                type = "Worker";
                            else if (a == 3)
                                type = "Employee";
                            else
                            {
                                Console.WriteLine("Неверный параметр");
                                break;
                            }
                            DelElementCollection(type, ref PersonGroup);
                            break;
                        default:
                            Console.WriteLine("Неверный параметр");
                            break;
                    }
                    break;
                case 4:
                    Hashtable ClonePersonGroup = new Hashtable(PersonGroup);
                    break;
                case 5:
                    if (!sorting)
                    {
                        List<Person> sortedList = new List<Person>(PersonGroup.Values.OfType<Person>());
                        sortedList.Sort();
                        PersonGroup.Clear();
                        for (int i = 0; i < sortedList.Count; i++)
                        {
                            PersonGroup.Add(i + 1, sortedList[i]);
                        }
                        Console.WriteLine("Коллекция успешно отсортирована.");
                        sorting = true;
                    }
                    else
                    {
                        Console.WriteLine("Коллекция уже отсортирована.");
                    }
                    break;
                case 6:
                    Menu(m);
                    m1 = Convert.ToInt32(Console.ReadLine());
                    FindElement(m1, PersonGroup);
                    break;
                case 7:
                    proces = false;
                    break;
                default:
                    Console.WriteLine("Неверный параметр");
                    break;
                }
                if (proces == false)
                    break;
            }
        }
    }
}