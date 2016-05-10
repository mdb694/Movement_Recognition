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

    public static double[,,] smoothing(double[,,] data, int range)
    {
        double[,,] smoothed = new double[data.GetLength(0), data.GetLength(1)-(2*range), data.GetLength(2)];
        for (int i = 0; i < data.GetLength(0); i++)
        {
            int index = 0;
            for (int j = range; j < data.GetLength(1) - range; j++)
            {
                for (int k = 0; k < data.GetLength(2); k++)
                {
                    double sum = 0;
                    for (int s = j - range; s < j + range + 1; s++)
                        sum = sum + data[i, s, k];
                    smoothed[i, index, k] = sum / (2 * range + 1);
                }
                index++;
            }
        }
        return smoothed;
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
