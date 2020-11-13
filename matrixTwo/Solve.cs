using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixTwo
{
    class Solve
    {
        public static Vector Gaussian(Matrix A, Vector F)
        {
            int Size = F.Size;
            Vector X = new Vector(Size);
            double R = 0;
            for (int i = 1; i <= Size; i++)
            {
                try
                {
                    //делаем на диагонали единичный элемент
                    R = 1 / A[i, i];
                    A[i, i] = 1;
                    for (int j = i + 1; j <= Size; j++)
                    {
                        A[i, j] *= R;
                    }
                    F[i] *= R;

                    //вычитаем уравнение из последующих
                    for (int k = i + 1; k <= Size; k++)
                    {
                        R = -A[k, i];
                        A[k, i] = 0;
                        for (int j = i + 1; j <= Size; j++)
                        {
                            A[k, j] += R * A[i, j];
                        }
                        F[k] += R * F[i];
                    }
                }
                catch (Exception e)
                { }
            }

            //получили верхнетреугольную матрицу с единичной диагональю, решим ее
            solveUpperTriangle(A, F);

            //в F теперь находится результат, возвращаем его
            return F;
        }

        //здесь треугольная матрица уже с единицами по диагонали
        public static void solveUpperTriangle(Matrix A, Vector F)
        {
            int Size = F.Size;
            double R;
            //по диагонали уже стоят единицы, поэтому просто обнуляем столбцы над ними
            for (int i = Size; i > 0; i--)
            {
                for (int j = i - 1; j > 0; j--)
                {
                    R = -A[j, i];
                    A[j, i] = 0;
                    F[j] += R * F[i];
                }
            }
        }

        public static Vector Rotations(Matrix A, Vector F)
        {
            int Size = F.Size;
            Vector X = new Vector(Size);
            double R, a, b, c, s, buf;
            for (int i = 1; i <= Size; i++)
            {
                try
                {
                    for (int j = i + 1; j <= Size; j++)
                    {
                        a = A[i, i];
                        b = A[j, i];
                        c = a / Math.Sqrt(a * a + b * b);
                        s = b / Math.Sqrt(a * a + b * b);
                        buf = F[i];
                        F[i] = c * F[i] + s * F[j];
                        F[j] = -s * buf + c * F[j];
                        for (int k = i; k <= Size; k++)
                        {
                            buf = A[i, k];
                            A[i, k] = c * A[i, k] + s * A[j, k];
                            A[j, k] = -s * buf + c * A[j, k];
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }

            //сделаем главную диагональ единичной
            for (int i = 1; i <= Size; i++)
            {
                try
                {
                    R = 1 / A[i, i];
                    A[i, i] = 1;
                    for (int j = i + 1; j <= Size; j++)
                    {
                        A[i, j] *= R;
                    }
                    F[i] *= R;
                }
                catch (Exception e)
                {
                }
            }

            solveUpperTriangle(A, F);

            //в F теперь находится результат, возвращаем его
            return F;
        }
    }
}
