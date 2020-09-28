using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_1
{
    class Debug
    {
        public static void do_()
        {
            double[] coords = { 1, 5, 4, 5, 1, 2, 4, 2 };
            Program.Quadrangle obj = new Program.Quadrangle(Services.Type.Square, coords);
            Program.quadrangles.Add(obj);
            /*double[] coords2 = { 1, 2, 2, 2, 1, 1, 2, 1 };
            Program.Quadrangle obj2 = new Program.Quadrangle(Services.Type.Rectangle, coords2);
            Program.quadrangles.Add(obj2);*/
            double[] coords3 = { 0, 0, 2, 0, 0, -2, 2, -2 };
            Program.Quadrangle obj3 = new Program.Quadrangle(Services.Type.Rectangle, coords3);
            Program.quadrangles.Add(obj3);
            double[] coords4 = { 0, 3, 2, 3, 0, -2, 2, -2 };
            Program.Quadrangle obj4 = new Program.Quadrangle(Services.Type.Rectangle, coords4);
            Program.quadrangles.Add(obj4);

            /*Console.WriteLine(Program.Quadrangle.intersection(0, 1));
            Console.WriteLine(Program.Quadrangle.intersection(1, 2));*/
        }
    }
}
