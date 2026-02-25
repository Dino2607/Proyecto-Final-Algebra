using System;

class Program
{
    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("        SISTEMA MATEMÁTICO EN C#");
            Console.WriteLine("============================================");
            Console.WriteLine("1. Saludo");
            Console.WriteLine("2. Fecha y hora actual");
            Console.WriteLine("3. Multiplicación de matrices 3x3");
            Console.WriteLine("4. Determinante por Sarrus 3x3");
            Console.WriteLine("5. Sistema de ecuaciones 2x2");
            Console.WriteLine("6. Sistemas de ecuaciones 4x4Gauss");
            Console.WriteLine("7. Salir");
            Console.WriteLine("============================================");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Saludar();
                    break;
                case 2:
                    MostrarHora();
                    break;
                case 3:
                    MultiplicarMatrices3x3();
                    break;
                case 4:
                    Sarrus3x3();
                    break;
                case 5:
                    SistemaEcuaciones();
                    break;
                case 6:
                    Gauss4x4();
                    break;
                case 7:
                    Console.WriteLine("\nGracias por usar el sistema.");
                    break;
                default:
                    Console.WriteLine("\nOpción inválida.");
                    break;
            }

            if (opcion != 6)
            {
                Console.WriteLine("\nPresione cualquier tecla para regresar al menú...");
                Console.ReadKey();
            }

        } while (opcion != 6);
    }





    // ================== SALUDO ==================
    static void Saludar()
    {
        Console.Clear();
        Console.Write("Ingrese su nombre: ");
        string nombre = Console.ReadLine();
        Console.WriteLine($"\nBienvenido {nombre}, el sistema está listo para usarse.");
    }

    // ================== FECHA Y HORA ==================
    static void MostrarHora()
    {
        Console.Clear();
        Console.WriteLine("Fecha y hora actual:");
        Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
    }

    // ================== MULTIPLICAR MATRICES ==================


    // ================== MULTIPLICACIÓN MATRICES 3x3 ==================
    static void MultiplicarMatrices3x3()
    {
        Console.Clear();
        Console.WriteLine("MULTIPLICACIÓN DE MATRICES 3x3\n");

        int[,] A = new int[3, 3];
        int[,] B = new int[3, 3];
        int[,] R = new int[3, 3];

        Console.WriteLine("Ingrese los valores de la matriz A:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"A[{i},{j}]: ");
                A[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("\nIngrese los valores de la matriz B:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"B[{i},{j}]: ");
                B[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // Multiplicación
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                R[i, j] = 0;
                for (int k = 0; k < 3; k++)
                {
                    R[i, j] += A[i, k] * B[k, j];
                }
            }
        }

        // Mostrar resultado
        Console.WriteLine("\nMatriz resultado (A x B):");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(R[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
    // ================== SARRUS 3x3 ==================
    static void Sarrus3x3()
    {
        Console.Clear();
        Console.WriteLine("DETERMINANTE POR MÉTODO DE SARRUS (3x3)\n");

        double[,] m = new double[3, 3];

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"m[{i},{j}]: ");
                m[i, j] = double.Parse(Console.ReadLine());
            }

        double determinante =
            m[0, 0] * m[1, 1] * m[2, 2] +
            m[0, 1] * m[1, 2] * m[2, 0] +
            m[0, 2] * m[1, 0] * m[2, 1]
          - m[0, 2] * m[1, 1] * m[2, 0]
          - m[0, 0] * m[1, 2] * m[2, 1]
          - m[0, 1] * m[1, 0] * m[2, 2];

        Console.WriteLine($"\nDeterminante = {determinante}");
    }

    // ================== SISTEMA DE ECUACIONES ==================
    static void SistemaEcuaciones()
    {
        Console.Clear();
        Console.WriteLine("SISTEMA DE ECUACIONES 2x2\n");

        Console.Write("a: ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("b: ");
        double b = double.Parse(Console.ReadLine());
        Console.Write("c: ");
        double c = double.Parse(Console.ReadLine());

        Console.Write("d: ");
        double d = double.Parse(Console.ReadLine());
        Console.Write("e: ");
        double e = double.Parse(Console.ReadLine());
        Console.Write("f: ");
        double f = double.Parse(Console.ReadLine());

        double det = a * e - b * d;
    }
    private static void Gauss4x4()
    {
        Console.Clear();
        int n = 4;
        double[,] matriz = new double[n, n + 1];
        double[] solucion = new double[n];

        Console.WriteLine("SISTEMA DE ECUACIONES 4x4 - MÉTODO DE GAUSS\n");
        Console.WriteLine("Ingrese los coeficientes y términos independientes:\n");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Ecuación {i + 1}:");
            for (int j = 0; j < n + 1; j++)
            {
                Console.Write($"Elemento [{i},{j}]: ");
                matriz[i, j] = double.Parse(Console.ReadLine());
            }
        }

        // Eliminación de Gauss
        for (int k = 0; k < n - 1; k++)
        {
            for (int i = k + 1; i < n; i++)
            {
                double factor = matriz[i, k] / matriz[k, k];

                for (int j = k; j < n + 1; j++)
                {
                    matriz[i, j] -= factor * matriz[k, j];
                }
            }
        }

        // Sustitución hacia atrás
        for (int i = n - 1; i >= 0; i--)
        {
            solucion[i] = matriz[i, n];
            for (int j = i + 1; j < n; j++)
            {
                solucion[i] -= matriz[i, j] * solucion[j];
            }
            solucion[i] /= matriz[i, i];
        }

        // Mostrar resultados
        Console.WriteLine("\nSolución del sistema:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"x{i + 1} = {solucion[i]}");
        }
    }
}