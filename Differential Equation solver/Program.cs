using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DU
{
    class DifferentialEquation
    {
        private double h = 0.1;           // step
        private double a = 0.2;         // Start of segment
        private double b = 1.2;         // End of segment
        private double y0 = 0.25;       // Initial value of cauchy

        private int sumOfSteps = 0;
        private double[] resultX;
        private double[] resultY;


        public DifferentialEquation()
        {
            sumOfSteps = (int)(Math.Abs(b - a) / h)+1;
            resultX = new double[sumOfSteps];
            resultY = new double[sumOfSteps];
            resultX[0] = a;
            resultY[0] = y0;
            for (int i = 1; i < sumOfSteps; i++)
            {
                resultX[i] = resultX[i - 1] + h;
                Console.WriteLine(resultX[i]);
            }

            Eiler();
            EilerKoshi();
            RungeKutta();
        }


        /*~~~~~~~~~~~~  Изначальная функция (правая часть ДУ)  ~~~~~~~~~~~~*/

         private double Function(double x, double y)
         {
             return 0.127 * (Math.Pow(x, 2.0) + Math.Cos(0.6 * x)) + 0.573 * y;
         }
         

        /*~~~~~~~~~~~~  Euler method  ~~~~~~~~~~~~*/

        private void Eiler()
        {
            Console.WriteLine("**********  Euler method  **********");
            for (int i = 1; i < sumOfSteps; i++)
            {
                resultY[i] = resultY[i - 1] + h * Function(resultX[i - 1], resultY[i - 1]);
            }

            show();
            Console.WriteLine();
        }


        /*~~~~~~~~~~~~  Cauchy-Euler method  ~~~~~~~~~~~~*/

        private void EilerKoshi()
        {
            Console.WriteLine("**********  Cauchy-Euler method  **********");
            for (int i = 1; i < sumOfSteps; i++)
            {
                resultY[i] = resultY[i - 1] + h / 2.0 * (Function(resultX[i - 1], resultY[i - 1]) + Function(resultX[i], resultY[i - 1] + h * Function(resultX[i - 1], resultY[i - 1])));
            }

            show();
            Console.WriteLine();
        }


        /*~~~~~~~~~~~~  Runge-Kutta method  ~~~~~~~~~~~~*/

        private void RungeKutta()
        {
            Console.WriteLine("**********  Runge-Kutta method  **********");

            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;

            for (int i = 1; i < sumOfSteps; i++)
            {
                k1 = Function(resultX[i - 1], resultY[i - 1]);
                k2 = Function(resultX[i - 1] + h / 2.0, resultY[i - 1] + h / 2.0 * k1);
                k3 = Function(resultX[i - 1] + h / 2.0, resultY[i - 1] + h / 2.0 * k2);
                k4 = Function(resultX[i - 1] + h, resultY[i - 1] + h * k3);

                resultY[i] = resultY[i - 1] + h / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4);
            }

            show();
        }


        /*~~~~~~~~~~~~  Data output  ~~~~~~~~~~~~*/

        private void show()
        {
            for (int i = 0; i < sumOfSteps; i++)
            {
                Console.WriteLine(Math.Round(resultY[i], 4));
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            DifferentialEquation equation = new DifferentialEquation();
            Console.ReadKey();
        }
    }
}
