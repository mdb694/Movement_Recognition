using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MovRec {
    public class MovRecSocket
    {
        private static int sizewin = 500;
        private static int maxSensori = 10;
        private static int numCampioni = 0;

        static TcpListener listener;
        static Socket socket;

        public static void Start()
        {
            Program.getForm().setText(DateTime.Now.ToString()+" :: Attendendo la connessione...");
            try {
                var EP = new IPEndPoint(IPAddress.Any, 45555);

                // Creo una socket di tipo TCP
                listener = new TcpListener(EP);
                while (true) {
                    try {

                        listener.Start();
                        socket = listener.AcceptSocket(); // blocca
                        DateTime time = DateTime.Now;
                        Program.getForm().setText(DateTime.Now.ToString() + " :: Connessione stabilita!");

                        Stream stream = new NetworkStream(socket);
                        BinaryReader bin = new BinaryReader(stream);

                        byte[] len = new byte[2];
                        byte[] tem = new byte[3];
                        int byteToRead;
                        byte[] pacchetto;
                        byte[] read;

                        //PULISCO??
                        Program.getForm().cleanModGraph();
                        numCampioni = 0;

                        while (!(tem[0] == 0xFF && tem[1] == 0x32)) // cerca la sequenza FF-32
                        {
                            tem[0] = tem[1];
                            tem[1] = tem[2];
                            if((read = bin.ReadBytes(1)) != null)                     
                                tem[2] = read[0];
                        }
                        if (tem[2] != 0xFF) // modalità normale
                        {
                            byteToRead = tem[2]; // byte da leggere
                        }
                        else  // modalità extended-length
                        {
                            len = new byte[2];
                            len = bin.ReadBytes(2);
                            byteToRead = (len[0] * 256) + len[1]; // byte da leggere
                        }

                        byte[] data = new byte[byteToRead + 1];
                        data = bin.ReadBytes(byteToRead + 1); // lettura dei dati

                        if (tem[2] != 0xFF)
                        {
                            pacchetto = new byte[byteToRead + 4]; // creazione pacchetto
                        }
                        else
                        {
                            pacchetto = new byte[byteToRead + 6];
                        }

                        int numSensori = (byteToRead - 2) / 52; // calcolo del numero di sensori
                        Program.getForm().setText(DateTime.Now.ToString() + " :: Rilevati " + numSensori + " sensori");
                        Program.getForm().setText(DateTime.Now.ToString() + " :: Ricezione dati");
                        pacchetto[0] = 0xFF; // copia dei primi elementi
                        pacchetto[1] = 0x32;
                        pacchetto[2] = tem[2];

                        if (tem[2] != 0xFF)
                        {
                            data.CopyTo(pacchetto, 3); // copia dei dati
                        }
                        else
                        {
                            pacchetto[3] = len[0];
                            pacchetto[4] = len[1];
                            data.CopyTo(pacchetto, 5); // copia dei dati
                        }

                        List<List<double>> array = new List<List<double>>(); // salvataggio dati
                        List<List<double>> tempArray;
                        List<List<List<double>>> sensorValue = new List<List<List<double>>>();
                        double[,,] sensorValueArray;
                        Analizzatore analiser = new Analizzatore();

                        int campioni = 0;
                        int leftband = 0;

                        int[] t = new int[maxSensori];

                        for (int x = 0; x < numSensori; x++)
                        {
                            array.Add(new List<double>()); // una lista per ogni sensore
                            t[x] = 5 + (52 * x);
                        }
                        
                        while (pacchetto.Length > 0)
                        {
                            for (int i = 0; i < numSensori; i++)
                            {
                                byte[] temp = new byte[4];
                                for (int tr = 0; tr < 13; tr++)// 13 campi, 3 * 3 + 4
                                {
                                    if (numSensori < 5)
                                    {
                                        temp[0] = pacchetto[t[i] + 3]; // lettura inversa
                                        temp[1] = pacchetto[t[i] + 2];
                                        temp[2] = pacchetto[t[i] + 1];
                                        temp[3] = pacchetto[t[i]];
                                    }
                                    else
                                    {
                                        temp[0] = pacchetto[t[i] + 5];
                                        temp[1] = pacchetto[t[i] + 4];
                                        temp[2] = pacchetto[t[i] + 3];
                                        temp[3] = pacchetto[t[i] + 2];
                                    }
                                    var valore = BitConverter.ToSingle(temp, 0); // conversione
                                    array[i].Add(valore); // memorizzazione
                                    t[i] += 4;
                                }
                            }
                            tempArray = array.Select(x => x.ToList()).ToList();
                            sensorValue.Add(tempArray);
                            
                            for (int x = 0; x < numSensori; x++)
                            {
                                t[x] = 5 + (52 * x);
                            }
                            campioni++;

                            for (int i = 0; i < numSensori; i++)
                            {
                                array[i].RemoveRange(0, 13);
                            }

                            if(campioni == leftband + sizewin)
                            {
                                sensorValueArray = new double[numSensori,sensorValue.Count,13];
                                int i=0, j=0, k=0;
                                foreach (var list in sensorValue)
                                {
                                    foreach (var subllist in list)
                                    {
                                        foreach (var element in subllist)
                                        {
                                            Console.Write(element+ " ");
                                            sensorValueArray[i, j, k] = element;
                                            k++;
                                        }
                                        Console.WriteLine();
                                        k = 0;
                                        i++;
                                    }
                                    Console.WriteLine("------------------------");
                                    i = 0;
                                    k = 0;
                                    j++;
                                }
                                analiser.setData(sensorValueArray);
                                analiser.analizza(numCampioni);
                                numCampioni++;

                                sensorValue.RemoveRange(0, sensorValue.Count / 2);
                                leftband = campioni - sizewin / 2;
                                time = time.AddSeconds(250 * 0.02);
                            }

                            if (numSensori < 5) // lettura pacchetto seguente
                            {
                                pacchetto = bin.ReadBytes(byteToRead + 4);
                            }
                            else
                            {
                                pacchetto = bin.ReadBytes(byteToRead + 6);
                            }

                            if(pacchetto.Length <= 0)
                            {
                                //ANALIZZO
                                //SETTO FINALE GRAFICI
                                sensorValueArray = new double[numSensori, sensorValue.Count, 13];
                                int i = 0, j = 0, k = 0;
                                foreach (var list in sensorValue)
                                {
                                    foreach (var subllist in list)
                                    {
                                        foreach (var element in subllist)
                                        {
                                            Console.Write(element + " ");
                                            sensorValueArray[i, j, k] = element;
                                            k++;
                                        }
                                        Console.WriteLine();
                                        k = 0;
                                        i++;
                                    }
                                    Console.WriteLine("------------------------");
                                    i = 0;
                                    k = 0;
                                    j++;
                                }
                                analiser.setData(sensorValueArray);
                                analiser.analizza(numCampioni);
                            }

                        }
                        stream.Close();
                        socket.Close();
                        Program.getForm().setText(DateTime.Now.ToString() + " :: Disconnesso. Attendo riconnessione...");
                    } catch (Exception)
                    {
                        socket.Close();
                        listener.Stop();
                        Program.getForm().setText(DateTime.Now.ToString() + " :: Disconnesso. Attendo riconnessione...");
                    }
                }
            } catch (SocketException e)
            {
                Program.getForm().setText(DateTime.Now.ToString() + " :: Errore connessione" + e);
            } finally
            {
                listener.Stop();
                Program.getForm().setText(DateTime.Now.ToString() + " :: In attesa di connessione...");
            }
        }
    }
}
