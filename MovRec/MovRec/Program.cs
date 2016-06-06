using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovRec
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,,] sensorValue = MovRecSocket.acquisition();
            Funzioni.printmultimatrix(sensorValue);

            double[,,] modulo = new double [sensorValue.GetLength(0), sensorValue.GetLength(1), 2];
            modulo = Funzioni.modulo(sensorValue);
            Funzioni.printmultimatrix(modulo);
            double[,,] theta = new double[sensorValue.GetLength(0), sensorValue.GetLength(1), 1];
            theta = Recognizer.orientamento(sensorValue);
            bool[,,]staz = Recognizer.stazionamento(Funzioni.devStandard(modulo, 10));
            Funzioni.printmultimatrixbool(staz);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Analisi myAnalisiFrom = new Analisi(modulo, theta);
            Application.Run(myAnalisiFrom);

        }
    }
}