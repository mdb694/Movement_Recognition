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
            DateTime time = DateTime.Now;

            double[,,] rawData = Funzioni.smoothing(sensorValueData, 20);
            double[,,] modulo = Funzioni.modulo(sensorValueData);
            double[,,] eulero = Funzioni.eulero(sensorValueData);
            Program.getForm().dr.deadreck(modulo, eulero, 500, numCampione);

            modulo = Funzioni.smoothing(modulo, 10);
            Program.getForm().updateModGraph(modulo,numCampione);

            int[] position = Recognizer.posizioneCorpo(rawData);
            for (int j = 0; j < position.Length; j++)
            {
                Console.Write(position[j] + "  ");
            }
            String output="";
            int before, actual, i = 1,occ=1, length;
            bool write = false;

            if (position.Length == 500)
                length = 500 / 2;
            else
                length = position.Length;

            while (i < length - 1)
            {
                actual = position[i];
                before = position[i - 1];
                if (!write)
                {
                    output = output + DateTime.Now.ToString();
                    write = true;
                }

                while (actual == before && i < (length - 1))
                {
                    occ++;
                    i++;
                    actual = position[i];
                    before = position[i - 1];
                }
                if (before == 0 && occ > 10)
                {
                    output = output + " - " + DateTime.Now.ToString() + " :: Sdraiato\r\n";
                    write = false;
                }
                if (before == 1 && occ > 10)
                {
                    output = output + " - " + DateTime.Now.ToString() + " :: Sdraiato/Seduto\r\n";
                    write = false;
                }
                if (before == 2 && occ > 10)
                {
                    output = output + " - " + DateTime.Now.ToString() + " :: Seduto\r\n";
                    write = false;
                }
                if (before == 3 && occ > 10)
                {
                    output = output + " - " + DateTime.Now.ToString() + " :: In piedi\r\n";
                    write = false;
                }
                i++;
                occ = 1;
            }
            Program.getForm().setText(output.Remove(output.Length - 2));
        }
    }
}
