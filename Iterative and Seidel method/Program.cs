using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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



        static void FillArray(double[,] matrix, double[,] freeMembers, int size) // Filling array
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


   
        static double[,] TriangleView(double[,] matrix, int size) //Transformation matrix to a triangular form
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




        static double[,] Gauss_Jordan(double[,] matrix, int size) // Gauss-Jordan method for calculating the inverse matrix 
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



        static double[,] InvertedMatrix(double[,] matrix, int size) //Inverse Matrix Calculation
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
                Console.WriteLine("Unable to calculate inverse matrix using Jordan Gauss method");
            }
            return matrix;
        }


  
        static void Assignment(double[,] matrix1, double[,] matrix2, int rows, int cols) // Matrix assignment 
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix1[i, j] = matrix2[i, j];
                }
            }
        }



        static double[,] AdditionMatrix(double[,] matrix1, double[,] matrix2, int rows, int cols) // Matrix addition 
        {
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }


        static double[,] SubtractionMatrix(double[,] matrix1, double[,] matrix2, int size) // Matrix subtraction
        {
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }

            return result;
        }



        static double[,] MultiplyMatrix(double[,] matrix1, double[,] matrix2, int rows1, int cols1, int rows2, int cols2) //  Matrix multiplication
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

   
        static bool Convergence(double[,] matrix, int size)  // Convergence check
        {
            if (MatrixNorm(matrix, size) < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

  
        static double MatrixNorm(double[,] matrix, int size)  //Finding the Euclidean Norm of a Matrix
        {
            double norm = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    norm += Math.Pow(matrix[i, j], 2);
                }
            }
            norm = Math.Sqrt(norm);
            return norm;
        }


            static double[,] MatrixEps(double[,] matrix, int size)  //  Creation of matrix E
        {
            double[,] e = new double[size, size];
            double[,] alpha = new double[size, size];
            double value = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    alpha[i, j] = matrix[i, j];
                }
            }

            while (!Convergence(alpha, size))
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        e[i, j] = value;
                    }
                }
                alpha = MultiplyMatrix(e, matrix, size, size, size, size);
                value /= 10;
            }

            return e;
        }



        static double MaxMatrixComparison(double[,] matrix1, double[,] matrix2, int rows)  //Finding the maximum element of the difference of matrix elements
        {
            double max = 0;
            double[] differences = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                differences[i] = Math.Abs(matrix1[i, 0] - matrix2[i, 0]);
            }
            max = differences[0];
            for (int i = 1; i < rows; i++)
            {
                if (differences[i] > max)
                {
                    max = differences[i];
                }
            }
            return max;
        }



        static void IterativeMethod(double[,] A, double[,] B, int sizeA, int rowsB, int colsB, double eps)   //Iterative Method
        {
            var sw1 = new Stopwatch();
            sw1.Start();
            double[,] alpha;
            double[,] beta;
            double[,] d;
            double[,] e = MatrixEps(A, sizeA);
            double[,] xLast = new double[sizeA, 1];
            double[,] xNext = new double[sizeA, 1];

            double maxSub;
            int count = 0;

            Console.WriteLine("Iterative method");
            alpha = MultiplyMatrix(e, A, sizeA, sizeA, sizeA, sizeA);
            d = SubtractionMatrix(InvertedMatrix(A, sizeA), e, sizeA);
            beta = MultiplyMatrix(d, B, sizeA, sizeA, rowsB, colsB);

            Assignment(xLast, beta, sizeA, 1);
            Assignment(xNext, AdditionMatrix(beta, MultiplyMatrix(alpha, xLast, sizeA, sizeA, sizeA, 1), sizeA, 1), sizeA, 1);
            maxSub = MaxMatrixComparison(xLast, xNext, sizeA);
            Assignment(xLast, xNext, sizeA, 1);
            count++;

            while (maxSub > eps)
            {
                Assignment(xNext, AdditionMatrix(beta, MultiplyMatrix(alpha, xLast, sizeA, sizeA, sizeA, 1), sizeA, 1), sizeA, 1);
                maxSub = MaxMatrixComparison(xLast, xNext, sizeA);
                Assignment(xLast, xNext, sizeA, 1);
                count++;
            }
            Show(xNext, sizeA, count);
            
        }



        static void Zeidel(double[,] A, double[,] B, int sizeA, int rowsB, int colsB, double eps)   //Seidel method
        {
            var sw2 = new Stopwatch();
            sw2.Start();
            double[,] alpha;
            double[,] beta;
            double[,] d;
            double[,] e = MatrixEps(A, sizeA);
            double[,] X1 = new double[sizeA, 1];
            double[,] X = new double[sizeA, 1];
            int count = 0;
            Console.WriteLine("Seidel method");
            double maxSub = 1;
            alpha = MultiplyMatrix(e, A, sizeA, sizeA, sizeA, sizeA);
            d = SubtractionMatrix(InvertedMatrix(A, sizeA), e, sizeA);
            beta = MultiplyMatrix(d, B, sizeA, sizeA, rowsB, colsB);
            Assignment(X1, beta, sizeA, 1);
            X[0, 0] = beta[0, 0] + MultiplyMatrix(alpha, X1, sizeA, sizeA, sizeA, 1)[0, 0];
            X1[0, 0] = X[0, 0];
            for (int j = 0; j < 1; j++)
            {

                for (int i = 0; i < sizeA; i++)
                {
                    X[i, 0] = beta[i, 0] + MultiplyMatrix(alpha, X1, sizeA, sizeA, sizeA, 1)[i, 0];
                    X1[i, 0] = X[i, 0];
                }
                count++;
            }
            Show(X, sizeA, count);
            
        }

                 
        static void Show(double[,] x, int rows, int count)  //Output of results
        {
            Console.WriteLine("Result: ");
            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("X" + (i + 1) + " = " + x[i, 0]);
            }
            Console.WriteLine("iterations = " + count);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int size;
            double eps = 0;
            size = 4;
            double[,] matrix = { { 0.05, -0.06, -0.12, 0.14 }, { 0.04, -0.12, 0.08, 0.11 }, { 0.34, 0.08, -0.06, 0.14 }, { 0.11, 0.12, 0, -0.03 } };
            double[,] freeMembers = { { 2.17 }, { -1.4 }, { 2.1 }, { 0.8 } };
            eps = 0.001;
            IterativeMethod(matrix, freeMembers, size, size, 1, eps);
            Zeidel(matrix, freeMembers, size, size, 1, eps);
            Console.ReadKey();
        }
    }
}
