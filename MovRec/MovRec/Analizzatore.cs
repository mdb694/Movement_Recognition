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
            double[,,] orient = Recognizer.orientamento(rawData);
            List<String> msg = new List<string>();

            Program.getForm().dr.deadreck(modulo, eulero, 500, numCampione);

            modulo = Funzioni.smoothing(modulo, 10);
            orient = Funzioni.smoothing(orient, 15);
            Program.getForm().updateModGraph(modulo,numCampione);
            Program.getForm().updateThetaOrGraph(orient, numCampione);

            double firstElement = orient[0,0,0];
            int a = 1;
            int j = 0;

            while (a < orient.GetLength(1))
            {
                j = a;
                while (a < orient.GetLength(1) && orient[0,a,0] >= orient[0,a - 1,0])
                {
                    a++;
                }
                if (Math.Abs(orient[0,a - 1,0] - firstElement) > 0.85)//Differenza di 45 gradi
                {  
                    msg.Add(time.AddSeconds((numCampione * 5) + (0.02 * a)).ToString()+" :: Girato a sinistra");
                    firstElement = orient[0,a - 1,0];
                }
                j = a;
                while (a < orient.GetLength(1) && orient[0, a, 0] < orient[0, a - 1, 0])
                { 
                    a++;
                }
                if (Math.Abs(orient[0, a - 1, 0] - firstElement) > 0.85)//Differenza di 45 gradi
                {
                    msg.Add(time.AddSeconds((numCampione * 5) + (0.02 * a)).ToString()+" :: Girato a destra");
                    firstElement = orient[0,a - 1,0];
                }
            }

            int[] position = Recognizer.posizioneCorpo(rawData);
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
                if (!write && i != length-2)
                {
                    output = output + (time.AddSeconds((numCampione * 5) + (0.02 * i))).ToString();
                    write = true;
                }

                while (actual == before && i < (length - 1))
                {
                    occ++;
                    i++;
                    actual = position[i];
                    before = position[i - 1];
                    if (actual == 0)
                        Program.getForm().loadImage("C:/Users/Marco/Desktop/DIDATTICA/1° SEMESTRE/Programmazione e Amministrazione Sistema/bin/lay.bmp");
                    if (actual == 1)
                        Program.getForm().loadImage("C:/Users/Marco/Desktop/DIDATTICA/1° SEMESTRE/Programmazione e Amministrazione Sistema/bin/laysit.bmp");
                    if (actual == 2)
                        Program.getForm().loadImage("C:/Users/Marco/Desktop/DIDATTICA/1° SEMESTRE/Programmazione e Amministrazione Sistema/bin/sit.bmp");
                    if (actual == 3)
                        Program.getForm().loadImage("C:/Users/Marco/Desktop/DIDATTICA/1° SEMESTRE/Programmazione e Amministrazione Sistema/bin/stand.bmp");
                }
                if (before == 0)
                {
                    output = output + " - " + (time.AddSeconds((numCampione * 5) + (0.02 * i))).ToString() + " :: Sdraiato\n";
                    write = false;
                }
                if (before == 1)
                {
                    output = output + " - " + (time.AddSeconds((numCampione * 5) + (0.02 * i))).ToString() + " :: Sdraiato/Seduto\n";
                    write = false;
                }
                if (before == 2 )
                {
                    output = output + " - " + (time.AddSeconds((numCampione * 5) + (0.02 * i))).ToString() + " :: Seduto\n";
                    write = false;
                }
                if (before == 3)
                {
                    output = output + " - " + (time.AddSeconds((numCampione * 5) + (0.02 * i))).ToString() + " :: In piedi\n";
                    write = false;
                }
                i++;
                occ = 1;
            }
            string[] splitted  = output.Split('\n');
            foreach (var line in splitted)
            {
                msg.Add(line);
            }
            msg.Sort();
            bool first = true;
            foreach (var item in msg)
            {
                if (!first)
                {
                    Program.getForm().setText(item);
                }
                first = false;
            }
        }
    }
}
