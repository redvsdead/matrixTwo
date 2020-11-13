using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixTwo
{
    class Matrix
    {
        public double[,] matrix;
        public int Size;

        public void Copy(Matrix m)
        {
            Size = m.Size;
            matrix = new double[Size, Size];
            for (int i = 0; i < Size; ++ i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    matrix[i, j] = m.matrix[i, j];
                }
            }
        }

        public Matrix(int _Size = 8, bool good = true, bool gil = false)
        {
            Size = _Size;
            matrix = new double[Size, Size];
            Random rn = new Random();
            if (good)
            {
                for (int i = 1; i <= Size; i++)
                {
                    for (int j = 1; j <= Size; j++)
                    {
                        this[i, j] = rn.NextDouble() * 2 - 1;
                        if (i == j)
                        {
                            this[i, j] += 10;
                        }
                    }
                }
            }
            else
            {             
                
                double R = 10;

                double[,] L = new double[Size, Size];
                double[,] U = new double[Size, Size];
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        L[i, j]= rn.NextDouble() * 2 - 1;
                        U[j, i] = rn.NextDouble() * 2 - 1;
                    }
                }

                for (int i = 0; i < Size; i++)
                {
                    L[i, i] /= R;
                    U[i, i] /= R;
                }

                for (int i = 1; i <= Size; i++)
                {
                    for (int j = 1; j <= Size; j++)
                    {
                        this[i, j] = 0;
                        for (int k = 1; k <= Size; k++)
                        {
                            this[i, j] += L[i - 1, k - 1] * U[k - 1, j - 1];
                        }
                    }
                }
                
            }
            if (gil)
            {
                for (int i = 1; i <= Size; i++)
                {
                    for (int j = 1; j <= Size; j++)
                    {
                        this[i, j] = (double)1 / (i + j - 1);
                    }
                }
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 1 || i > Size)
                {
                    throw new Exception("Error, row index is out if range");
                }
                if (j < 1 || j > Size)
                {
                    throw new Exception("Error, column index is out if range");
                }
                return matrix[i - 1, j - 1];
            }
            set
            {
                if (i < 1 || i > Size)
                {
                    throw new Exception("Error, row index is out if range");
                }
                if (j < 1 || j > Size)
                {
                    throw new Exception("Error, column index is out if range");
                }
                matrix[i - 1, j - 1] = value;
            }
        }
       
        public static Vector operator *(Matrix matrix, Vector vector)
        {
            if (matrix.Size != vector.Size)
            {

                throw new Exception("Error, can not multiply");
            }
            int Size = vector.Size;
            Vector res = new Vector(Size);
            for (int i = 1; i <= Size; ++i)
            {
                res[i] = 0;
                for (int j = 1; j <= Size; j++)
                    res[i] += matrix[i, j] * vector[j];
            }
            return res;
        }

        public static Vector operator *(Vector vector, Matrix matrix)
        {
            return matrix * vector;
        }

        public static Matrix operator *(Matrix matrix, double num)
        {
            int Size = matrix.Size;
            Matrix res = new Matrix(Size);
            for (int i = 1; i <= Size; ++i)
            {
                for (int j = 1; j <= Size; ++j)
                {
                    res[i, j] = matrix[i, j] * num;
                }
            }
            return res;
        }

        public static Matrix operator *(double num, Matrix matrix)
        {
            return matrix * num;
        }

        public void Print()
        {
            for (int i = 1; i <= Size; ++i)
            {
                for (int j = 1; j <= Size; ++j)
                {
                    Console.Write(this[i, j] + "   ");
                }
                Console.WriteLine();
            }
        }
    }
}
