using System;

namespace Lagrange_interpolation
{
    class Program
    {



        /*~~~~~~~~~~~~  Формула Лагранжа для двух точек  ~~~~~~~~~~~~*/

        static void LagrangeFor2Points(double[] X, double[] Y)
        {
            double x = 0;
            double y = 0;
            double L1 = 0;
            double L2 = 0;
            Console.WriteLine("Полином Лагранжа для 2 точек: L(x) = L1 * " + Y[0] + " + L2 * " + Y[1]);
            Console.WriteLine("Введите координату х");
            x = Convert.ToDouble(Console.ReadLine());
            L1 = (x - X[1]) / (X[0] - X[1]);
            L2 = (x - X[0]) / (X[1] - X[0]);
            Console.WriteLine("L1: " + L1);
            Console.WriteLine("L2: " + L2);
            y = L1 * Y[0] + L2 * Y[1];
            Console.WriteLine("Значение y в точке х по функции Лагранжа: " + y);
            Console.WriteLine("Значение y в точке х по изначальной функции: " + Function(x));
            Console.WriteLine("Разница дальта = " + Math.Abs(y - Function(x)));
        }


        /*~~~~~~~~~~~~  Формула Лагранжа для трёх точек  ~~~~~~~~~~~~*/

        static void LagrangeFor3Points(double[] X, double[] Y)
        {
            double x = 0;
            double y = 0;
            double L1 = 0;
            double L2 = 0;
            double L3 = 0;
            Console.WriteLine("Полином Лагранжа для 3 точек: L(x) = L1 * " + Y[0] + " + L2 * " + Y[1] + " + L3 * " + Y[2]);
            Console.WriteLine("Введите координату х");
            x = Convert.ToDouble(Console.ReadLine());
            L1 = ((x - X[1]) * (x - X[2])) / ((X[0] - X[1]) * (X[0] - X[2]));
            L2 = ((x - X[0]) * (x - X[2])) / ((X[1] - X[0]) * (X[1] - X[2]));
            L3 = ((x - X[0]) * (x - X[1])) / ((X[2] - X[0]) * (X[2] - X[1]));
            Console.WriteLine("L1: " + L1);
            Console.WriteLine("L2: " + L2);
            Console.WriteLine("L3: " + L3);
            y = L1 * Y[0] + L2 * Y[1] + L3 * Y[2];
            Console.WriteLine("Значение y в точке х по функции Лагранжа: " + y);
            Console.WriteLine("Значение y в точке х по изначальной функции: " + Function(x));
            Console.WriteLine("Разница дальта = " + Math.Abs(y - Function(x)));
        }


        /*~~~~~~~~~~~~  Формула Лагранжа для четырёх точек  ~~~~~~~~~~~~*/

        static void LagrangeFor4Points(double[] X, double[] Y)
        {
            double x = 0;
            double y = 0;
            double L1 = 0;
            double L2 = 0;
            double L3 = 0;
            double L4 = 0;
            Console.WriteLine("Полином Лагранжа для 4 точек: L(x) = L1 * " + Y[0] + " + L2 * " + Y[1] + " + L3 * " + Y[2] + " + L4 * " + Y[3]);
            Console.WriteLine("Введите координату х");
            x = Convert.ToDouble(Console.ReadLine());
            L1 = ((x - X[1]) * (x - X[2]) * (x - X[3])) / ((X[0] - X[1]) * (X[0] - X[2]) * (X[0] - X[2]));
            L2 = ((x - X[0]) * (x - X[2]) * (x - X[3])) / ((X[1] - X[0]) * (X[1] - X[2]) * (X[1] - X[2]));
            L3 = ((x - X[0]) * (x - X[1]) * (x - X[3])) / ((X[2] - X[1]) * (X[2] - X[1]) * (X[2] - X[2]));
            L4 = ((x - X[0]) * (x - X[1]) * (x - X[2])) / ((X[3] - X[0]) * (X[3] - X[0]) * (X[3] - X[2]));
            Console.WriteLine("L1: " + L1);
            Console.WriteLine("L2: " + L2);
            Console.WriteLine("L3: " + L3);
            y = L1 * Y[0] + L2 * Y[1] + L3 * Y[2];
            Console.WriteLine("Значение y в точке х по функции Лагранжа: " + y);
            Console.WriteLine("Значение y в точке х по изначальной функции: " + Function(x));
            Console.WriteLine("Разница дальта = " + Math.Abs(y - Function(x)));
        }


        /*~~~~~~~~~~~~  Функция 10 вариант  ~~~~~~~~~~~~*/

        static double Function(double x)
        {
            double y = 0.3 * Math.Pow(0.5, x);
            return y;
        }


        /*~~~~~~~~~~~~  Функция А 10 вариант  ~~~~~~~~~~~~*/

        static double FunctionA(double x)
        {
            double y = 2.1 / (1.8 * x + 1.7);
            return y;
        }


        /*~~~~~~~~~~~~  Ввод узлов интерполяции  ~~~~~~~~~~~~*/

        static void FillArrays(double[] X, double[] Y, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Введите " + (i + 1) + " абциссу");
                X[i] = Convert.ToDouble(Console.ReadLine());
                Y[i] = Function(X[i]);
            }
        }


        static void Main()
        {
            bool work = true;
            int menu = 0;
            double[] X;
            double[] Y;
            while (work)
            {
                Console.WriteLine("()()()()()()   Введите колличество точек   ()()()()()()");
                Console.WriteLine(" (2) ------------------ Полином Лагранжа для двух точек");
                Console.WriteLine(" (3) ------------------ Полином Лагранжа для трёх точек");
                Console.WriteLine(" (4) ------------------ Полином Лагранжа для четырёх точек");
                Console.WriteLine(" любая клавиша -------- Выход");
                menu = Convert.ToInt32(Console.ReadLine());
                switch (menu)
                {
                    case 2:
                        X = new double[2];
                        Y = new double[2];
                        FillArrays(X, Y, 2);
                        LagrangeFor2Points(X, Y);
                        break;
                    case 3:
                        X = new double[3];
                        Y = new double[3];
                        FillArrays(X, Y, 3);
                        LagrangeFor3Points(X, Y);
                        break;
                    case 4:
                        X = new double[4];
                        Y = new double[4];
                        FillArrays(X, Y, 4);
                        LagrangeFor4Points(X, Y);
                        break;
                    default:
                        work = false;
                        break;
                }
            }
        }
    }
}