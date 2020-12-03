using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace Lab_3
{
    public class Program
    {

        public class Organization
        {
            public double getAverageSalary() {
                double sum = 0;
                if (employees.Count != 0)
                    foreach (Employee obj in employees)
                        sum += obj.salary;
                return sum / employees.Count;
            }
            public List<Employee> employees = new List<Employee>();

            public Organization() { }

            public void ToXML(string fileName)
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Employee>));
                    XML.Serialize(stream, employees);
                    stream.Flush();
                }
            }

            public static Organization FromXml(string fileName)
            {
                var organization = new Organization();
                using (var stream = File.OpenRead(fileName))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(List<Employee>));
                    var employees = XML.Deserialize(stream) as IEnumerable<Employee>;
                    if (employees != null) organization.employees.AddRange(employees);
                }
                return organization;
            }
        };


        // Должность и соответсвующая премия
        static Dictionary<string, double> Position = new Dictionary<string, double>
        {
            ["Boss"] = 100,
            ["NotBoss"] = 10
        };


        // Абстрактный класс Сотрудник
        [XmlInclude(typeof(HourGuy))]
        [XmlInclude(typeof(FixedGuy))]
        public abstract class Employee
        {
            static int num = 0;
            public int _id;
            public string name;
            public string birthday;
            public string position;
            public double bonus;
            public double salary;
            public string type;

            public Employee(string _name, string _birthday, string _position, string _type)
            {
                _id = num;
                num++;
                name = _name;
                birthday = _birthday;
                position = _position;
                bonus = Position[position];
                type = _type;
            }

            public Employee() { }

            public abstract double countSalary();

            public void view()
            {
                Console.WriteLine($"\nEmployee № {_id}\nName - {name}\nBirthday - {birthday}\nPosition - {position}\nBonus - {bonus}\nType - {type}\nSalary - {salary}\n");
            }

        }


        // Класс Сотрудник с почасовой оплатой
        public class HourGuy : Employee
        {
            public double hour_stake;
            public override double countSalary()
            {
                return 20.8 * 8 * hour_stake + bonus;
            }
            
            public HourGuy(string _name, string _birthday, string _position, string _type, double _hour_stake)
                : base(_name, _birthday, _position, _type)
            {
                hour_stake = _hour_stake;
                salary = countSalary();
            }

            public HourGuy() { }
        }


        // Класс Сотрудник с фиксированной оплатой
        public class FixedGuy : Employee
        {
            public double fixed_stake;
            public override double countSalary()
            {
                return fixed_stake + bonus;
            }

            public FixedGuy(string _name, string _birthday, string _position, string _type, double _fixed_stake)
                : base(_name, _birthday, _position, _type)
            {
                fixed_stake = _fixed_stake;
                salary = countSalary();
            }

            public FixedGuy() { }
        }

        public static Organization organization = new Organization();

        static void Main(string[] args)
        {
            Debug();
            Interface.Menu();
        }

        static void Debug()
        {
            organization.employees.Add(new HourGuy("Alice", "bd", "NotBoss", "HourGuy", 50));
            organization.employees.Add(new HourGuy("Bob", "bd", "NotBoss", "HourGuy", 50));
            organization.employees.Add(new HourGuy("Debra", "bd", "Boss", "HourGuy", 50));
            organization.employees.Add(new FixedGuy("Oleg", "bd", "NotBoss", "FixedGuy", 5000));
            organization.employees.Add(new FixedGuy("Julia", "bd", "NotBoss", "FixedGuy", 9000));
            organization.employees.Add(new FixedGuy("Mark", "bd", "Boss", "FixedGuy", 10000));
        }
    }
}
