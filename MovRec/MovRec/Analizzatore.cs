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
            Program.getForm().dr.deadreck(modulo, eulero, 500, numCampione);

            modulo = Funzioni.smoothing(modulo, 10);
            Program.getForm().updateModGraph(modulo,numCampione);

        }
    }
}
