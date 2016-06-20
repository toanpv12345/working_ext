using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VETC;

namespace TestCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string encriptKey = "FC9DC415668CF44B";
            string serverIP = "192.168.70.56";
            string account = "test01";
            string pwd = "test@123";
            int station = 1;
            int serverPort = 2699;

            VETCService.ServiceEvent += new VETCServiceEvent(OnReceiver);

            VETCService service = new VETCService(serverIP, serverPort, account, pwd, station, encriptKey);

            Console.Read();
        }

        static void OnReceiver(string packet)
        {
            Console.WriteLine(packet);
        }
    }
}
