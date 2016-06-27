using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using VETC;

namespace TestCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIP = "192.168.70.56";
            int serverPort = 2699;

            VETCService.ServiceEvent += new VETCServiceEvent(OnReceiver);
            VETCService service = new VETCService(serverIP, serverPort);

            Console.Read();
        }

        static void OnReceiver(string packet)
        {
            Console.WriteLine("CS: " + packet);
        }
    }
}
