using System;
using System.Collections.Generic;

namespace Lab_3
{
    class Program
    {

        public class Organization
        {
            public double TotalSallary = 0;
            public double AmountOfEmployees = 0;
            public double getAverageSallary(double addSallary) {
                return TotalSallary / AmountOfEmployees;
            }
            public static List<Employee> employees = new List<Employee>();

            public Organization() { }
        };


        // Должность и соответсвующая премия
        static Dictionary<string, double> Position = new Dictionary<string, double>
        {
            ["Boss"] = 100,
            ["NotBoss"] = 10
        };


        // Абстрактный класс Сотрудник
        public abstract class Employee
        {
            static int num = 0;
            public int _id;
            public string name;
            public string birthday;
            public string position;
            public double bonus;
            public double salary;

            public Employee(string _name, string _birthday, string _position)
            {
                _id = num;
                num++;
                name = _name;
                birthday = _birthday;
                position = _position;
                bonus = Position[position];
                salary = countSallary();
            }

            public abstract double countSallary();

        }


        // Класс Сотрудник с почасовой оплатой
        public class HourGuy : Employee
        {
            public double hour_stake;
            public override double countSallary()
            {
                return 20.8 * 8 * hour_stake + bonus;
            }
            
            public HourGuy(string _name, string _birthday, string _position, double _hour_stake)
                : base(_name, _birthday, _position)
            {
                hour_stake = _hour_stake;
            }
        }


        // Класс Сотрудник с фиксированной оплатой
        public class FixedGuy : Employee
        {
            public double fixed_stake;
            public override double countSallary()
            {
                return fixed_stake + bonus;
            }

            public FixedGuy(string _name, string _birthday, string _position, double _fixed_stake)
                : base(_name, _birthday, _position)
            {
                fixed_stake = _fixed_stake;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
