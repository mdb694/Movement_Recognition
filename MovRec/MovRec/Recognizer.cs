using System;

public class Recognizer
{
    //funzione che,data una finisestra temporale di N istanti, stabilisce se un corpo è fermo o no
    //input devStandard [5,180(range=10),2]
    //output bool stazionamneto [5,180,1]
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
                result[i, j, 0] = Math.Atan2(values[i, j, 7] , values[i, j, 8]);
            }
        }

        result = Funzioni.eliminaDiscont(result);
        return result;
    }
    
    public static int[] posizioneCorpo (double[,,] values)
    {
        int[] result = new int[values.GetLength(1)];
        for(int i=0; i< values.GetLength(1); i++)
        {
            if(values[0,i,0] < 2.7)
            {
                result[i] = 0;//lay
            }
            else
            {
                if (values[0, i, 0] >= 2.7 && values[0, i, 0] < 3.7)
                {
                    result[i] = 1;//LaySit
                }
                else
                {
                    if (values[0, i, 0] >= 3.7 && values[0, i, 0] < 7.2)
                    {
                        result[i] = 2;//Sit
                    }
                    else
                    {
                        result[i] = 3;//Stand
                    }
                }
            }
            
        }
        return result;
    }
    
}
