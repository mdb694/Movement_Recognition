using System;

public class Funzioni
{
    //calcola il modulo dei valori dell'accelerometro e del giroscopio
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

    //funzione che smussa i picchi di una funzione tramite una media mobile
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

    //funzione che calcola il rapporto incrementale di un vettore
    public static double[] derivata(double[] values)
    {
        double[] result = new double[values.Length-1];

        for(int i=0; i<result.Length; ++i)
        {
            result[i] = values[i + 1] - values[i];
        }

        return result;
    }
    

    //calcola deviazione standard sul range di valori scelto in chiamata
    public static double[,,] devStandard(double[,,] values, int range)
    {
        double[,,] result = new double[values.GetLength(0), values.GetLength(1) - (2 * range), values.GetLength(2)];

        for (int i = 0; i < values.GetLength(0); i++)
        {
            int index = 0;
            for (int j = range; j < values.GetLength(1) - range; j++)
            {
                for (int k = 0; k < values.GetLength(2); k++)
                {
                    //calcolo media mobile
                    double sum = 0;
                    for (int s = j - range; s < j + range + 1; s++)
                        sum = sum + values[i, s, k];
                    double media = sum / (2 * range + 1);
                    //

                    result[i, index, k] = 0;
                    for(int a = j- range; a < j + range + 1; a++)
                    {
                        result[i, index, k] = result[i, index, k] + Math.Pow(values[i,a,k] - media, 2);
                    }
                    result[i, index, k] = result[i, index, k] / (2 * range + 1);

                    result[i, index, k] = Math.Sqrt(result[i, index, k]);
                }

                index++;
            }
        }

        return result;

    }

    //funzione che estrae gli angoli di Eulero, a partire dai quaternioni
    public static double[,,] eulero(double[,,] values)
    {
        double[,,] result = new double[values.GetLength(0), values.GetLength(1),3];
        for (int i = 0; i < values.GetLength(0); i++)
        {
            for (int j = 0; j < values.GetLength(1); j++)
            {
                result[i, j, 0] = Math.Atan( (2 * values[i, j, 11] * values[i, j, 12] + 2 * values[i, j, 9] * values[i, j, 10]) /
                                             ( 2 * Math.Pow(values[i, j, 9],2) + 2 * Math.Pow(values[i, j, 12], 2) -1)
                                             );
                result[i, j, 1] = -(Math.Asin(2 * values[i, j, 10] * values[i, j, 12] - 2 * values[i, j, 9] * values[i, j, 11]));

                result[i, j, 2] = Math.Atan((2 * values[i, j, 10] * values[i, j, 11] + 2 * values[i, j, 9] * values[i, j, 12]) /
                                             (2 * Math.Pow(values[i, j, 9], 2) + 2 * Math.Pow(values[i, j, 10], 2) - 1)
                                             );
            }
        }
        return result;
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
