using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

public class MovRecSocket
{
    public static double[,,] acquisition ()
    {
        var EP = new IPEndPoint(IPAddress.Any, 45555);

        // Creo una socket di tipo TCP
        var listener = new TcpListener(EP);
        listener.Start();

        var socket = listener.AcceptSocket(); // blocca
        Console.WriteLine("Connessione stabilita!");

        Stream stream = new NetworkStream(socket);
        BinaryReader bin = new BinaryReader(stream);

        byte[] len = new byte[2];
        byte[] tem = new byte[3];
        int byteToRead;
        byte[] pacchetto;
        int sizewin = 200;

        while (!(tem[0] == 0xFF && tem[1] == 0x32)) // cerca la sequenza FF-32
        {
            tem[0] = tem[1];
            tem[1] = tem[2];
            byte[] read = bin.ReadBytes(1);
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
        Console.WriteLine("Numero sensori: " + numSensori);
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
        int maxSensori = 10;
        // Set a variable to the My Documents path.
        string mydocpath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        int[] t = new int[maxSensori];

        for (int x = 0; x < numSensori; x++)
        {
            array.Add(new List<double>()); // una lista per ogni sensore
            t[x] = 5 + (52 * x);
        }
        int tempwin = 0;
        double[,,] sensorValue = new double[numSensori, sizewin, 13];
        /*            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\data.txt"))
                    {
                        outputFile.WriteLine("time; sensor; acc1; acc2; acc3; giro1; giro2; giro3; magn1; magn2; magn3;");
                    }
        */
        while (tempwin < sizewin * numSensori)
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
            for (int x = 0; x < numSensori; x++)
            {
                t[x] = 5 + (52 * x);
            }

            for (int j = 0; j < numSensori; j++)
            {
                /*                    using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\data.txt", true))
                                    {
                                        outputFile.Write(tempwin + "; " + j + "; ");
                                    }
                */
                for (int tr = 0; tr < 13; tr++) //con 13 stampo anche i quaternioni
                {
                    // esempio output su console
                    Console.Write(array[j][tr] + "; ");
                    //salvo nella matrice
                    sensorValue[j, tempwin / numSensori, tr] = array[j][tr];
                    // Append text to an existing file named "data.txt".
                    /*                        using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\data.txt", true))
                                            {
                                                outputFile.Write(array[j][tr] + "; ");
                                            }
                    */
                }
                /*                    using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\data.txt", true))
                                    {
                                        outputFile.WriteLine();
                                    }
                */
                Console.WriteLine();
                array[j].RemoveRange(0, 13); // cancellazione dati
                tempwin++;
            }

            /*                using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\data.txt", true))
                            {
                                outputFile.WriteLine();
                            }
            */
            Console.WriteLine();

            if (numSensori < 5) // lettura pacchetto seguente
            {
                pacchetto = bin.ReadBytes(byteToRead + 4);
            }
            else
            {
                pacchetto = bin.ReadBytes(byteToRead + 6);
            }
        }
        return sensorValue;
    }
}
