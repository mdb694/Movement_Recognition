using System;
using System.Collections.Generic;


public class DeadReckoning
{
    public List<double[]> deadreck (double [,,] modulo, double[,,]eulero, int sizeWin)
    {
        List<double[]> percorso = new List<double[]>();
        percorso.Add(new double[2] { 0, 0 });

        double x, y;
        double[,,] dev = Funzioni.devStandard(modulo, 10);
        dev = Funzioni.smoothing(dev, 10);
        int l;
        if (modulo.GetLength(1) == sizeWin)
            l = modulo.GetLength(1) / 2;
        else
            l = modulo.GetLength(1);

        for(int i = 0; i < l; i++)
        {
            x = dev[0, i, 0] * Math.Cos(eulero[0,i,2]);
            y = dev[0, i, 0] * Math.Sin(eulero[0, i, 2]);
            percorso.Add(new double[2] { x, y });
        }
        return percorso;
    }
}
