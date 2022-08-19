using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count_methods_laba_1
{
    class SysOfEquations
    {
        private int size;
        private double[,] matrix;
        private double MainDeterminant;

        private double FindTheDeterminant(double[,] matrix)
        {
            double[,] cashmatrix;
            double determinant;
            if (size == 1)
            {
                determinant = matrix[0, 0];
            }
            else if (size == 2)
            {
                determinant = matrix[0, 0] * matrix[1, 1] - (matrix[1, 0] * matrix[0, 1]);
            }
            else if (size == 3)
            {
                determinant = matrix[0, 0] * matrix[1, 1] * matrix[2, 2] + (matrix[0, 2] * matrix[1, 0] * matrix[2, 1]) + (matrix[0, 1] * matrix[1, 2] * matrix[2, 0]) - (matrix[2, 0] * matrix[1, 1] * matrix[0, 2]) - (matrix[1, 0] * matrix[0, 1] * matrix[2, 2]) - (matrix[0, 0] * matrix[2, 1] * matrix[1, 2]);
            }
            else
            {
                cashmatrix = TriangleView(matrix);
                determinant = 1;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j)
                        {
                            determinant *= cashmatrix[i, j];
                        }
                    }
                }
            }

            if (determinant == 0)
            {
                throw new IndexOutOfRangeException("Determinant should be not equal 0");
            }
            return determinant;
        }

        private void FillArray()
        {
            Console.WriteLine("Enter the elements of matrix");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine("Enter [" + (i + 1) + "]" + "[" + (j + 1) + "]" + " element");
                    matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("Enter the right side of the system of equations");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Enter the first element of last column");
                matrix[i, size] = Convert.ToInt32(Console.ReadLine());
            }
        }

        private void ShowArray(double[,] matrix)
        {
            Console.WriteLine("===========================================");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j] + "x" + (j + 1) + " ");
                }
                Console.Write("= " + matrix[i, size]);
                Console.WriteLine();
            }
            Console.WriteLine("===========================================");
        }

        private void ShowArrayDelta(double[,] matrix)
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

        private void ShowAllMatrix(double[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    if (j == size)
                    {
                        Console.Write("      " + matrix[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        private double[,] TriangleView(double[,] matrix)
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
                            for (int l = i; l <= size; l++)
                            {
                                matrix[k, l] = matrix[k, l] - (matrix[i, l] * delitel);
                            }

                        }
                    }
                }
            }
            return matrix;
        }

        public void Kramer()
        {
            Console.WriteLine("()()()()()()    Kramer method    ()()()()()()");
            double[,] CashMatrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        CashMatrix[j, k] = matrix[j, k];
                        if (k == i)
                        {
                            CashMatrix[j, k] = matrix[j, size];
                        }
                    }
                }
                Console.WriteLine("=================Delta" + (i + 1) + "===================" + "\n");
                ShowArrayDelta(CashMatrix);
                Console.WriteLine("\n" + "\n" + "X" + (i + 1) + " = " + FindTheDeterminant(CashMatrix) / MainDeterminant);

            }

        }

        public void Gauss()
        {
            Console.WriteLine("\n" + "()()()()()()    Gauss method    ()()()()()()");
            double[,] CashMatrix = new double[size, size + 1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    CashMatrix[i, j] = matrix[i, j];
                }
            }

            Console.WriteLine("\n" + "============= thriangle view ============");

            ShowAllMatrix(AntiLongResult(TriangleView(CashMatrix)));

            double[] results = new double[size]; // обратный ход
            double subtrahend = 0;

            results[size - 1] = CashMatrix[size - 1, size] / CashMatrix[size - 1, size - 1];
            int u = 1;

            for (int i = size - 2; i >= 0; i--)
            {
                for (int j = size - 1; j > i; j--)
                {
                    subtrahend += CashMatrix[i, j] * results[size - u];
                    u++;
                }
                results[i] = (CashMatrix[i, size] - subtrahend) / CashMatrix[i, i];
                subtrahend = 0;
                u = 1;
            }

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("X" + (i + 1) + ": " + results[i] + "\n");
            }
        }

        public void Gauss_Jordan()
        {
            Console.WriteLine("\n" + "()()()()()()    Gauss-Jordan method    ()()()()()()");
            double[,] CashMatrix = TriangleView(matrix);
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
                            CashMatrix[k - 1, size] = CashMatrix[k - 1, size] + CashMatrix[i, size] * delitel;
                        }
                    }
                }
            }

            ShowAllMatrix(AntiLongResult(CashMatrix));

            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("X" + (i + 1) + ": " + CashMatrix[i, size] / CashMatrix[i, i] + "\n");
            }
        }


        public SysOfEquations(double[,] matrix, int size)
        {
            this.size = size;
            this.matrix = matrix;
            FillArray();
            ShowArray(matrix); //test
            MainDeterminant = FindTheDeterminant(matrix);
            Kramer();
            Gauss();
            Gauss_Jordan();
            Console.ReadKey();
        }

        private double[,] AntiLongResult(double[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    matrix[i, j] = Math.Round(matrix[i, j], 3);
                }
            }
            return matrix;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            int size;
            Console.WriteLine("Enter the size of matrix");
            size = Convert.ToInt32(Console.ReadLine());
            double[,] matrix = new double[size, size + 1];
            SysOfEquations sysofequations = new SysOfEquations(matrix, size);

        }
    }
}