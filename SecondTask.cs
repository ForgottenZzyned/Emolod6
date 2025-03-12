using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Emolod6._1
{
    enum Names
    {
        Ivan = 0,
        Vasulii = 1,
        Denys = 2,
        Artem = 3,
        Nikita = 4,
        Andrew = 5,
        Vlad = 6,
        Stas = 7,
        Bogdan = 8,
        Alex = 9
    }
    abstract class Person
    {
        public int hours { get; set; }
        public Names name { get; set; }
        public double money { get; set; }
        protected int salaryPerHour { get; set; } = 30;
        protected int defaulHourCount { get; set; } = 160;
        public uint id { get; set; }
        public abstract void CalculateSalary(int hours);

    }

    class Professor : Person
    {
        public Professor(Names name)
        {
            this.id = 0;
            this.name = name;
        }
        
        public float coeff { get; set; } = 3f;
        public override void CalculateSalary(int hours)
        {
            float salary = 0;
            while(hours > defaulHourCount)
            {
                hours--;
                salary += salaryPerHour * coeff * 1;
            }
            while(hours > 0)
            {
                hours--;
                salary += salaryPerHour * 5 * 1;
            }
            this.money += Convert.ToDouble(salary) + 2000;
            Console.WriteLine($"This is {this.name}, he is Professor and his salary is {salary}, his hours in this month is {this.hours}");
            this.hours = 0;
        }
    }
    class Student : Person
    {
        public Student(Names name)
        {
            this.id = 1;
            this.name = name;
        }
        public float coeff { get; set; } = 0.5f;
        public override void CalculateSalary(int hours)
        {
            float salary = 0;
            while (hours > defaulHourCount)
            {
                hours--;
                salary += salaryPerHour * coeff * 1;
            }
            while (hours > 0)
            {
                hours--;
                salary += salaryPerHour * 1 * 1;
            }
            Random rand = new Random();
            this.money += Convert.ToDouble(salary) + rand.Next(-700,701);
            Console.WriteLine($"This is {this.name}, he is Student and his salary is {salary}, his hours in this month is {this.hours}");
            this.hours = 0;
        }
    }

    class Staff : Person
    {
        public Staff(Names name)
        {
            this.id = 2;
            this.name = name;
        }
        public float coeff { get; set; } = 1.2f;
        public override void CalculateSalary(int hours)
        {
            float salary = 0;
            while (hours > defaulHourCount)
            {
                hours--;
                salary += salaryPerHour * coeff * 1;
            }
            while (hours > 0)
            {
                hours--;
                salary += salaryPerHour * 2 * 1;
            }
            this.money += Convert.ToDouble(salary);            
            Console.WriteLine($"This is {this.name}, he is Staff and his salary is {salary}, his hours in this month is {this.hours}");
            this.hours = 0;
        }
    }
    internal class SecondTask
    {
        static List<Person> persons = new List<Person>();
        static void Main()
        {
            Random rand = new Random();
            Array enumValues = Enum.GetValues(typeof(Names));
            for(int i = 0; i < 3 ; i++)
            {
                Professor professor = new Professor((Names)enumValues.GetValue(rand.Next(enumValues.Length)));
                persons.Add(professor);
            }
            for (int i = 0; i < 10; i++)
            {
                Student student = new Student((Names)enumValues.GetValue(rand.Next(enumValues.Length)));
                persons.Add(student);
            }
            for (int i = 0; i < 3; i++)
            {
                Staff staff = new Staff((Names)enumValues.GetValue(rand.Next(enumValues.Length)));
                persons.Add(staff);
            }
            DoYearCycle();
        }

        static void DoYearCycle()
        {
            Random rnd = new Random();            
            for (int i = 0; i< 12; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    foreach (Person person in persons)
                    {
                        switch (person.id)
                        {
                            case 0:
                                person.hours += (8 + rnd.Next(-1, 4));
                                break;
                            case 1:
                                person.hours += (8 + rnd.Next(-1, 4));
                                break;
                            case 2:
                                person.hours += (8 + rnd.Next(-1, 4));
                                break;
                        }
                    }
                }
                foreach (Person person in persons)
                {
                    person.CalculateSalary(person.hours);
                }
                Console.WriteLine("Press Enter to start next month");
                Console.ReadLine();
            }
            foreach(Person person in persons)
            {
                Console.WriteLine($"Here is total salary of {person.name} - {person.money}");
            }
            Console.WriteLine("Year completed");
        }
    }
}
