using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCOC.API;
using TCOC.API.Encrypt;
using TCOC.API.Extensions;
using TCOC.API.Objects;

namespace BE_Stress_Tool
{
    public class NoticeArg : EventArgs
    {
        #region Public Properties
        /// <summary>
        /// Return message
        /// </summary>
        public int ThreadID
        {
            get { return _threadID; }
        }

        public int Loop
        {
            get { return _loopNum; }
        }

        public string Message
        {
            get { return _message; }
        }
        #endregion

        #region Private Members
        /// <summary>
        /// Message
        /// </summary>
        private int _threadID;
        private string _message;
        private int _loopNum;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new MessageEventArgs object.
        /// </summary>
        /// <param name="message">Message object that is associated with this event</param>
        public NoticeArg(int inid, int inLoop, string inmessage)
        {
            _threadID = inid;
            _message = inmessage;
            _loopNum = inLoop;
        }
        #endregion
    }

    class RunScenarioWorkerThread
    {
        // Connection parameter
        public int ThreadID;
        public string IPAdd;
        public int Port;
        public int Timeout;
        public string User;
        public string Pass;
        public int Loop;
        public string EncryptAES;
        public string Encrypt3DES;
        public ConfigFile objSettingConfig;

        // Scenario parameter
        public List<string> listScenario;

        // Local variable
        private string PlateRes;
        private int TicketRes;
        private int round;

        // Event for GUI
        public event EventHandler<NoticeArg> EventHappned;

        // Worker thread definition
        private BackgroundWorker bw = new BackgroundWorker();

        // Construction
        public RunScenarioWorkerThread()
        {
            PlateRes = "AF001";
            TicketRes = 1;
            round = 0;
        }

        // Run worker thread to communicate with server
        public void Run()
        {
            // Set parameter for worker thread
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);

            // Run worker thread
            if (bw.IsBusy != true)
            {
                bw.RunWorkerAsync();
            }

        }

        // Main work of thread
        public async void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                // Create new connection
                byte[] byIVArray = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var p = new APIClient(IPAdd, Port, Timeout, EncryptAES, byIVArray);

                // Loop for repeat
                for (round = 0; round < Loop; round++)
                {
                    // Loop for all step
                    for (int i = 0; i < listScenario.Count; i++)
                    {
                        switch (listScenario[i])
                        {
                            case "CONNECT":
                                // Get command parameter
                                int stationID = ComFunc.StringToInt32(objSettingConfig.allItems["CONNECTStation"]);
                                string serverIP = objSettingConfig.allItems["SocketServerIP"];
                                int serverPort = ComFunc.StringToInt32(objSettingConfig.allItems["SocketServerPort"]);
                                // Create command
                                ReportProgress("CONNECT to server ...");
                                var connectReq = new ConnectRequestBody(User, Pass, stationID);
                                // Send and Wait response
                                var re = await p.ConnectAsync(serverIP, serverPort, connectReq);
                                // Report status
                                ReportProgress("CONNECT result: " + re?.Status.ToString());
                                break;
                            case "SHAKE":
                                // Get command parameter
                                // Send command to server and wait response
                                ReportProgress("Send SHAKE to server ...");
                                var re2 = await p.ShakeAsync();
                                // Report status
                                ReportProgress("SHAKE result: " + re2?.Status.ToString());
                                break;
                            case "TERMINATE":
                                // Get command parameter
                                // Send command to server and Wait response
                                ReportProgress("Send TERMINATE to server ...");
                                var re3 = await p.TerminateAsync();
                                // Report status
                                ReportProgress("TERMINATE result: " + re3?.Status.ToString());
                                break;
                            case "CHECKIN":
                                // Get command parameter
                                string eTagCHECKIN = objSettingConfig.allItems["CHECKINEtag"];
                                int stationCHECKIN = ComFunc.StringToInt32(objSettingConfig.allItems["CHECKINStation"]);
                                int laneCHECKIN = ComFunc.StringToInt32(objSettingConfig.allItems["CHECKINLane"]);
                                // Send command to server and Wait response
                                ReportProgress("Send CHECKIN to server ...");
                                var checkin = new ChargeRequestBody(eTagCHECKIN, stationCHECKIN, laneCHECKIN);
                                var re4 = await p.CheckinAsync(checkin);
                                // Report status
                                ReportProgress("CHECKIN result: " + re4?.Status.ToString());
                                PlateRes = re4.Plate;
                                TicketRes = re4.TicketId;
                                break;
                            case "COMMIT":
                                // Get command parameter
                                string eTagCOMMIT = objSettingConfig.allItems["COMMITEtag"];
                                int stationCOMMIT = ComFunc.StringToInt32(objSettingConfig.allItems["COMMITStation"]);
                                int laneCOMMIT = ComFunc.StringToInt32(objSettingConfig.allItems["COMMITLane"]);
                                int imageCOMMIT = ComFunc.StringToInt32(objSettingConfig.allItems["COMMITImageCount"]);
                                // Send command to serve rand wait response
                                ReportProgress("Send COMMIT to server ...");
                                var transaction = new TransactionRequestBody(eTagCOMMIT, stationCOMMIT, laneCOMMIT, TicketRes, PlateStatus.Match, PlateRes, imageCOMMIT);
                                var re5 = await p.CommitAsync(transaction);
                                // Report status
                                ReportProgress("COMMIT result: " + re5?.Status.ToString());
                                break;
                            case "ROLLBACK":
                                // Get command parameter
                                string eTagROLLBACK = objSettingConfig.allItems["ROLLBACKEtag"];
                                int stationROLLBACK = ComFunc.StringToInt32(objSettingConfig.allItems["ROLLBACKStation"]);
                                int laneROLLBACK = ComFunc.StringToInt32(objSettingConfig.allItems["ROLLBACKLane"]);
                                int imageROLLBACK = ComFunc.StringToInt32(objSettingConfig.allItems["ROLLBACKImageCount"]);
                                // Send command to serve rand wait response
                                ReportProgress("Send ROLLBACK to server ...");
                                var rollback = new TransactionRequestBody(eTagROLLBACK, stationROLLBACK, laneROLLBACK, TicketRes, PlateStatus.Match, PlateRes, imageROLLBACK);
                                var re6 = await p.RollbackAsync(rollback);
                                // Report status
                                ReportProgress("ROLLBACK result: " + re6?.Status.ToString());
                                break;
                            case "CHARGE":
                                // Get command parameter
                                string eTagCHARGE = objSettingConfig.allItems["CHARGEEtag"];
                                int stationCHARGE = ComFunc.StringToInt32(objSettingConfig.allItems["CHECKINStation"]);
                                int laneCHARGE = ComFunc.StringToInt32(objSettingConfig.allItems["CHECKINLane"]);
                                // Send command to server and Wait response
                                ReportProgress("Send CHARGE to server ...");
                                var charge_req = new ChargeRequestBody(eTagCHARGE, stationCHARGE, laneCHARGE);
                                var re7 = await p.ChargeAsync(charge_req);
                                // Report status
                                break;
                            default:
                                ReportProgress("Error = Wrong Command");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report Error
                ReportProgress("Error = " + ex.Message);
            }

            // Report progress: complete
            ReportProgress("COMPLETED !");
        }

        // Report Progress
        private void ReportProgress(string Message)
        {
            // Create new notification argument
            NoticeArg Arg = new NoticeArg(ThreadID, round, Message);

            // Raise Msg Incoming event
            var handler = EventHappned;
            if (handler != null)
            {
                handler(this, Arg);
            }
        }
    }
}
