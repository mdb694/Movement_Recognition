using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace MovRec
{
    public partial class Analisi : Form
    {
        double[] lastPoint = new double[20];
        public DeadReckoning dr;
        double[] lastThetaPoint = new double[2];

        delegate void WriteTextBox(string msg);
        delegate void SaveTextBox();
        delegate void LoadImage(String address);
        delegate void UpdateGraph(double[,,] mod, int numCampione);
        delegate void CleanGraph();
        delegate void UpdateThetaGraph(double[,,] orient, int numCampione);
        delegate void CleanThetaGraph();
        delegate void UpdateDeadGraph(List<double[]> path);
        delegate void CleanDeadGraph();

        public Analisi()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dr = new DeadReckoning();
            // pane used to draw your chart
            GraphPane myPane = new GraphPane();
            //setto il textbox a read only
            this.richTextBox1.ReadOnly = true;
            // set your pane
            myPane = zedGraphControl1.GraphPane;

            // set a title
            myPane.Title.Text = "Modulo Accelerometro";

            // set X and Y axis titles
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";

            // pane used to draw your second chart
            myPane = new GraphPane();
            
            // set your pane
            myPane = zedGraphControl2.GraphPane;

            // set a title
            myPane.Title.Text = "Modulo Giroscopio";

            // set X and Y axis titles
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";

            myPane = new GraphPane();
            myPane = zedGraphControl3.GraphPane;
            myPane.Title.Text = "Modulo Accelerometro";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";

            myPane = new GraphPane();
            myPane = zedGraphControl4.GraphPane;
            myPane.Title.Text = "Modulo Giroscopio";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";
            
            myPane = new GraphPane();
            myPane = zedGraphControl5.GraphPane;
            myPane.Title.Text = "Modulo Accelerometro";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";
   
            myPane = new GraphPane();
            myPane = zedGraphControl6.GraphPane;
            myPane.Title.Text = "Modulo Giroscopio";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";
            
            myPane = new GraphPane();
            myPane = zedGraphControl7.GraphPane;
            myPane.Title.Text = "Modulo Accelerometro";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";

            myPane = new GraphPane();
            myPane = zedGraphControl8.GraphPane;
            myPane.Title.Text = "Modulo Giroscopio";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";
            
            myPane = new GraphPane();
            myPane = zedGraphControl9.GraphPane;
            myPane.Title.Text = "Modulo Accelerometro";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Acc Modulo";
            
            myPane = new GraphPane();
            myPane = zedGraphControl10.GraphPane;
            myPane.Title.Text = "Modulo Giroscopio";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "Giro Modulo";

            //STAMPA THETA
            // poing pair lists
            PointPairList listPointsOne = new PointPairList();
            // line item
            LineItem myCurveOne;
            LineItem myThetaCurve;
            myPane = new GraphPane();
            listPointsOne = new PointPairList();
            myPane = zedGraphControl11.GraphPane;
            myPane.Title.Text = "Theta";
            myPane.XAxis.Title.Text = "Tempo";
            myPane.YAxis.Title.Text = "THETA";
/*            for (int i = 0; i < mod.GetLength(1); i++)
            {
                listPointsOne.Add(i, theta[0, i, 0]);
           }
*/          myThetaCurve = myPane.AddCurve(null, listPointsOne, Color.Blue, SymbolType.Circle);
//            zedGraphControl11.AxisChange();

            //DEAD
            myPane = new GraphPane();
            listPointsOne = new PointPairList();
            myPane = zedGraphControl12.GraphPane;
            myPane.Title.Text = "Dead Reckoning";
            myPane.XAxis.Title.Text = "X";
            myPane.YAxis.Title.Text = "Y";
            myCurveOne = myPane.AddCurve(null, listPointsOne, Color.Gold, SymbolType.Diamond);

        }
        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl3_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl4_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void zedGraphControl9_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl10_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl11_Load(object sender, EventArgs e)
        {

        }
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl12_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void setText(String msg)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                WriteTextBox wtxt = new WriteTextBox(setText);
                this.Invoke(wtxt, new object[] { msg });
            }
            else
            {
                this.richTextBox1.AppendText(msg+ "\r\n");
            }
        }

        public void updateModGraph(double[,,] mod, int numCampione)
        {
            if (this.zedGraphControl1.InvokeRequired || this.zedGraphControl2.InvokeRequired ||
                this.zedGraphControl3.InvokeRequired || this.zedGraphControl4.InvokeRequired ||
                this.zedGraphControl5.InvokeRequired || this.zedGraphControl6.InvokeRequired ||
                this.zedGraphControl7.InvokeRequired || this.zedGraphControl8.InvokeRequired ||
                this.zedGraphControl9.InvokeRequired || this.zedGraphControl10.InvokeRequired)
            {
                UpdateGraph updtgrp = new UpdateGraph(updateModGraph);
                this.Invoke(updtgrp, new object[] { mod, numCampione });
            }
            else
            {
                int lung;
                if (mod.GetLength(1) == 500)
                    lung = 250;
                else
                    lung = mod.GetLength(1);
                // poing pair lists
                PointPairList listPointsOne = new PointPairList();
                // line item
                LineItem myCurveOne, myCurveTwo;
                // ---- CURVE ONE ----
                // draw a sin curve
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[0] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[1] = mod[0, i, 0];
                    }
                    if (numCampione > 0 && i== 0)
                        listPointsOne.Add(lastPoint[0], lastPoint[1]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[0, i, 0]);
                }
                // set lineitem to list of points
                myCurveOne = zedGraphControl1.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                // draw 
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                zedGraphControl1.Refresh();
                //-----------------------------------------------------------------------------------//
                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[2] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[3] = mod[0, i, 1];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[2], lastPoint[3]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[0, i, 1]);
                }
                myCurveTwo = zedGraphControl2.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl2.AxisChange();
                zedGraphControl2.Invalidate();
                zedGraphControl2.Refresh();
                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[4] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[5] = mod[1, i, 0];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[4], lastPoint[5]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[1, i, 0]);
                }
                myCurveOne = zedGraphControl3.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl3.AxisChange();
                zedGraphControl3.Invalidate();
                zedGraphControl3.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[6] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[7] = mod[1, i, 1];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[6], lastPoint[7]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[1, i, 1]);
                }
                myCurveOne = zedGraphControl4.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl4.AxisChange();
                zedGraphControl4.Invalidate();
                zedGraphControl4.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[8] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[9] = mod[2, i, 0];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[8], lastPoint[9]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[2, i, 0]);
                }
                myCurveOne = zedGraphControl5.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl5.AxisChange();
                zedGraphControl5.Invalidate();
                zedGraphControl5.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[10] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[11] = mod[2, i, 1];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[10], lastPoint[11]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[2, i, 1]);
                }
                myCurveOne = zedGraphControl6.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl6.AxisChange();
                zedGraphControl6.Invalidate();
                zedGraphControl6.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[12] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[13] = mod[3, i, 0];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[12], lastPoint[13]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[3, i, 0]);
                }
                myCurveOne = zedGraphControl7.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl7.AxisChange();
                zedGraphControl7.Invalidate();
                zedGraphControl7.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[14] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[15] = mod[3, i, 1];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[14], lastPoint[15]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[3, i, 1]);
                }
                myCurveOne = zedGraphControl8.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl8.AxisChange();
                zedGraphControl8.Invalidate();
                zedGraphControl8.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[16] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[17] = mod[4, i, 0];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[16], lastPoint[17]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[4, i, 0]);
                }
                myCurveOne = zedGraphControl9.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl9.AxisChange();
                zedGraphControl9.Invalidate();
                zedGraphControl9.Refresh();

                listPointsOne = new PointPairList();
                for (int i = 0; i < lung; i++)
                {
                    if (i == lung - 1)
                    {
                        lastPoint[18] = ((0.02 * i) + (numCampione * 5));
                        lastPoint[19] = mod[4, i, 1];
                    }
                    if (numCampione > 0 && i == 0)
                        listPointsOne.Add(lastPoint[18], lastPoint[19]);
                    listPointsOne.Add(((0.02 * i) + (numCampione * 5)), mod[4, i, 1]);
                }
                myCurveOne = zedGraphControl10.GraphPane.AddCurve(null, listPointsOne, Color.Black, SymbolType.Circle);
                zedGraphControl10.AxisChange();
                zedGraphControl10.Invalidate();
                zedGraphControl10.Refresh();
            }
        }

        public void cleanModGraph()
        {
            if (this.zedGraphControl1.InvokeRequired || this.zedGraphControl2.InvokeRequired ||
                this.zedGraphControl3.InvokeRequired || this.zedGraphControl4.InvokeRequired ||
                this.zedGraphControl5.InvokeRequired || this.zedGraphControl6.InvokeRequired ||
                this.zedGraphControl7.InvokeRequired || this.zedGraphControl8.InvokeRequired ||
                this.zedGraphControl9.InvokeRequired || this.zedGraphControl10.InvokeRequired)
            {
                CleanGraph clngrp = new CleanGraph(cleanModGraph);
                this.Invoke(clngrp, new object[] { });
            }
            else
            {
                if (zedGraphControl1.GraphPane.CurveList.Count != 0)
                    zedGraphControl1.GraphPane.CurveList.Clear();
                if (zedGraphControl2.GraphPane.CurveList.Count != 0)
                    zedGraphControl2.GraphPane.CurveList.Clear();
                if (zedGraphControl3.GraphPane.CurveList.Count != 0)
                    zedGraphControl3.GraphPane.CurveList.Clear();
                if (zedGraphControl4.GraphPane.CurveList.Count != 0)
                    zedGraphControl4.GraphPane.CurveList.Clear();
                if (zedGraphControl5.GraphPane.CurveList.Count != 0)
                    zedGraphControl5.GraphPane.CurveList.Clear();
                if (zedGraphControl6.GraphPane.CurveList.Count != 0)
                    zedGraphControl6.GraphPane.CurveList.Clear();
                if (zedGraphControl7.GraphPane.CurveList.Count != 0)
                    zedGraphControl7.GraphPane.CurveList.Clear();
                if (zedGraphControl8.GraphPane.CurveList.Count != 0)
                    zedGraphControl8.GraphPane.CurveList.Clear();
                if (zedGraphControl9.GraphPane.CurveList.Count != 0)
                    zedGraphControl9.GraphPane.CurveList.Clear();
                if (zedGraphControl10.GraphPane.CurveList.Count != 0)
                    zedGraphControl10.GraphPane.CurveList.Clear();
            }
        }

        public void updateDeadReckGraph(List<double[]> path)
        {
            if (this.zedGraphControl12.InvokeRequired)
            {
                UpdateDeadGraph d = new UpdateDeadGraph(updateDeadReckGraph);
                this.Invoke(d, new object[] { path });
            }
            else
            {
                for (int i = 0; i < path.Count; i++)
                {  
                    zedGraphControl12.GraphPane.CurveList[0].AddPoint(path[i][0], path[i][1]); // Aggiungo x e y

                    zedGraphControl12.AxisChange();
                    zedGraphControl12.Invalidate();
                    zedGraphControl12.Refresh();
                }
                zedGraphControl12.AxisChange();
                zedGraphControl12.Invalidate();
                zedGraphControl12.Refresh();
            }
        }

        public void cleanDeadReckGraph()
        {
            if (this.zedGraphControl12.InvokeRequired)
            {
                CleanDeadGraph clndr = new CleanDeadGraph(cleanDeadReckGraph);
                this.Invoke(clndr, new object[] { });
            }
            else
            {
                if (zedGraphControl12.GraphPane.CurveList.Count != 0)
                    zedGraphControl12.GraphPane.CurveList[0].Clear();
            }
        }

        public void updateThetaOrGraph(double[,,] orient, int numCampione)
        {
            if (this.zedGraphControl11.InvokeRequired)
            {
                UpdateThetaGraph d = new UpdateThetaGraph(updateThetaOrGraph);
                this.Invoke(d, new object[] {orient, numCampione });
            }
            else
            {
                if (numCampione > 0)
                {
                    double[,,] newTheta = new double[1, orient.GetLength(1) + 1, 1];
                    newTheta[0, 0, 0] = lastThetaPoint[1];
                    for (int i = 1; i < orient.GetLength(1); i++)
                    {
                        newTheta[0, i, 0] = orient[0, i - 1, 0];
                    }
                    orient = Funzioni.eliminaDiscont(newTheta);
                }
                int lung;
                if (orient.GetLength(1) >= 500)
                    lung = 500 / 2 ;
                else
                    lung = orient.GetLength(1);

                for (int i = 1; i < lung; i++)
                {
                    zedGraphControl11.GraphPane.CurveList[0].AddPoint(((0.02 * (i-1)) + (numCampione * 5)), orient[0, i, 0]); // Aggiungo x e y
                    if (i == lung - 1)
                    {
                        lastThetaPoint[0] = ((0.02 * i) + (numCampione * 5));
                        lastThetaPoint[1] = orient[0, i, 0];
                    }
                    zedGraphControl11.AxisChange();
                    zedGraphControl11.Invalidate();
                    zedGraphControl11.Refresh();
                }
                zedGraphControl11.AxisChange();
                zedGraphControl11.Invalidate();
                zedGraphControl11.Refresh();
            }
        }

        public void cleanThetaOrGraph()
        {
            if (this.zedGraphControl11.InvokeRequired)
            {
                CleanThetaGraph clndr = new CleanThetaGraph(cleanThetaOrGraph);
                this.Invoke(clndr, new object[] { });
            }
            else
            {
                if (zedGraphControl11.GraphPane.CurveList.Count != 0)
                    zedGraphControl11.GraphPane.CurveList[0].Clear();
            }
        }

        public void saveText()
        {
            if (this.richTextBox1.InvokeRequired)
            {
                SaveTextBox stxt = new SaveTextBox(saveText);
                this.Invoke(stxt, new object[] { });
            }
            else
            {
                string mydocpath =
                            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                this.richTextBox1.SaveFile(mydocpath + @"/log.rtf");
            }
        }

        public void loadImage(String address)
        {
            if (this.pictureBox1.InvokeRequired)
            {
                LoadImage ldimg = new LoadImage(loadImage);
                this.Invoke(ldimg, new object[] { address });
            }
            else
            {
                pictureBox1.ImageLocation = address;
                this.pictureBox1.Load();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                button1.Text = "AVVIATO";
                button1.Enabled = false;
                MovRecSocket acq = new MovRecSocket();
                Thread acquisitionThread = new Thread(new ThreadStart(MovRecSocket.Start));
                acquisitionThread.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveText();
            MessageBox.Show("File di Log salvato correttamente", "Conferma Salvataggio",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Analisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveText();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
