using System;
using System.Linq;

namespace Lab_3
{
    public class Interface
    {
        const string fileName = @"c:\Temp\organization.xml";


        public static void Menu()
        {
            Console.WriteLine("Action list:\n 1. Add employee\n 2. View employees\n 3. [Task2]\n 4. [Task3]\n 5. Write XML\n 6. Read XML");
            switch (Console.ReadLine())
            {
                case "1": Add(); break;
                case "2": View_all(); break;
                case "3": Task2(); break;
                case "4": Task3(); break;
                case "5": Write(); break;
                case "6": Read(); break;
                default: return;
            }
        }

        // Добавление работника
        static void Add()
        {
            Console.WriteLine("\n #### ADDING MENU ####");
            Console.WriteLine("\nAdd:\n 1. HourGuy\n 2. FixedGuy");
            string x = Console.ReadLine();
            Console.WriteLine("\n Input name"); string _name = Console.ReadLine();
            Console.WriteLine("\n Input birthday"); string _birthday = Console.ReadLine();
            Console.WriteLine("\n Input position"); string _position = __position();
            switch (x)
            {
                case "1":
                    Console.WriteLine("\n Input hour stake"); double _hour_stake = Convert.ToDouble((Console.ReadLine()));
                    Program.organization.employees.Add(new Program.HourGuy(_name, _birthday, _position, "HourGuy", _hour_stake));
                    Program.organization.employees.Last().view();
                    Console.WriteLine($"\n Average salary of this organization = {Program.organization.getAverageSalary()}");
                    Menu();
                    break;
                case "2":
                    Console.WriteLine("\n Input fixed stake"); double _fixed_stake = Convert.ToDouble((Console.ReadLine()));
                    Program.organization.employees.Add(new Program.FixedGuy(_name, _birthday, _position, "FixedGuy", _fixed_stake));
                    Program.organization.employees.Last().view();
                    Console.WriteLine($"\n Average salary of this organization = {Program.organization.getAverageSalary()}");
                    Menu();
                    break;
                default:
                    Menu(); return;
            }
        }
  
        static string __position()
        {
            Console.WriteLine("\nChoose position:\n 1. Boss\n 2. NotBoss");
            switch(Console.ReadLine())
            {
                case "1":
                    return "Boss";
                case "2":
                    return "NotBoss";
                default:
                    return __position();
            }
        }


        // Упорядочить последовательность
        static void View_all()
        {
            Sort();
            foreach (Program.Employee obj in Program.organization.employees) obj.view();
            Console.WriteLine($"\nAverage organization salary - {Program.organization.getAverageSalary()}\n");
            Menu();
        }


        static void Sort()
        {
            Program.organization.employees.Sort(delegate (Program.Employee x, Program.Employee y)
            {
                if (x.salary == y.salary)
                    return x.name.CompareTo(y.name);
                return y.salary.CompareTo(x.salary);
            });
        }


        static void Task2()
        {
            Sort(); int i = 0;
            try
            {
                Console.WriteLine("\n");
                while (i < 5)
                {
                    Console.WriteLine(Program.organization.employees[i].name);
                    i++;
                }
                Console.WriteLine("\n");
            }
            catch (Exception ex) { Console.WriteLine("\nLess than 5 employees!\n"); }
            Menu();
        }

        static void Task3()
        {
            Sort();
            try
            {
                int len = Program.organization.employees.Count;
                Console.WriteLine($"\n{ Program.organization.employees[len - 1]._id}, { Program.organization.employees[len-2]._id}, { Program.organization.employees[len-3]._id}\n");
            }
            catch (Exception ex) { Console.WriteLine("\nLess than 3 employees!\n"); }
            Menu();
        }




        // Записать XML
        static void Write()
        { 
            Program.organization.ToXML(fileName);
            Console.WriteLine("\n File has been written!\n");
            Menu();
        }

        // Считать XML
        static void Read()
        {
            try
            {
                Program.organization = Program.Organization.FromXml(fileName);
                Console.WriteLine("\nFile has been read!\n");
            }
            catch (Exception ex) { Console.WriteLine($"\nError: {ex.Message}\n"); }
            Menu();
        }
    }
}
