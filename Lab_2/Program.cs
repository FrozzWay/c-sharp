using System;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input first number in complex form z = a + bi:");
            ComplexNumber num1 = newComplexNumber();
            Console.WriteLine("\nInput second number in complex form z = a + bi:");
            ComplexNumber num2 = newComplexNumber();
            Console.WriteLine("\nPrint: \n 1 to sum\n 2 to difference\n 3 to multiply");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine((num1 + num2).value());
                    break;
                case "2":
                    Console.WriteLine((num1 - num2).value());
                    break;
                case "3":
                    Console.WriteLine((num1 * num2).value());
                    break;
            }
        }

        static ComplexNumber newComplexNumber()
        {
            Console.Write("a ( ReZ ) = "); double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b ( ImZ ) = "); double b = Convert.ToDouble(Console.ReadLine());
            return new ComplexNumber(a, b);
        }
        
    }
}
