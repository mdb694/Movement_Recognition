using System;

public class Funzioni
{
    public static double [,,] modulo(double[,,] data)
    {
        double[,,] mod = new double[data.GetLength(0), data.GetLength(1), 2];
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j<data.GetLength(1); j++)
            {
                for (int k = 0; k < 6; k=k + 3)
                {
                    mod[i, j, k / 3] = Math.Sqrt(Math.Pow(data[i,j,k],2) + 
                                                 Math.Pow(data[i,j,k+1],2) + 
                                                 Math.Pow(data[i,j,k+2],2)); 
                }
            }
        }
        return mod;
    }

    public static void printmultimatrix(double[,,] matrix)
    {
        Console.WriteLine("Matrix print: ");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int k = 0; k < matrix.GetLength(2); k++)
                {
                    Console.Write(matrix[i, j, k] + "; ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
