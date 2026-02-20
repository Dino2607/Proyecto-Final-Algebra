using System;
using System.Runtime.InteropServices.Marshalling;

class Program
{
    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("     SISTEMA EN CONSOLA C#");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Saludar");
            Console.WriteLine("2. Sumar dos números");
            Console.WriteLine("3. Mostrar fecha actual");
            Console.WriteLine("4. matriz");
            Console.WriteLine("5  Saliendo del programa");
            Console.WriteLine("=================================");
            Console.Write("Seleccione una opción: ");

            opcion = Convert.ToInt32(Console.ReadLine());
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Saludar();
                    break;


                case 2:
                    Console.Clear();
                    Sumar();
                    break;

                case 3:
                    Console.Clear();
                    MostrarFecha();
                    break;


                case 4:
                    Console.Clear();
                    Console.WriteLine("=================================");
                    Console.WriteLine("   DETERMINANTE POR LAPLACE 4x4");
                    Console.WriteLine("=================================\n");

                    Matrices matrices = new Matrices();
                    int[,] matriz = new int[4, 4];

                    // Captura de la matriz
                    Console.WriteLine("Ingrese los valores de la matriz 4x4:\n");

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write($"Elemento [{i + 1},{j + 1}]: ");
                            matriz[i, j] = Convert.ToInt32(Console.ReadLine());
                        }
                    }

                    // Mostrar matriz ingresada
                    Console.WriteLine("\nMatriz ingresada:\n");
                    matrices.MostrarMatriz(matriz);

                    // Selección de fila
                    Console.Write("\nSeleccione la fila para aplicar Laplace (1 - 4): ");
                    int fila = Convert.ToInt32(Console.ReadLine());

                    // Calcular determinante
                    int determinante = matrices.Laplace(matriz, fila - 1);

                    // Mostrar resultado
                    Console.WriteLine("\n=================================");
                    Console.WriteLine($"Determinante total: {determinante}");
                    Console.WriteLine("=================================");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();

        } while (opcion != 5);
    }

    // ====== MÉTODOS ======

    static void Saludar()
    {
        Console.Write("Ingrese su nombre: ");
        string nombre = Console.ReadLine();
        Console.WriteLine($"\nHola {nombre}, bienvenido al sistema 😎");
    }

    static void Sumar()
    {
        Console.Write("Ingrese el primer número: ");
        int num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Ingrese el segundo número: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        int resultado = num1 + num2;
        Console.WriteLine($"\nLa suma es: {resultado}");
    }

    static void MostrarFecha()
    {
        Console.WriteLine("\nFecha y hora actual:");
        Console.WriteLine(DateTime.Now);
    }





    // --- CLASE CON LOS MÉTODOS ---
    class Matrices
    {
        // Genera una matriz 4x4 con valores ya definidos
        public int[,] Demo()
        {
            int[,] matriz = new int[4, 4];

            matriz[0, 0] = 1; matriz[0, 1] = 2; matriz[0, 2] = 6; matriz[0, 3] = 28;
            matriz[1, 0] = 4; matriz[1, 1] = 5; matriz[1, 2] = 3; matriz[1, 3] = 30;
            matriz[2, 0] = 3; matriz[2, 1] = 1; matriz[2, 2] = 4; matriz[2, 3] = 33;
            matriz[3, 0] = 5; matriz[3, 1] = 4; matriz[3, 2] = 2; matriz[3, 3] = 35;

            return matriz;
        }

        // Imprime la matriz fila por fila
        public void MostrarMatriz(int[,] matriz)
        {
            for (int fila = 0; fila < matriz.GetLength(0); fila++)
            {
                string f = "";
                for (int columna = 0; columna < matriz.GetLength(1); columna++)
                {
                    f += $" {matriz[fila, columna]} \t";
                }
                Console.WriteLine(f);
            }
        }

        // Calcula el determinante de una matriz 3x3 usando la regla de Sarrus
        public int Sarrus3x3(int[,] matriz)
        {
            int d1 = matriz[0, 0] * matriz[1, 1] * matriz[2, 2];
            int d2 = matriz[1, 0] * matriz[2, 1] * matriz[0, 2];
            int d3 = matriz[2, 0] * matriz[0, 1] * matriz[1, 2];

            int i1 = matriz[0, 2] * matriz[1, 1] * matriz[2, 0];
            int i2 = matriz[1, 2] * matriz[2, 1] * matriz[0, 0];
            int i3 = matriz[2, 2] * matriz[0, 1] * matriz[1, 0];

            return (d1 + d2 + d3) - (i1 + i2 + i3);
        }

        // Multiplica todos los valores de un arreglo
        public int Multiplicar(int[] lista)
        {
            int resultado = lista[0];
            for (int i = 1; i < lista.Length; i++)
            {
                resultado *= lista[i];
            }
            return resultado;
        }

        // Crea una submatriz eliminando la fila y columna indicadas
        private int[,] obtenerSubMatriz(int[,] matriz, int filaEliminar, int colEliminar)
        {
            int n = matriz.GetLength(0);
            int[,] submatriz = new int[n - 1, n - 1];

            int r = 0;

            for (int i = 0; i < n; i++)
            {
                if (i == filaEliminar) continue;

                int c = 0;

                for (int j = 0; j < n; j++)
                {
                    if (j == colEliminar) continue;

                    submatriz[r, c] = matriz[i, j];
                    c++;
                }
                r++;
            }

            return submatriz;
        }

        // Calcula el determinante usando expansión por Laplace
        public int Laplace(int[,] matriz, int FilaSeleccionada)
        {
            int resultado = 0;

            for (int columna = 0; columna < matriz.GetLength(1); columna++)
            {
                int numero = matriz[FilaSeleccionada, columna];

                // Obtengo la submatriz eliminando la fila seleccionada y la columna actual
                int[,] submatriz = obtenerSubMatriz(matriz, FilaSeleccionada, columna);

                // Calculo el determinante de la submatriz 3x3
                int menorComplementario = Sarrus3x3(submatriz);

                // Determino el signo según la posición (alternancia + y -)
                int signo = ((FilaSeleccionada + columna) % 2 == 0) ? 1 : -1;

                int adjunto = signo * menorComplementario;

                // Sumo el resultado parcial al total
                resultado += numero * adjunto;

                Console.WriteLine($"Cofactor [{FilaSeleccionada + 1},{columna + 1}]: {numero} * ({signo}) * {menorComplementario} = {numero * adjunto}");
            }

            return resultado;
        }
    }
}