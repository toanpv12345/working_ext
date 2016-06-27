using System;
using System.Threading.Tasks;
using TCOC.API;
using TCOC.API.Encrypt;
using TCOC.API.Extensions;
using TCOC.API.Objects;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.RunAsync().GetAwaiter().GetResult();
        }

        public async Task RunAsync()
        {
            try
            {
                //anh de interval =3 :)
                //client port 25
                string encriptKey = "FC9DC415668CF44B";
                string serverIP = "192.168.70.56";
                string account = "vetc";
                string pwd = "vetc@123";
                int station = 460;
                int serverPort = 2699;

                var p = new APIClient("10.6.4.22", 25, 30, encriptKey, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                var re = await p.ConnectAsync(serverIP, serverPort, new ConnectRequestBody(account, pwd, station));
                Console.WriteLine(re?.Status);

                var charge = new ChargeRequestBody("AF001", station, 1);
                var re3 = await p.CheckinAsync(charge);
                Console.WriteLine(re3?.Status);

                var transaction = new TransactionRequestBody("AF001", 1, 2, re3.TicketId, PlateStatus.Match, re3.Plate, 1);
                var re4 = await p.CommitAsync(transaction);
                Console.WriteLine(re4?.Status);

                //var re2 = await p.ChargeAsync(charge);
                //Console.WriteLine(re2.Status);
                //var re5 = await p.RollbackAsync(transaction);
                //var re6 = await p.TerminateAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace}");
            }
            Console.Write("done");
            Console.ReadLine();
        }
    }
}
