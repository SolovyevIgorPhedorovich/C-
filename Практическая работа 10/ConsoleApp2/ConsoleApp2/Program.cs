using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public interface IRandomInit
    {
        void RandomInit();
    }

    public class Person : IRandomInit, IComparable, ICloneable
    {
        protected string Name { get { return Name; } set { RandomInit(); } }
        protected string Surname;
        protected string Gender;
        public int Age { get { return Age; } set { RandomInit(); } }

        public Person(string name_imput, string surname_input, string Gender_input, int Age_input)
        {
            Name = name_imput;
            Surname = surname_input;
            Gender = Gender_input;
            Age = Age_input;
        }
        public virtual void RandomInit()
        {
            string[] NameMass = new string[] { "Tom", "Devid", "Maks", "lopper" };
            Random rnd = new Random();
            Age = rnd.Next(20, 50);
            Name = NameMass[rnd.Next(0, NameMass.Length)];
        }

        public virtual void Print()
        {
            Console.WriteLine("Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }

        public int CompareTo(object x)
        {
            Person tempX = (Person)x;
            if (String.Compare(this.Name, tempX.Name) > 0) return 1;
            if (String.Compare(this.Name, tempX.Name) < 0) return -1;
            return 0;

        }
        public Person ShalloCopy()
        {
            return (Person)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new Person("Клон:" + this.Name, this.Surname, this.Gender, this.Age);
        }
    }

    public class employee : Person, IRandomInit
    {
        public employee(string name_imput, string surname_input, string Gender_input, int Age_input) : base(name_imput, surname_input, Gender_input, Age_input) { }
        public void RandomInit()
        {
            Random rnd = new Random();
            Age = rnd.Next(20, 50);
        }
        public override void Print()
        {
            Console.WriteLine("Служащий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }

    public class worker : Person, IRandomInit
    {
        public worker(string name_imput, string surname_input, string Gender_input, int Age_input) : base(name_imput, surname_input, Gender_input, Age_input) { }
        public new void RandomInit()
        {
            Random i = new Random();
            Age = i.Next(20, 60);
        }
        public override void Print()
        {
            Console.WriteLine("Рабочий: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
    }
    public class ingener : Person, IRandomInit, IComparable
    {
        public ingener(string name_imput, string surname_input, string Gender_input, int Age_input) : base(name_imput, surname_input, Gender_input, Age_input) { }
        public override void Print()
        {
            Console.WriteLine("Инженер: Имя: {0}\t Фамилия: {1}\t Пол: {2}\t Возраст: {3}", Name, Surname, Gender, Age);
        }
        public void RandomInit()
        {
            Random i = new Random();
            Age = i.Next(18, 50);
        }
    }

    public class Direcror : IRandomInit, IComparable, ICloneable
    {
        public int Age;
        public string Name;
        public string Surname;
        public void RandomInit()
        {
            string[] n = new string[] { "Tom", "Emeli", "Devid", "Rizy", "Loid" };
            string[] s = new string[] { "Pirty", "Sluding", "Mouve", "Troidin", "Vincert" };
            Random rnd = new Random();
            Age = rnd.Next(30, 70);
            Name = n[rnd.Next(0, 4)];
            Surname = s[rnd.Next(0, 4)];
        }
        public void Print()
        {
            Console.WriteLine("Директор: Имя:{0},\t Фамилия:{1}, \t Возраст:{2}", Name, Surname, Age);
        }
        public int CompareTo(object x)
        {
            if (x is Direcror)
            {
                Direcror tempX = (Direcror)x;
                if (this.Age < tempX.Age) return -1;
                if (this.Age > tempX.Age) return 1;
                else return 0;
            }
            if (x is Person)
            {
                Person tempX = (Person)x;
                if (this.Age < tempX.Age) return -1;
                if (this.Age > tempX.Age) return 1;
                else return 0;
            }
            else return 0;
        }
        public Direcror ShalloCopy()
        {
            return (Direcror)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new Direcror();
        }
    }

    public class SortByAge : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            if (x is Person && y is Person)
            {
                Person o1 = (Person)x;
                Person o2 = (Person)y;
                if (o1.Age < o2.Age) return -1;
                if (o1.Age > o2.Age) return 1;
                else return 0;
            }
            if (x is Direcror && y is Person)
            {
                Direcror o1 = (Direcror)x;
                Person o2 = (Person)y;
                if (o1.Age < o2.Age) return -1;
                if (o1.Age > o2.Age) return 1;
                else return 0;
            }
            if (x is Person && y is Direcror)
            {
                Person o1 = (Person)x;
                Direcror o2 = (Direcror)y;
                if (o1.Age < o2.Age) return -1;
                if (o1.Age > o2.Age) return 1;
                else return 0;
            }
            else return 0;

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IRandomInit[] i = { new employee("Tom", "Helt", "M", 30), new ingener("Devid", "Pload", "M", 20), new ingener("Adam", "Lopepr", "M", 30), new worker("", "dsajkoife", "M", 30), new worker("Emver", "Hoeten", "W", 23) };
            foreach (var v in i)
            {
                v.RandomInit();
                if (v is Person x) x.Print();
                if (v is Direcror d) d.Print();
            }
            Console.WriteLine("Сортировака по имения:");
            Array.Sort(i);
            foreach (var v in i)
            {
                if (v is Person x) x.Print();
                if (v is Direcror d) d.Print();
            }
            Console.WriteLine("Сортировака в возрасту:");
            Array.Sort(i, new SortByAge());
            foreach (var v in i)
            {
                if (v is Person x) x.Print();
                if (v is Direcror d) d.Print();
            }
            Console.WriteLine("Копирование:");
            int l = 0, l1 = 0, leng = 0;
            foreach (var v in i)
            {
                if (v is Person) l++;
                if (v is Direcror) l1++;
            }
            Person[] p = new Person[l];
            Direcror[] di = new Direcror[l1];
            foreach (var v in i)
            {
                if (v is Person) { p[leng] = (Person)v; leng++; }
                else continue;
            }
            Person p1 = new Person("", "", "", 0);
            p1 = p[0];
            p1.Print();

            p1 = p[1].ShalloCopy();
            p1.Print();

            p1 = (Person)p[2].Clone();
            p1.Print();

        }
    }
}

