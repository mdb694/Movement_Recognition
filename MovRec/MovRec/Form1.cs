using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace MovRec
{
    public partial class Analisi : Form
    {
        double[,,] mod;
        public Analisi(double[,,] mod)
        {
            this.mod = mod;
            InitializeComponent();
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // pane used to draw your chart
            GraphPane myPane = new GraphPane();

            // poing pair lists
            PointPairList listPointsOne = new PointPairList();

            // line item
            LineItem myCurveOne;

            // set your pane
            myPane = zedGraphControl1.GraphPane;

            // set a title
            myPane.Title.Text = "Modulo Accelerometro";

            // set X and Y axis titles
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";

            // ---- CURVE ONE ----
            // draw a sin curve
            for (int i = 0; i < mod.GetLength(1); i++)
            {
                listPointsOne.Add(i, mod[0, i, 0]);
            }

            // set lineitem to list of points
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
            // ---------------------

            // draw 
            zedGraphControl1.AxisChange();

            // pane used to draw your second chart
            myPane = new GraphPane();

            // poing pair lists
            listPointsOne = new PointPairList();
            
            // set your pane
            myPane = zedGraphControl2.GraphPane;

            // set a title
            myPane.Title.Text = "Modulo Giroscopio";

            // set X and Y axis titles
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";

            // ---- CURVE ONE ----
            // draw a sin curve
            for (int i = 0; i < mod.GetLength(1); i++)
            {
                listPointsOne.Add(i, mod[0, i, 1]);
            }

            // set lineitem to list of points
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
            // ---------------------

            // draw 
            zedGraphControl2.AxisChange();
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
