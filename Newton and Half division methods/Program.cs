using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Counting_methods_laba_3
{
    class Program
    {
        static double Function(double x) 
        {
            double FunctionValue;
            FunctionValue = 2.0 - Math.Log(x) - x;
            return FunctionValue;
        }

        static double FunctionA(double x)
        {
            double FunctionValue;
            FunctionValue = Math.Pow(2.0, Math.Pow(x, 2)) - 1 / x;
            return FunctionValue; ;
        }

        static double DiffFunction(double x)
        {
            double FunctionValue;
            FunctionValue = -1 * 1 / x - 1;
            return FunctionValue;
        }

        static double DiffFunctionA(double x)
        {
            double FunctionValue;
            FunctionValue = Math.Pow(2.0, Math.Pow(x, 2)) * Math.Log(2.0) * 2 * x + 1 / Math.Pow(x, 2);
            return FunctionValue;
        }

        static bool IsBiggerThanZero(double value)
        {
            bool comparing = false;
            if (value > 0)
            {
                comparing = true;
            }
            return comparing;
        }

        static void MethodHalfDivision()
        {
            double eps = 0.001;
            double a = 1.0;
            double b = 2.0;
            double c = 0.0;
            double x = 0.0;
            int i = 0;

            Console.WriteLine("Half division method");
            Console.WriteLine("1-st function");

            while (Math.Abs(a - b) > eps)
            {
                c = a + Math.Abs(a - b) / 2.0;
                if (IsBiggerThanZero(Function(a)) && !IsBiggerThanZero(Function(c)) || !IsBiggerThanZero(Function(a)) && IsBiggerThanZero(Function(c)))
                {
                    b = c;
                }
                else if (IsBiggerThanZero(Function(b)) && !IsBiggerThanZero(Function(c)) || !IsBiggerThanZero(Function(b)) && IsBiggerThanZero(Function(c)))
                {
                    a = c;
                }
                i++;
            }
            x = a + Math.Abs((a - b) / 2.0);
            Console.WriteLine("X = " + x);
            Console.WriteLine("Sum of iterations: " + i);
            Console.WriteLine("=====================================");
            Console.WriteLine("2-nd function");

            a = 0.0;
            b = 1.0;
            c = 0.0;
            i = 0;
            x = 0.0;

            while (Math.Abs(a - b) > eps)
            {
                c = a + Math.Abs(a - b) / 2.0;
                if (IsBiggerThanZero(FunctionA(a)) && !IsBiggerThanZero(FunctionA(c)) || !IsBiggerThanZero(FunctionA(a)) && IsBiggerThanZero(FunctionA(c)))
                {
                    b = c;
                }
                else if (IsBiggerThanZero(FunctionA(b)) && !IsBiggerThanZero(FunctionA(c)) || !IsBiggerThanZero(FunctionA(b)) && IsBiggerThanZero(FunctionA(c)))
                {
                    a = c;
                }
                i++;
            }
            x = a + Math.Abs((a - b) / 2.0);
            Console.WriteLine("X = " + x);
            Console.WriteLine("Sum of iterations: " + i);
        }

        static void NewtonMethod()
        {
            double eps = 0.001;
            bool isEquationSolved = false;
            double x0 = 2.0;
            double x1 = 0.0;
            int i = 0;
            Console.WriteLine("Newton method");
            Console.WriteLine("1-st function");
            while (isEquationSolved == false)
            {
                x1 = x0 - Function(x0) / DiffFunction(x0);
                i++;
                if (Math.Abs(x1 - x0) <= eps)
                {
                    isEquationSolved = true;
                }
                else
                {
                    x0 = x1;
                }
            }

            Console.WriteLine("X = " + x0);
            Console.WriteLine("Sum of iterations: " + i);
            Console.WriteLine("=====================================");
            Console.WriteLine("2-nd function");
            isEquationSolved = false;
            x0 = 1.0;
            x1 = 0.0;
            i = 0;
            while (isEquationSolved == false)
            {
                x1 = x0 - FunctionA(x0) / DiffFunctionA(x0);
                i++;
                if (Math.Abs(x1 - x0) <= eps)
                {
                    isEquationSolved = true;
                }
                else
                {
                    x0 = x1;
                }
            }
            Console.WriteLine("X = " + x0);
            Console.WriteLine("Sum of iterations: " + i);
        }

        static void Main(string[] args)
        {
            MethodHalfDivision();
            Console.WriteLine();
            NewtonMethod();
            Console.ReadKey();
        }
    }
}
