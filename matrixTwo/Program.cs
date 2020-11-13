using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrix;
            Matrix m = new Matrix();
            Vector X;
            Vector F;
            Vector ans;
            Vector res = new Vector();
            double error1 = 0;
            double error2 = 0;
            
            Console.WriteLine("For conditioned matrixes:");
            Console.WriteLine("\tN\t\tGaussian max. error\t\tRotations max. error");
            for (int i = 2; i <= 256; i *= 2)
            {
                matrix = new Matrix(i);
                X = Vector.GenerateVector(i);
                F = matrix * X;
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Gaussian(m, res);
                error1 = Vector.GetMaxInaccuracy(X, ans);
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Rotations(m, res);
                error2 = Vector.GetMaxInaccuracy(X, ans);
                Console.WriteLine("\t" + i + "\t\t" + error1 + "\t\t" + error2);
            }

            Console.WriteLine();
            Console.WriteLine("For ill-conditioned matrixes:");
            Console.WriteLine("\tN\t\tGaussian max. error\t\tRotations max. error");
            for (int i = 2; i <= 14; i += 2)
            {
                matrix = new Matrix(i, false);
                X = Vector.GenerateVector(i);
                F = matrix * X;
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Gaussian(m, res);
                error1 = Vector.GetMaxInaccuracy(X, ans);
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Rotations(m, res);
                error2 = Vector.GetMaxInaccuracy(X, ans);
                Console.WriteLine("\t" + i + "\t\t" + error1 + "\t\t" + error2);
            }

            Console.WriteLine();
            Console.WriteLine("For Gilbert matrixes:");
            Console.WriteLine("\tN\t\tGaussian max. error\t\tRotations max. error");
            for (int i = 2; i <= 14; i += 2)
            {
                matrix = new Matrix(i, false, true);
                X = Vector.GenerateVector(i);
                F = matrix * X;
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Gaussian(m, res);
                error1 = Vector.GetMaxInaccuracy(X, ans);
                m.Copy(matrix);
                res = Vector.Copy(F);
                ans = Solve.Rotations(m, res);
                error2 = Vector.GetMaxInaccuracy(X, ans);
                Console.WriteLine("\t" + i + "\t\t" + error1 + "\t\t" + error2);
            }
        }
    }
}
