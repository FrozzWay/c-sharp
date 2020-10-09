using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_2
{
    class ComplexNumber
    {
        private double _a, _b;
        private double absolute;
        public ComplexNumber(double a, double b) // Конструктор класса
        {
            _a = a;
            _b = b;
            absolute = Math.Sqrt(a * a + b * b);
        }

        public string value() // Вывод комплексного числа на консоль
        {
            double angle1 = 180 / Math.PI * Math.Acos(_a / absolute);
            double angle2 = 180 / Math.PI * Math.Asin(_b / absolute);
            if (angle2 < 0) angle1 *= -1;
            if (_b != 0) return $"\n{_a}+{_b}i\n{absolute:0.##}[cos({angle1:0.##}°)+isin({angle1:0.##}°)]"; else return $"\n{_a}\n{absolute:0.##}[cos({angle1:0.##}°)+isin({angle1:0.##}°)]";
        }

        // Операция сложения двух комплексных чисел
        public static ComplexNumber operator +(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x._a + y._a, x._b + y._b);
        }

        // Операция сложения комплексного числа и вещественного
        public static ComplexNumber operator +(ComplexNumber x, double y)
        {
            return new ComplexNumber(x._a + y, x._b);
        }

        // Операция вычитания двух комплексных чисел
        public static ComplexNumber operator -(ComplexNumber x, ComplexNumber y)
        {
            return new ComplexNumber(x._a - y._a, x._b - y._b);
        }

        // Операция вычитания комплексного числа и вещественного
        public static ComplexNumber operator -(ComplexNumber x, double y)
        {
            return new ComplexNumber(x._a - y, x._b);
        }

        // Операция умножения двух комплексных чисел
        public static ComplexNumber operator *(ComplexNumber x, ComplexNumber y)
        {
            double a = x._a * y._a - x._b * y._b;
            double b = x._a * y._b + x._b * y._a;
            return new ComplexNumber(a, b);
        }

        // Операция умножения комплексного числа и вещественного
        public static ComplexNumber operator *(ComplexNumber x, double y)
        {
            return new ComplexNumber(x._a * y, x._b * y);
        }
    }
}
