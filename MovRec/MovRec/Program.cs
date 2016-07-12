using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovRec
{
    static class Program
    {
        private static Analisi myAnalisiForm;

        [STAThread]
        static void Main()
        {
            /*
            double[,,] sensorValue = MovRecSocket.acquisition();
            Funzioni.printmultimatrix(sensorValue);

            double[,,] modulo = new double [sensorValue.GetLength(0), sensorValue.GetLength(1), 2];
            modulo = Funzioni.modulo(sensorValue);
            Funzioni.printmultimatrix(modulo);
            double[,,] theta = new double[sensorValue.GetLength(0), sensorValue.GetLength(1), 1];
            theta = Recognizer.orientamento(sensorValue);
            bool[,,]staz = Recognizer.stazionamento(Funzioni.devStandard(modulo, 10));
            Funzioni.printmultimatrixbool(staz);
            List<double[]> path = DeadReckoning.deadreck(modulo, Funzioni.eulero(sensorValue), 300);
            foreach (var item in path)
            {
                Console.WriteLine(item[0] + " ::: " + item[1]);
            }
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myAnalisiForm = new Analisi(/*modulo, theta, path*/);
            Application.Run(myAnalisiForm);
        }

        public static Analisi getForm()
        {
            return myAnalisiForm;
        }
    }
}