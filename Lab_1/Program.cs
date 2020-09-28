using System;
using System.Collections.Generic;

namespace Lab_1
{
    class Program
    {
        public class Quadrangle
        {
            static int num = 0;
            private int _id;
            private double[] _coords;
            private Services.Type _type;
            public Quadrangle(Services.Type type, double[] coords) // Конструктор класса
            {
                _id = num;
                _type = type;
                _coords = coords;
                num++;
            }

            public void view()
            {
                Console.WriteLine($"\nObject № {_id}");
                Console.WriteLine($"{_type}");
                Console.WriteLine($"A({_coords[0]:0.##};{_coords[1]:0.##}) B({_coords[2]:0.##};{_coords[3]:0.##}) C({_coords[4]:0.##};{_coords[5]:0.##}) D({_coords[6]:0.##};{_coords[7]:0.##})");
            }

            public static void add()
            {
                double[] coords = Services.get_coords();
                Services.Type type = Services.define_type(coords);
                if (type == Services.Type.Undefinded)
                {
                    Console.WriteLine("It's not a square or rectangle, try again..");
                    return;
                }
                Quadrangle obj = new Quadrangle(type, coords);
                quadrangles.Add(obj);
                Console.WriteLine("Successfuly added new element:");
                obj.view();
            }

            public void move()
            {
                Console.WriteLine("\n Specify the displacement vector {a,b}: ");
                Console.Write("Coord a: "); double a = Convert.ToDouble(Console.ReadLine());
                Console.Write("Coord b: "); double b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                for (int i = 0; i < 8; i += 2)
                {
                    _coords[i] += a;
                    _coords[i+1] += b;
                }
                Console.WriteLine("After:"); view();
            }

            public void resize()
            {
                Console.Write("\n Specify the value: "); double value = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                _coords[0] -= value; _coords[4] -= value; _coords[5] -= value; _coords[7] -= value;
                _coords[1] += value; _coords[2] += value; _coords[3] += value; _coords[6] += value;
                Console.WriteLine("After:"); view();
            }

            public void rotate()
            {
                double[] E = new double[2];
                double angle;
                Console.WriteLine("\nSpecify pivot point E (x,y).");
                Console.Write("X: "); E[0] = Convert.ToDouble(Console.ReadLine());
                Console.Write("Y: "); E[1] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Angle in degrees: "); angle = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                double[] new_coords = new double[8];
                for (int i = 0; i < 8; i += 2)
                {
                    double[] A = new double[2] { _coords[i], _coords[i+1]};
                    A = Services.rotate_point(A, E, angle);
                    _coords[i] = A[0]; _coords[i + 1] = A[1];
                }
                Console.WriteLine("After:"); view();
            }

            public static bool intersection(int id1, int id2)
            {
                double[][] quadrangle_1 = new double[4][] {
                    new double[2] { quadrangles[id1]._coords[0], quadrangles[id1]._coords[1] },
                    new double[2] { quadrangles[id1]._coords[2], quadrangles[id1]._coords[3] },
                    new double[2] { quadrangles[id1]._coords[4], quadrangles[id1]._coords[5] },
                    new double[2] { quadrangles[id1]._coords[6], quadrangles[id1]._coords[7] }
                };
                double[][] quadrangle_2 = new double[4][] {
                    new double[2] { quadrangles[id2]._coords[0], quadrangles[id2]._coords[1] },
                    new double[2] { quadrangles[id2]._coords[2], quadrangles[id2]._coords[3] },
                    new double[2] { quadrangles[id2]._coords[4], quadrangles[id2]._coords[5] },
                    new double[2] { quadrangles[id2]._coords[6], quadrangles[id2]._coords[7] }
                };

                double[] A = new double[2];
                double[] B = new double[2];
                double[] U = new double[2];
                double Y, Q;
                for (int i = 0; i < 4; i++)
                {
                    A[0] = quadrangle_1[i][0];
                    A[1] = quadrangle_1[i][1];
                    B[0] = quadrangle_1[(i + 1) % 4][0];
                    B[1] = quadrangle_1[(i + 1) % 4][1];
                    U[0] = quadrangle_1[(i + 2) % 4][0];
                    U[1] = quadrangle_1[(i + 2) % 4][1];
                    double const1 = (B[1] - A[1]);
                    double const2 = (B[0] - A[0]);
                    bool is_axis = true;
                    Y = (U[0] - A[0]) * const1 - (U[1] - A[1]) * const2;
                    for (int j = 0; j < 4; j++)
                    {
                        Q = (quadrangle_2[j][0] - A[0]) * const1 - (quadrangle_2[j][1] - A[1]) * const2;
                        if ((Y > 0 && Q > 0) || (Y < 0 && Q < 0))
                        {
                            is_axis = false;
                            break;
                        }
                    }
                    if (is_axis) return false;
                }
                return true;
            }
        }


        public class Circle
        {
            static int num = 0;
            private int _id;
            private double _x, _y, _R;
            public Circle(double x, double y, double R) // Конструктор класса
            {
                _x = x; _y = y; _R = R; _id = num;
                num++;
            }

            public void view()
            {
                Console.WriteLine($"\nObject № {_id}");
                Console.WriteLine($"Center (X,Y) - ({_x}:0.##;{_y:0.##})\nRadius - {_R}");
            }

            public static void add()
            {
                double x, y, R;
                Console.WriteLine("\nSpecify the Center coordinates:");
                Console.Write("Print X"); x = Convert.ToDouble(Console.ReadLine());
                Console.Write("Print Y"); y = Convert.ToDouble(Console.ReadLine());
                Console.Write("Print Radius"); R = Convert.ToDouble(Console.ReadLine());
                Circle obj2 = new Circle(x, y, R);
                circles.Add(obj2);
                obj2.view();
            }

            public void move()
            {
                Console.WriteLine("\n Specify the displacement vector {a,b}: ");
                Console.Write("Coord a: "); double a = Convert.ToDouble(Console.ReadLine());
                Console.Write("Coord b: "); double b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                _x += a; _y += b;
                Console.WriteLine("After:"); view();
            }

            public void resize()
            {         
                Console.Write("\n Specify the value: "); double value = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                _R += value;
                Console.WriteLine("After:"); view();
            }

            public void rotate()
            {
                double[] E = new double[2];
                double angle;
                Console.WriteLine("\nSpecify pivot point E (x,y).");
                Console.Write("X: "); E[0] = Convert.ToDouble(Console.ReadLine());
                Console.Write("Y: "); E[1] = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Angle in degrees: "); angle = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Before:"); view();
                double[] A = new double[2] { _x, _y };
                A = Services.rotate_point(A, E, angle);
                _x = A[0]; _y = A[1];
                Console.WriteLine("After:"); view();
            }

            public static bool intersection(int id1, int id2)
            {
                double[] OO = new double[2] { circles[id2]._x - circles[id1]._x, circles[id2]._y - circles[id1]._y };
                double d = Services.get_len_of_vector(OO);
                if (d < circles[id1]._R + circles[id2]._R) return true; else return false;
            }
        }


        public static List<Quadrangle> quadrangles = new List<Quadrangle>();
        public static List<Circle> circles = new List<Circle>();


        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            Debug.do_();
            Interface.Menu();
        }

    }
}


