using System;

namespace Lab_1
{
    public class Services
    {
        public enum Type { Undefinded, Square, Rectangle, circle }

        // Запрос координат точек у пользователя.
        public static double[] get_coords()
        {
            Console.WriteLine("Specify coordinates of your rectangle according the next scheme:\n\n                          A B\n                          C D\n");
            Console.WriteLine("Print each of them one by one(A -> B -> C -> D). It'll be 8 records.");
            double[] coords = new double[8];
            for (double i = 0; i < 8; i++)
            {
                string[] points = { "A", "B", "C", "D" };
                string[] o = { "x: ", "y: " };
                Console.Write($"{points[Convert.ToInt32(Math.Floor(i /2))]} {o[Convert.ToInt32(i) % 2]}");
                coords[Convert.ToInt32(i)] = Convert.ToDouble(Console.ReadLine());
            }
            return coords;
        }

        // Определяет тип четырёхугольника.
        public static Type define_type(double[] coords)
        {
            double[][] sides = create_sides(coords);
            if (check_for_rectangle(sides))
                if (check_for_square(sides))
                    return Type.Square;
                else
                    return Type.Rectangle;
            else
                return Type.Undefinded;
        }
        // Находит стороны четырёхугольника по переданным координатам его вершин.
        public static double[][] create_sides(double[] coords)
        {
            double a1, a2, b1, b2, c1, c2, d1, d2;
            a1 = coords[0]; a2 = coords[1];
            b1 = coords[2]; b2 = coords[3];
            c1 = coords[4]; c2 = coords[5];
            d1 = coords[6]; d2 = coords[7];
            double[] AB = { b1 - a1, b2 - a2 };
            double[] AC = { c1 - a1, c2 - a2 };
            double[] CD = { d1 - c1, d2 - c2 };
            double[] DB = { b1 - d1, b2 - d2 };
            double[][] sides = { AB, AC, CD, DB };
            return sides;
        }

        // Проверяет равен ли угол между двумя сторонами 90 градусам.
        public static bool check_angle(double[] v1, double[] v2)
        {
            double v1_len = get_len_of_vector(v1);
            double v2_len = get_len_of_vector(v2);
            double cos = (v1[0] * v2[0] + v1[1] * v2[1] / v1_len * v2_len);
            if (cos == 0) return true; else return false;
        }

        // Проверяет является ли четырёхугольник прямоугольником.
        public static bool check_for_rectangle(double[][] sides)
        {
            bool angle1 = check_angle(sides[0], sides[1]); // AB & AC
            bool angle2 = check_angle(sides[1], sides[2]); // AC & CD
            bool angle3 = check_angle(sides[2], sides[3]); // CD & DB
            if (angle1 && angle2 && angle3) return true; else return false;
        }

        // Получение длины стороны.
        public static double get_len_of_vector(double[] v)
        {
            return Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2));
        }

        // Проверяет является ли четырёхугольник с 3мя прямыми углами квадратом.
        public static bool check_for_square(double[][] sides)
        {
            double AB = get_len_of_vector(sides[0]);
            double AC = get_len_of_vector(sides[1]);
            double CD = get_len_of_vector(sides[2]);
            if (AB == AC && AC == CD) return true; else return false;
        }

        // Поворачивает точку A относительно точки E на угол angle в градусах.
        public static double[] rotate_point(double[] A, double[] E, double angle)
        {
            angle = angle * Math.PI / 180;
            A[0] = A[0] - E[0];
            A[1] = A[1] - E[1];
            A[0] = A[0] * Math.Cos(angle) - A[1] * Math.Sin(angle);
            A[1] = A[0] * Math.Sin(angle) - A[1] * Math.Cos(angle);
            A[0] = A[0] + E[0];
            A[1] = A[1] + E[1];
            return A;
        }
    }
}