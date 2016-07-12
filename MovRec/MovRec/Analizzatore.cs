using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovRec
{
    class Analizzatore
    {
        private double[,,] sensorValueData;

        public void setData(double[,,] value)
        {
            this.sensorValueData = value;
        }

        public void analizza(int numCampione)
        {
            double[,,] modulo = Funzioni.modulo(sensorValueData);
            double[,,] eulero = Funzioni.eulero(sensorValueData);
            eulero = Funzioni.eliminaDiscont(eulero);
            modulo = Funzioni.smoothing(modulo, 10);
            List<double[]> pathDR = DeadReckoning.deadreck(modulo, eulero, 500);

            Program.getForm().updateModGraph(modulo,numCampione);
            Program.getForm().updateDeadReckGraph(pathDR);
            pathDR.Clear();
        }
    }
}
