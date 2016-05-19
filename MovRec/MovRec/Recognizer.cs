using System;

public class Recognizer
{
    //funzione che,data una finisestra temporale di N istanti, stabilisce se un corpo è fermo o no
	public static bool[,,] stazionamento(double[,,] values)
    {
        bool[,,] result = new bool[values.GetLength(0), values.GetLength(1), 1];

        for(int i=0; i<values.GetLength(0); i++)
        {
            for(int j=0; j< values.GetLength(1); j++)
            {
                if (values[i, j, 0] <= 1)//confronto con la deviazione standard dell'accelerometro
                {
                    result[i, j, 0] = true;
                }
                else
                {
                    result[i, j, 0] = false;
                }
            }
        }

        return result;
    }

    //funzione che calcola per ogni istante della finestra, l'angolo theta tra il vettore che punta al nord magnetico e l'asse z del sistema mondo, sul piano x-z
    public static double[,,] orientamento(double[,,] values)
    {
        double[,,] result = new double[values.GetLength(0), values.GetLength(1), 1];

        for(int i=0; i< values.GetLength(0); i++)
        {
            for(int j=0; j< values.GetLength(1); j++)
            {
                result[i, j, 0] = Math.Atan(values[i, j, 7] / values[i, j, 8]);
            }
        }

        return result;
    }
    /*
    public static int[,,] posizioneCorpo (double[,,] values)
    {
        int[,,] = new int[values.GetLength(0), values.GetLength(1), 1];


    }
    */
}
