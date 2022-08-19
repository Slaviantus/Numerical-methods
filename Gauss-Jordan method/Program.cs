using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counting_methods_laba_2
{
    class Program
    {
        static double SumNonDiagonalElements(double[,] matrix, int size, int rowNumber)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                if (rowNumber != i)
                {
                    sum += matrix[rowNumber, i];
                }
            }
            return sum;
        }

        static bool Convergence(double[,] matrix, int size)// проверка на сходимость
        {
            bool convergence = true;
            for (int i = 0; i < size; i++)
            {
                if (Math.Abs(matrix[i, i]) <= Math.Abs(SumNonDiagonalElements(matrix, size, i)))
                {
                    convergence = false;
                }
            }
            return convergence;
        }

        static void IterativeMethod(double[,] matrix, int size, double eps)
        {
            double[] oldResults = new double[size];
            double[] results = new double[size];
            bool solved = false;
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                oldResults[i] = matrix[i, size];
            }

            while (!solved)
            {
                count++;
                for (int i = 0; i < size; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < size - 1; j++)
                    {
                        if (i != j)
                        {

                            sum += (matrix[i, j] * oldResults[j]);
                        }
                    }
                    results[i] = (1.0 / matrix[i, i]) * (matrix[i, size] - sum);

                }

                for (int i = 0; i < size; i++)
                {
                    if (Math.Abs(oldResults[i] - results[i]) < eps)
                    {
                        solved = true;
                    }
                }

                for (int i = 0; i < size; i++)
                {
                    oldResults[i] = results[i];
                }

            }

            Console.WriteLine("ххххххххххххх|  Корни уравнения итерационным методом  |ххххххххххххх");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("X" + (i + 1) + " " + results[i]);
            }
            Console.WriteLine("Колличество иттераций: " + count);
        }



        static void ShowAllMatrix(double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


        static void ShowAllExtendedMatrix(double[,] matrix, int size) 
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size * 2; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }






        static void FillArray(double[,] matrix, int size)
        {
            Console.WriteLine("Enter the elements of matrix");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine("Enter [" + (i + 1) + "]" + "[" + (j + 1) + "]" + " element");
                    matrix[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            Console.WriteLine("Enter the right side of the system of equations");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Enter the first element of last column");
                matrix[i, size] = Convert.ToDouble(Console.ReadLine());
            }
        }

        static bool IsDiagonalElementNull(double[,] matrix, int size)
        {
            bool isDiagonalElementNull = false;
            for (int i = 0; i < size; i++)
            {
                if (matrix[i, i] == 0)
                {
                    isDiagonalElementNull = true;
                    break;
                }
            }
            return isDiagonalElementNull;
        }

        static double[,] TriangleView(double[,] matrix, int size)
        {
            double delitel;
            for (int i = 0; i < size; i++) // прямой ход
            {
                for (int j = 0; j < size; j++)
                {
                    if ((i == j) && (i != size - 1))
                    {
                        for (int k = i + 1; k < size; k++)
                        {
                            delitel = matrix[k, j] / matrix[i, j];
                            for (int l = i; l < size * 2; l++)
                            {
                                matrix[k, l] = matrix[k, l] - (matrix[i, l] * delitel);
                            }

                        }
                    }
                }
            }
            return matrix;
        }

        static void Gauss_Jordan(double[,] matrix, int size)
        {
            Console.WriteLine("\n" + "()()()()()()    Gauss-Jordan method    ()()()()()()");
            double[,] CashMatrix = TriangleView(matrix, size);
            double delitel = 0;

            for (int i = size - 1; i >= 0; i--)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        for (int k = i; k > 0; k--)
                        {
                            delitel = -1 * CashMatrix[k - 1, j] / CashMatrix[i, j];
                            CashMatrix[k - 1, j] = CashMatrix[k - 1, j] + CashMatrix[i, j] * delitel;
                            for (int l = size; l < size * 2; l++)
                            {
                                CashMatrix[k - 1, l] = CashMatrix[k - 1, l] + CashMatrix[i, l] * delitel;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size * 2; j++)
                {
                    CashMatrix[i, j] = CashMatrix[i, j] / CashMatrix[i, i];
                }
            }

            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^Result^^^^^^^^^^^^^^^^^^");

            ShowAllExtendedMatrix(AntiLongResultExtended(CashMatrix, size), size);

            Console.WriteLine();
        }

        static double[,] AntiLongResult(double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = Math.Round(matrix[i, j], 3);
                }
            }
            return matrix;
        }

        static double[,] AntiLongResultExtended(double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size * 2; j++)
                {
                    matrix[i, j] = Math.Round(matrix[i, j], 3);
                }
            }
            return matrix;
        }

        static void InvertedMatrix(double[,] matrix, int size)
        {
            if (!IsDiagonalElementNull(matrix, size))
            {
                double[,] extendedMatrix = new double[size, size * 2];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        extendedMatrix[i, j] = matrix[i, j];
                    }

                    for (int j = size; j < size * 2; j++)
                    {
                        if (j == i + size)
                        {
                            extendedMatrix[i, j] = 1.0;
                        }
                        else
                        {
                            extendedMatrix[i, j] = 0;
                        }
                    }
                }
                ShowAllExtendedMatrix(extendedMatrix, size);
                Gauss_Jordan(extendedMatrix, size);
            }
            else
            {
                Console.WriteLine("Невозможно вычислить обратную матрицу с помощью метода Жордана Гаусса");
            }
        }


        static void Main(string[] args)
        {
            int size;
            double eps = 0;


            Console.WriteLine("Enter the size of matrix:");  // пользовательский ввод
            size = Convert.ToInt32(Console.ReadLine());
            double[,] matrix = new double[size, size + 1];

            FillArray(matrix, size);



            ShowAllMatrix(matrix, size);


            if (Convergence(matrix, size))
            {
                IterativeMethod(matrix, size, eps);
            }
            else
            {
                InvertedMatrix(matrix, size);
            }

            Console.ReadKey();
        }
    }
}
