using System;

namespace Lab_1
{
    class Interface
    {
        public static void Menu()
        {
            Console.WriteLine("\n #### MAIN MENU ####");
            Console.WriteLine("\nPrint:\n 1 to add an object\n 2 to modify object by ID\n 3 to view all objects\n 4 to compare them");
            switch (Console.ReadLine())
            {
                case "1": Add(); break;
                case "2": Modify(); break;
                case "3": View_all(); break;
                case "4": Compare(); break;
                default: return;
            }
        }

        static void Add()
        {
            Console.WriteLine("\n #### ADDING MENU ####");
            Console.WriteLine("\nPrint:\n 1 for square or rectangle\n 2 for cirle\n else to Main Menu.");
            switch (Console.ReadLine())
            {
                case "1":
                    Program.Quadrangle.add();
                    Add();
                    break;
                case "2":
                    Program.Circle.add();
                    Add();
                    break;
                default:
                    Menu(); return;
            }
        }

        static void View_all()
        {
            foreach (Program.Quadrangle obj in Program.quadrangles)    
                obj.view();
            foreach (Program.Circle obj in Program.circles)
                obj.view();
            Menu(); return;
        }

        static void Modify()
        {
            Console.WriteLine("\n #### MODIFYING MENU ####");
            Console.WriteLine("\nPrint:\n 1 for quadrangle\n 2 for circle");
            string type;
            switch (Console.ReadLine())
            {
                case "1":
                    type = "quadrangle"; break;
                case "2":
                    type = "circle"; break;
                default:
                    Menu(); return;       
            }
            Console.WriteLine("\nPrint the ID of object:"); int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nPrint:\n 1 to move\n 2 to resize\n 3 to rotate");
            switch (Console.ReadLine())
            {
                case "1":
                    if (type == "quadrangle") Program.quadrangles[id].move(); else Program.circles[id].move();
                    Menu();
                    break;
                case "2":
                    if (type == "quadrangle") Program.quadrangles[id].resize(); else Program.circles[id].resize();
                    Menu();
                    break;
                case "3":
                    if (type == "quadrangle") Program.quadrangles[id].rotate(); else Program.circles[id].rotate();
                    Menu();
                    break;
                default:
                    Menu(); break;
            }
        }

        static void Compare()
        {
            Console.WriteLine("\n #### COMPARING MENU ####");
            Console.WriteLine("\nPrint:\n 1 for quadrangles\n 2 for circles");
            string type;
            switch (Console.ReadLine())
            {
                case "1":
                    type = "quadrangle"; break;
                case "2":
                    type = "circle"; break;
                default:
                    Menu(); return;
            }
            Console.WriteLine("\nPrint the ID of object №1:"); int id1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nPrint the ID of object №2:"); int id2 = Convert.ToInt32(Console.ReadLine());

            bool intersect;
            if (type == "quadrangle")
                intersect = Program.Quadrangle.intersection(id1, id2);
            else
                intersect = Program.Circle.intersection(id1, id2);

            if (intersect)
                Console.WriteLine("Objects intersect");
            else
                Console.WriteLine("Objects do not intersect.");
            Menu();
        }
    }
}