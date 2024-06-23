using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Person
    {
        public string Name;
        public string Surname;
        public string Gender;
        protected int Age;
        
        public Person(string NameInput, string SurnameInput, string GenderInput, int AgeInput)
        {
            Name = NameInput;
            Surname = SurnameInput;
            Gender = GenderInput;
            Age = AgeInput;
        }

        public void Find(string x){
            string[] param = x.Split();
            if ((Name == param[0] || "" == param[0]) && (Surname == param[1] || "" == param[1]) && (Gender == param[2] || "" == param[2]) && (Age == Convert.ToInt32(param[3]) || "" == param[3])){
                Print();
            }
            else
                Console.WriteLine("Персонал не найден.");
        }

        virtual public void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class Employee:Person
    {
        public Employee(string NameInput, string SurnameInput, string GenderInput, int AgeInput):base(NameInput, SurnameInput, GenderInput, AgeInput){}
        override public void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class Worker:Person
    {
        public Worker(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class Enginer:Person
    {
        public Enginer(string name_imput, string surname_input, string Gender_input, int age_input) : base(name_imput, surname_input, Gender_input, age_input) { }
        override public void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
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
        static int ColElementCollection(string type, ArrayList PersonGroup){
            int x = 0;
            for (int i = 0; i < PersonGroup.Count; i++){
                if (Convert.ToString(PersonGroup[i]) == "Project1."+type)
                    x++;
            }
            return x;
        }
        static void PrintElementColletion(string type, ArrayList PersonGroup){
            switch (type)
            {
                case "Enginer":
                    foreach (Person x in PersonGroup){
                    if (x is Enginer a)
                        a.Print();
                    }
                    break;
                case "Worker":
                    foreach (Person x in PersonGroup){
                    if (x is Worker a)
                        a.Print();
                    }
                    break;
                case "Employee":
                    foreach (Person x in PersonGroup){
                    if (x is Employee a)
                        a.Print();
                    }
                    break;
            }
        }
        static void DelElementCollection(string type, ref ArrayList PersonGroup){
            for (int i = 0; i < PersonGroup.Count; i++){
                if (Convert.ToString(PersonGroup[i]) == "Project1."+type)
                    PersonGroup.RemoveAt(i);
            }
        }
        static void FindElement(int m, ArrayList PersonGroup){
            Console.WriteLine("Не нужные фильтры оставить пустыми.");
            Console.Write("Имя:");
            string name = Console.ReadLine();
            Console.Write("Фамилия:");
            string surname =  Console.ReadLine();
            Console.Write("Пол:");
            string gender = Console.ReadLine();
            Console.Write("Возрасть:");
            string age = Console.ReadLine();
            string param = name + " " + surname + " " + gender + " " + age;
            switch (m){
                case 1:
                    foreach (Person x in PersonGroup){
                        if (x is Enginer e){
                            x.Find(param);         
                        }
                    }
                    break;
                case 2:
                    foreach (Person x in PersonGroup){
                        if (x is Worker e)
                        {
                            x.Find(param);
                        }
                    }
                    break;
                case 3:
                    foreach (Person x in PersonGroup){
                        if (x is Employee e)
                        {
                            x.Find(param);
                        }
                    }
                    break;
                case 4:
                    foreach (Person x in PersonGroup){
                        x.Find(param);
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
            ArrayList PersonGroup = new ArrayList(); 
            PersonGroup.Add( new Employee("Tom", "Helt", "M", 20));
            PersonGroup.Add( new Enginer("Adam", "Lopepr", "M", 23));
            PersonGroup.Add( new Worker("Emver", "Hoeten", "W", 30));
            PersonGroup.Add( new Person("Daive", "Mains", "M", 45));
            bool sorting = false;
            while (true)
            {
                
                foreach (Person x in PersonGroup)
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
                        
                        Menu(m);
                        int m1 = Convert.ToInt32(Console.ReadLine());
                        
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
                                PersonGroup.Add(new Employee(name, surname, gender, age));
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
                                PersonGroup.Add(new Enginer(name, surname, gender, age));
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
                                PersonGroup.Add(new Worker(name, surname, gender, age));
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
                        Console.Write("Введите номер удалаемого эллемента.");
                        int x = Convert.ToInt32(Console.ReadLine());
                        PersonGroup.RemoveAt(x-1);
                        break;
                    case 3:
                        
                        Menu(m);
                        m1 = Convert.ToInt32(Console.ReadLine());
                        
                        switch (m1){
                            case 1:
                                Console.WriteLine("Вид элемента:");
                                Console.WriteLine("1. Инженер");
                                Console.WriteLine("2. Рабочий");
                                Console.WriteLine("3. Служащий");
                                int a = Convert.ToInt32(Console.ReadLine());
                                string type = "";
                                if (a == 1){
                                    type = "Enginer";}
                                else if (a == 2)
                                    {type = "Worker";}
                                else if (a == 3)
                                    {type = "Employee";}
                                else 
                                    {Console.WriteLine("Неверный параметр");
                                    break;}
                                Console.WriteLine("Количество элементов {0}:{1}",type, ColElementCollection(type, PersonGroup));
                                break;
                            case 2:
                                Console.WriteLine("Вид элемента:");
                                Console.WriteLine("1. Инженер");
                                Console.WriteLine("2. Рабочий");
                                Console.WriteLine("3. Служащий");
                                a = Convert.ToInt32(Console.ReadLine());
                                type = "";
                                if (a == 1){
                                    type = "Enginer";}
                                else if (a == 2)
                                    {type = "Worker";}
                                else if (a == 3)
                                    {type = "Employee";}
                                else 
                                    Console.WriteLine("Неверный параметр");
                                PrintElementColletion(type, PersonGroup);
                                break;
                            case 3:
                                Console.WriteLine("Вид элемента:");
                                Console.WriteLine("1. Инженер");
                                Console.WriteLine("2. Рабочий");
                                Console.WriteLine("3. Служащий");
                                a = Convert.ToInt32(Console.ReadLine());
                                type = "";
                                if (a == 1){
                                    type = "Enginer";}
                                else if (a == 2)
                                    {type = "Worker";}
                                else if (a == 3)
                                    {type = "Employee";}
                                else 
                                    Console.WriteLine("Неверный параметр");
                                DelElementCollection(type, ref PersonGroup);
                                break;
                            default:
                                Console.WriteLine("Неверный параметр");
                                break;
                        }
                        break;
                    case 4:
                        ArrayList ClonePersonGroup = new ArrayList(PersonGroup);
                        break;
                    case 5:
                        if (sorting == false){
                            PersonGroup.Sort();
                            
                            Console.WriteLine("Коллекция успешно отсортрованна.");
                        }
                        else
                            Console.WriteLine("Коллекция уже отсортированна.");
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