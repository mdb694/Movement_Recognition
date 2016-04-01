using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MovRec
{
    class Program
    {
        static void Main(string[] args)
        {
            var EP = new IPEndPoint(IPAddress.Any, 45555);

            // Creo una socket di tipo TCP
            var listener = new TcpListener(EP);
            listener.Start();

            var socket = listener.AcceptSocket(); // blocca
        }
    }
}
