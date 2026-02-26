using System;

class Program
{
    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine(" SISTEMA DE MATRICES Y ECUACIONES LINEALES");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. Multiplicación de matrices 3x3");
            Console.WriteLine("2. Determinante por Regla de Sarrus (3x3)");
            Console.WriteLine("3. Determinante por Laplace (3x3)");
            Console.WriteLine("4. Determinante por Laplace (4x4)");
            Console.WriteLine("5. Sistema por Cramer 3x3");
            Console.WriteLine("6. Sistema por Cramer 4x4");
            Console.WriteLine("7. Sistema por Gauss 4x4");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (opcion)
            {
                case 1: MultiplicarMatrices(); break;
                case 2: SarrusPasoAPaso(); break;
                case 3: Laplace3x3(); break;
                case 4: Laplace4x4(); break;
                case 5: Cramer3x3(); break;
                case 6: Cramer4x4(); break;
                case 7: Gauss4x4(); break;
            }

            if (opcion != 8)
            {
                Console.WriteLine("\nPresione una tecla para volver al menú...");
                Console.ReadKey();
            }

        } while (opcion != 8);
    }

    // ================= MULTIPLICACIÓN =================
    static void MultiplicarMatrices()
    {
        int[,] A = LeerMatrizInt();
        int[,] B = LeerMatrizInt();
        int[,] R = new int[3, 3];

        Console.WriteLine("\nProcedimiento:");
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                R[i, j] = 0;
                for (int k = 0; k < 3; k++)
                {
                    Console.WriteLine($"R[{i + 1},{j + 1}] += {A[i, k]} * {B[k, j]}");
                    R[i, j] += A[i, k] * B[k, j];
                }
            }

        Console.WriteLine("\nResultado:");
        MostrarMatriz(R);
    }

    // ================= SARRUS =================
    static void SarrusPasoAPaso()
    {
        double[,] m = LeerMatrizDouble();

        Console.WriteLine("\nRegla de Sarrus:");
        double p1 = m[0, 0] * m[1, 1] * m[2, 2];
        double p2 = m[0, 1] * m[1, 2] * m[2, 0];
        double p3 = m[0, 2] * m[1, 0] * m[2, 1];

        double n1 = m[0, 2] * m[1, 1] * m[2, 0];
        double n2 = m[0, 0] * m[1, 2] * m[2, 1];
        double n3 = m[0, 1] * m[1, 0] * m[2, 2];

        Console.WriteLine($"Positivos: {p1} + {p2} + {p3}");
        Console.WriteLine($"Negativos: {n1} + {n2} + {n3}");

        double det = (p1 + p2 + p3) - (n1 + n2 + n3);
        Console.WriteLine($"Determinante = {det}");
    }

    // ================= LAPLACE 3x3 =================
    static void Laplace3x3()
    {
        double[,] m = LeerMatrizDouble();
        Console.WriteLine("\nExpansión por Laplace fila 1:");

        double det = 0;
        for (int j = 0; j < 3; j++)
        {
            double menor = m[1, (j + 1) % 3] * m[2, (j + 2) % 3] -
                           m[1, (j + 2) % 3] * m[2, (j + 1) % 3];

            double cofactor = Math.Pow(-1, j) * m[0, j] * menor;
            Console.WriteLine($"Cofactor ({0},{j}) = {cofactor}");
            det += cofactor;
        }

        Console.WriteLine($"Determinante = {det}");
    }

    // ================= LAPLACE 4x4 =================
    static void Laplace4x4()
    {
        double[,] m = LeerMatrizDouble(4);
        Console.WriteLine("\nExpansión por Laplace fila 1:");

        double det = 0;
        for (int j = 0; j < 4; j++)
        {
            double[,] sub = SubMatriz(m, 0, j);
            double menor = Determinante3x3(sub);
            double cofactor = Math.Pow(-1, j) * m[0, j] * menor;

            Console.WriteLine($"Cofactor ({0},{j}) = {cofactor}");
            det += cofactor;
        }

        Console.WriteLine($"Determinante = {det}");
    }

    // ================= CRAMER =================
    static void Cramer3x3()
    {
        double[,] A = LeerMatrizDouble();
        double[] B = LeerVector(3);

        double detA = Determinante3x3(A);
        Console.WriteLine($"Det A = {detA}");

        for (int i = 0; i < 3; i++)
        {
            double[,] temp = (double[,])A.Clone();
            for (int j = 0; j < 3; j++)
                temp[j, i] = B[j];

            Console.WriteLine($"x{i + 1} = {Determinante3x3(temp) / detA}");
        }
    }

    static void Cramer4x4()
    {
        double[,] A = LeerMatrizDouble(4);
        double[] B = LeerVector(4);

        double detA = Determinante4x4(A);
        Console.WriteLine($"Det A = {detA}");

        for (int i = 0; i < 4; i++)
        {
            double[,] temp = (double[,])A.Clone();
            for (int j = 0; j < 4; j++)
                temp[j, i] = B[j];

            Console.WriteLine($"x{i + 1} = {Determinante4x4(temp) / detA}");
        }
    }

    // ================= GAUSS =================
    static void Gauss4x4()
    {
        double[,] m = new double[4, 5];
        Console.WriteLine("Ingrese matriz aumentada 4x4:");

        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 5; j++)
                m[i, j] = double.Parse(Console.ReadLine());

        for (int k = 0; k < 3; k++)
        {
            for (int i = k + 1; i < 4; i++)
            {
                double f = m[i, k] / m[k, k];
                Console.WriteLine($"F{i + 1} = F{i + 1} - ({f})F{k + 1}");

                for (int j = k; j < 5; j++)
                    m[i, j] -= f * m[k, j];
            }
        }

        double[] x = new double[4];
        for (int i = 3; i >= 0; i--)
        {
            x[i] = m[i, 4];
            for (int j = i + 1; j < 4; j++)
                x[i] -= m[i, j] * x[j];
            x[i] /= m[i, i];
        }

        Console.WriteLine("\nSolución:");
        for (int i = 0; i < 4; i++)
            Console.WriteLine($"x{i + 1} = {x[i]}");
    }

    // ================= UTILIDADES =================
    static int[,] LeerMatrizInt()
    {
        int[,] m = new int[3, 3];
        Console.WriteLine("Ingrese matriz 3x3:");
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                m[i, j] = int.Parse(Console.ReadLine());
        return m;
    }

    static double[,] LeerMatrizDouble(int n = 3)
    {
        double[,] m = new double[n, n];
        Console.WriteLine($"Ingrese matriz {n}x{n}:");
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                m[i, j] = double.Parse(Console.ReadLine());
        return m;
    }

    static double[] LeerVector(int n)
    {
        double[] v = new double[n];
        Console.WriteLine("Ingrese vector B:");
        for (int i = 0; i < n; i++)
            v[i] = double.Parse(Console.ReadLine());
        return v;
    }

    static void MostrarMatriz(int[,] m)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
                Console.Write(m[i, j] + "\t");
            Console.WriteLine();
        }
    }

    static double Determinante3x3(double[,] m)
    {
        return m[0, 0] * m[1, 1] * m[2, 2] +
               m[0, 1] * m[1, 2] * m[2, 0] +
               m[0, 2] * m[1, 0] * m[2, 1] -
               m[0, 2] * m[1, 1] * m[2, 0] -
               m[0, 0] * m[1, 2] * m[2, 1] -
               m[0, 1] * m[1, 0] * m[2, 2];
    }

    static double Determinante4x4(double[,] m)
    {
        double det = 0;
        for (int j = 0; j < 4; j++)
            det += Math.Pow(-1, j) * m[0, j] * Determinante3x3(SubMatriz(m, 0, j));
        return det;
    }

    static double[,] SubMatriz(double[,] m, int fila, int col)
    {
        double[,] sub = new double[3, 3];
        int r = 0, c;
        for (int i = 0; i < 4; i++)
        {
            if (i == fila) continue;
            c = 0;
            for (int j = 0; j < 4; j++)
            {
                if (j == col) continue;
                sub[r, c++] = m[i, j];
            }
            r++;
        }
        return sub;
    }
}