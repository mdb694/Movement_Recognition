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
        double[,,] smoothed = new double[data.GetLength(0), data.GetLength(1), data.GetLength(2)];
        double sum = 0;

        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                for (int k = 0; k < data.GetLength(2); k++)
                {
                    if (j < range)
                    {
                        for (int s = 0; s < range + j; s++)
                            sum = sum + data[i, s, k];
                        smoothed[i, j, k] = sum / (j + range + 1);
                    }
                    else if (j >= range && j <= data.GetLength(1) - range)
                    {
                        for (int s = j - range; s < j + range; s++)
                            sum = sum + data[i, s, k];
                        smoothed[i, j, k] = sum / (2 * range + 1);
                    }
                    else if(j > data.GetLength(1) - range)
                    {
                        for (int s = j - range; s < data.GetLength(1); s++)
                            sum = sum + data[i, s, k];
                        smoothed[i, j, k] = sum / (data.GetLength(1) - (j - range));
                    }
                    sum = 0;
                }
            }
        }
        return smoothed;
    }

    //funzione che calcola il rapporto incrementale di un vettore
    public static double[] derivata(double[] values)
    {
        double[] result = new double[values.Length];

        for(int i=0; i<result.Length; ++i)
        {
            result[i] = values[i + 1] - values[i];
        }
        result[values.Length - 1] = 0;

        return result;
    }
    

    //calcola deviazione standard sul range di valori scelto in chiamata
    //input: result della funz modulo [5,200,2]
    //output: [5,180,2]
    public static double[,,] devStandard(double[,,] values, int range)
    {
        double[,,] result = new double[values.GetLength(0), values.GetLength(1), values.GetLength(2)];
        double[,,] mediamobile = smoothing(values, 10);
        double sum = 0;

        for (int i = 0; i < values.GetLength(0); i++)
        {
            for (int j = 0; j < values.GetLength(1); j++)
            {
                for (int k = 0; k < values.GetLength(2); k++)
                {
                    if (j < range)
                    {
                        for (int s = 0; s < j + range; s++)
                            sum = sum + Math.Pow(values[i, s, k] - mediamobile[i, s, k], 2);
                        result[i, j, k] = Math.Sqrt(sum / (range + j + 1));
                    } else if (j >= range && j <= values.GetLength(1) - range)
                    {
                        for (int s = j - range; s < j + range; s++)
                            sum = sum + Math.Pow(values[i, s, k] - mediamobile[i, s, k], 2);
                        result[i, j, k] = Math.Sqrt(sum / (2 * range + 1));
                    } else if (j > values.GetLength(1) - range)
                    {
                        for (int s = j - range; s < values.GetLength(1); s++)
                            sum = sum + Math.Pow(values[i, s, k] - mediamobile[i, s, k], 2);
                        result[i, j, k] = Math.Sqrt(sum / (values.GetLength(1) - (j - range)));
                    }
                    sum = 0;                    
                }
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
                result[i, j, 0] = Math.Atan2( (2 * values[i, j, 11] * values[i, j, 12] + 2 * values[i, j, 9] * values[i, j, 10]) ,
                                             ( 2 * Math.Pow(values[i, j, 9],2) + 2 * Math.Pow(values[i, j, 12], 2) -1)
                                             );
                result[i, j, 1] = -(Math.Asin(2 * values[i, j, 10] * values[i, j, 12] - 2 * values[i, j, 9] * values[i, j, 11]));

                result[i, j, 2] = Math.Atan2((2 * values[i, j, 10] * values[i, j, 11] + 2 * values[i, j, 9] * values[i, j, 12]) ,
                                             (2 * Math.Pow(values[i, j, 9], 2) + 2 * Math.Pow(values[i, j, 10], 2) - 1)
                                             );
            }
        }
        return result;
    }

    //FUNZIONE AUSILIARIA
    public static double[,,] eliminaDiscont(double[,,] values)
    {
        double[,,] result = new double[values.GetLength(0),values.GetLength(1),values.GetLength(2)];
        for (int i = 0; i < values.GetLength(0); i++)
        {
            for (int j = 1; j < values.GetLength(1); j++)
            {
                for (int k = 0; k < values.GetLength(2); k++)
                {
                    if (Math.Abs((values[i, j - 1, k]) - (values[i, j, k])) > (160 * Math.PI / 180.0))
                    {
                        if ((values[i, j - 1, k]) > (values[i, j, k]))
                        {
                            result[i, j, k] = values[i, j, k] + Math.PI * 2;
                        }
                        else if ((values[i, j - 1, k]) < (values[i, j, k]))
                        {
                            result[i, j, k] = values[i, j, k] - Math.PI * 2;
                        }

                    }
                    else
                        result[i, j, k] = values[i, j, k];
                }
            }
        }
        return result;
    }
    ///FUNZIONI PER LE STAMPE A CONSOLLE DI DEBUG
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

    public static void printmultimatrixbool(bool[,,] matrix)
    {
        Console.WriteLine("Matrix print: ");
        for (int i = 0; i < matrix.GetLength(1); i++)
        {   
            for (int j = 0; j < matrix.GetLength(2); j++)
            {
                Console.Write("istante " + i + "  ");
                for (int k = 0; k < matrix.GetLength(0); k++)
                {
                    Console.Write(matrix[k, i, j] + "; ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
