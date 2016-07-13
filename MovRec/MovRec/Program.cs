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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myAnalisiForm = new Analisi();
            Application.Run(myAnalisiForm);
        }

        public static Analisi getForm()
        {
            return myAnalisiForm;
        }
    }
}