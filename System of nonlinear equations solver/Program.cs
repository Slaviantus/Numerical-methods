using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SNAU
{
    class SNAU
    {

        // private double x0 = 1.4;
        private double x0 = 0.7;
       // private double y1_0 = 2.198642079;
        //private double y2_0 = -1.5;
        private double y2_0 = 0.6;
        private double eps = 0.001; // точность эпсилон

        private int size = 2;
        private double[,] yakobiMatrix;// матрица Якоби
        private double[,] X;
        private double[,] newX;

        public SNAU()
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~  Simple iteration method  ~~~~~~~~~~~~~~~~~~~~");
            IterativeMethod();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~  Newton method  ~~~~~~~~~~~~~~~~~~~~");
            NewtonMethod();
        }


        /***************************************  Методы для метода Простых итераций  ***************************************/


        /*~~~~~~~~~~~~~~  1-е уравнение системы выраженное для метода простых итераций  ~~~~~~~~~~~~~~*/

        private double FirstEquation(double x, double y)
        {
            return Math.Sin(x + 2) - 1.5;

        }


        /*~~~~~~~~~~~~~~  2-е уравнение системы выраженное для метода простых итераций  ~~~~~~~~~~~~~~*/

        private double SecondEquation(double x, double y)
        {
            return -1 * Math.Cos(y - 2) + 0.5;

        }


        /*~~~~~~~~~~~~~~  Частная производная выраженная dF1/dx  ~~~~~~~~~~~~~~*/

        private double VdF1_DX(double x, double y)
        {
            return Math.Cos(x + 2);

        }


        /*~~~~~~~~~~~~~~  Частная производная выраженная dF1/dy  ~~~~~~~~~~~~~~*/

        private double VdF1_DY(double x, double y)
        {
            return 0;
        }


        /*~~~~~~~~~~~~~~  Частная производная выраженная dF2/dx  ~~~~~~~~~~~~~~*/

        private double VdF2_DX(double x, double y)
        {
            return 0;
        }


        /*~~~~~~~~~~~~~~  Частная производная выраженная dF2/dy  ~~~~~~~~~~~~~~*/

        private double VdF2_DY(double x, double y)
        {
            return Math.Sin(y - 2);
        }


        /*~~~~~~~~~~~~~~  Проверка на сходимость  ~~~~~~~~~~~~~~*/

        private bool Convergence(double x, double y)
        {

            if (Math.Abs((VdF1_DX(x, y) + VdF2_DX(x, y))) <= 1 && (Math.Abs((VdF1_DY(x, y) + VdF2_DY(x, y))) <= 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /*~~~~~~~~~~~~~~  Метод простых итераций  ~~~~~~~~~~~~~~*/

        private void IterativeMethod()
        {
            double x = x0, y = y2_0;
            double nextX, nextY;

            bool exit = false;

            int itr = 0;

            if (Convergence(x, y))
            {
                while (!exit)
                {
                    nextY = FirstEquation(x, y);
                    nextX = SecondEquation(x, y);

                    Console.WriteLine("X:  " + nextX + "     Y:   " + nextY);

                    itr++;

                    if ((Math.Abs(x - nextX) <= eps) && (Math.Abs(y - nextY) <= eps))
                    {
                        exit = true;
                    }

                    x = nextX;
                    y = nextY;
                }

                Console.WriteLine();
                Console.WriteLine("Sum of iterations: " + itr);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The system does not satisfy the convergence condition");
            }

        }



        /***************************************  Методы для метода Ньютона  ***************************************/


        /*~~~~~~~~~~~~~~  Частная производная dF1/dx  ~~~~~~~~~~~~~~*/

        private double dF1_DX(double x, double y)
        {
            return Math.Cos(x + y) - 1.2;
        }


        /*~~~~~~~~~~~~~~  Частная производная dF1/dy  ~~~~~~~~~~~~~~*/

        private double dF1_DY(double x, double y)
        {
            return Math.Cos(x + y);
        }


        /*~~~~~~~~~~~~~~  Частная производная dF2/dx  ~~~~~~~~~~~~~~*/

        private double dF2_DX(double x, double y)
        {
            return 2 * x;
        }


        /*~~~~~~~~~~~~~~  Частная производная dF2/dy  ~~~~~~~~~~~~~~*/

        private double dF2_DY(double x, double y)
        {
            return 2 * y;
        }


        /*~~~~~~~~~~~~~~  Первое уравнение системы для метода Ньютона  ~~~~~~~~~~~~~~*/

        private double FirstFunction(double x, double y)
        {
            return Math.Sin(x + y) - 1.2 * x - 0.1;
        }


        /*~~~~~~~~~~~~~~  Второе уравнение системы для метода Ньютона  ~~~~~~~~~~~~~~*/

        private double SecondFunction(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2) - 1;
        }


        /*~~~~~~~~~~~~~~  Приведение матрицы к треугольному виду  ~~~~~~~~~~~~~~*/

        private double[,] TriangleView(double[,] matrix, int size)
        {
            double delitel;
            for (int i = 0; i < size; i++)
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


        /*~~~~~~~~~~~~~~  Метод Гаусса-Жордана для нахождения обратной матрицы   ~~~~~~~~~~~~~~*/

        private double[,] Gauss_Jordan(double[,] matrix, int size)
        {
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
                delitel = CashMatrix[i, i];
                for (int j = 0; j < size * 2; j++)
                {
                    CashMatrix[i, j] = CashMatrix[i, j] / delitel;
                }
            }

            return CashMatrix;
        }


        /*~~~~~~~~~~~~~~  Проверка на наличие нулевых диагональных элементов  ~~~~~~~~~~~~~~*/

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


        /*~~~~~~~~~~~~~~  Нахождение обратной матрицы  ~~~~~~~~~~~~~~*/

        private double[,] InvertedMatrix(double[,] matrix, int size)
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
                extendedMatrix = Gauss_Jordan(extendedMatrix, size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = extendedMatrix[i, j + size];
                    }
                }
            }
            else
            {
                Console.WriteLine("Unable to calculate inverse matrix with Jordan Gauss method");
            }
            return matrix;
        }


        /*~~~~~~~~~~~~~~  Умножение матриц  ~~~~~~~~~~~~~~*/

        private double[,] MultiplyMatrix(double[,] matrix1, double[,] matrix2, int rows1, int cols1, int rows2, int cols2)
        {
            double[,] resultMatrix = new double[rows2, cols2];

            for (int i = 0; i < rows1; i++)
            {
                double[] rowMatrix1 = new double[cols1];
                for (int l = 0; l < cols1; l++)
                {
                    rowMatrix1[l] = matrix1[i, l];
                }
                for (int p = 0; p < cols2; p++)
                {
                    resultMatrix[i, p] = 0;
                    for (int q = 0; q < rows2; q++)
                    {
                        resultMatrix[i, p] += rowMatrix1[q] * matrix2[q, p];
                    }
                }
            }
            return resultMatrix;
        }


        /*~~~~~~~~~~~~~~  Вычитание матриц  ~~~~~~~~~~~~~~*/

        private double[,] SubtractionMatrix(double[,] matrix1, double[,] matrix2, int size)
        {
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return result;
        }


        /*~~~~~~~~~~~~~~  Присваивание матриц  ~~~~~~~~~~~~~~*/

        static void Assignment(double[,] matrix1, double[,] matrix2, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix1[i, j] = matrix2[i, j];
                }
            }
        }


        /*~~~~~~~~~~~~~~  Составление обратной матрицы Якоби  ~~~~~~~~~~~~~~*/

        private double[,] InvertedYakobi(double[,] matrix)
        {

            yakobiMatrix[0, 0] = dF1_DX(matrix[0, 0], matrix[1, 0]);
            yakobiMatrix[0, 1] = dF1_DY(matrix[0, 0], matrix[1, 0]);
            yakobiMatrix[1, 0] = dF2_DX(matrix[0, 0], matrix[1, 0]);
            yakobiMatrix[1, 1] = dF2_DY(matrix[0, 0], matrix[1, 0]);

            return InvertedMatrix(yakobiMatrix, size);
        }


        /*~~~~~~~~~~~~~~  Составление матрицы f (значений уравнений системы)   ~~~~~~~~~~~~~~*/

        private double[,] FunctionMatrix(double[,] matrix)
        {
            double[,] functionMatrix = new double[size, 1];
            functionMatrix[0, 0] = FirstFunction(matrix[0, 0], matrix[1, 0]);
            functionMatrix[1, 0] = SecondFunction(matrix[0, 0], matrix[1, 0]);

            return functionMatrix;
        }


        /*~~~~~~~~~~~~~~  Метод Ньютона  ~~~~~~~~~~~~~~*/

        private void NewtonMethod()
        {

            yakobiMatrix = new double[size, size];
            X = new double[size, 1];
            newX = new double[size, 1];

            int itr = 0;

            X[0, 0] = x0;
            X[1, 0] = y2_0;

            bool exit = false;

            while (!exit)
            {
                Assignment(newX, SubtractionMatrix(X, MultiplyMatrix(InvertedYakobi(X), FunctionMatrix(X), 2, 2, 2, 1), size), size, 1);

                Console.WriteLine("X:  " + newX[0, 0] + "     Y:   " + newX[1, 0]);

                itr++;

                if ((Math.Abs(newX[0, 0] - X[0, 0]) <= eps) && (Math.Abs(newX[1, 0] - X[1, 0]) <= eps))
                {
                    exit = true;
                }

                Assignment(X, newX, size, 1);
            }

            Console.WriteLine();
            Console.WriteLine("Sum of iterations: " + itr);
        }
    }






    class Program
    {
        static void Main(string[] args)
        {
            SNAU equationSystem = new SNAU();
            Console.ReadKey();
        }
    }
}
