using System;
using System.Collections.Generic;

namespace MovRec
{
    public class DeadReckoning
    {
        double firstX = 0;
        double firstY = 0;
        List<double[]> percorso;

        public DeadReckoning()
        {
            percorso = new List<double[]>();
        }

        public void deadreck(double[,,] modulo, double[,,] eulero, int sizeWin, int numCampione)
        {
            
            double x = 0, y = 0;
            double[,,] dev = Funzioni.devStandard(modulo, 10);
            dev = Funzioni.smoothing(dev, 10);
            eulero = Funzioni.smoothing(eulero, 10);

            int l;
            if (modulo.GetLength(1) == sizeWin)
                l = modulo.GetLength(1) / 2;
            else
                l = modulo.GetLength(1);

            for (int i = 1; i < l; i++)
            {
                if (dev[0, i, 0] > 1)
                {
                    x = dev[0, i, 0] * Math.Cos(eulero[0, i, 2]);
                    y = dev[0, i, 0] * Math.Sin(eulero[0, i, 2]);
                    firstX = firstX + x;
                    firstY = firstY + y;
                    percorso.Add(new double[2] { firstX, firstY });
                }
            }
            Program.getForm().updateDeadReckGraph(percorso);
            percorso.Clear();
        }
    }
}
