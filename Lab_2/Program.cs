using System;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        class ComplexNumber
        {
            private double _a, _b;
            public ComplexNumber(double a, double b) // Конструктор класса
            {
                _a = a;
                _b = b;
            }

            public string value() // Вывод комплексного числа на консоль
            {
                if (_b != 0) return $"{_a}+{_b}i"; else return $"{_a}";
            }

            // Операция сложения двух комплексных чисел
            public static ComplexNumber operator + (ComplexNumber x, ComplexNumber y)
            {
                return new ComplexNumber(x._a + y._a, x._b + y._b);
            }

            // Операция сложения комплексного числа и вещественного
            public static ComplexNumber operator + (ComplexNumber x, double y)
            {
                return new ComplexNumber(x._a + y, x._b);
            }

            // Операция вычитания двух комплексных чисел
            public static ComplexNumber operator - (ComplexNumber x, ComplexNumber y)
            {
                return new ComplexNumber(x._a - y._a, x._b - y._b);
            }

            // Операция вычитания комплексного числа и вещественного
            public static ComplexNumber operator - (ComplexNumber x, double y)
            {
                return new ComplexNumber(x._a - y, x._b);
            }

            // Операция умножения двух комплексных чисел
            public static ComplexNumber operator * (ComplexNumber x, ComplexNumber y)
            {
                double a = x._a * y._a - x._b * y._b;
                double b = x._a * y._b + x._b * y._a;
                return new ComplexNumber(a, b);
            }

            // Операция умножения комплексного числа и вещественного
            public static ComplexNumber operator * (ComplexNumber x, double y)
            {
                return new ComplexNumber(x._a * y, x._b * y);
            }
        }
    }
}
