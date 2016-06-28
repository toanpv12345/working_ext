using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BE_Stress_Tool
{
    class Struct
    {
        // CONNECT structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CONNECT
        {
            public int CONNECTMessageLength;
            public int CONNECTCommandId;
            public int CONNECTRequestId;
            public int CONNECTSessionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CONNECTUsername;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CONNECTPassword;
            public int CONNECTStation;
            public int CONNECTTimeout;
        };
        public static CONNECT stCONNECT = new CONNECT();

        // SHAKE structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SHAKE
        {
            public int SHAKEMessageLength;
            public int SHAKECommandId;
            public int SHAKERequestId;
            public int SHAKESessionId;
        };
        public static SHAKE stSHAKE = new SHAKE();

        // TERMINATE structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TERMINATE
        {
            public int TERMINATEMessageLength;
            public int TERMINATECommandId;
            public int TERMINATERequestId;
            public int TERMINATESessionId;
        };
        public static TERMINATE stTERMINATE = new TERMINATE();

        // CHECKIN structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CHECKIN
        {
            public int CHECKINMessageLength;
            public int CHECKINCommandId;
            public int CHECKINRequestId;
            public int CHECKINSessionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public char[] CHECKINEtag;
            public int CHECKINStation;
            public int CHECKINLane;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] CHECKINPlate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] CHECKINTID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] CHECKINHashValue;
        };
        public static CHECKIN stCHECKIN = new CHECKIN();

        // COMMIT structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct COMMIT
        {
            public int COMMITMessageLength;
            public int COMMITCommandId;
            public int COMMITRequestId;
            public int COMMITSessionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public char[] COMMITEtag;
            public int COMMITStation;
            public int COMMITLane;
            public int COMMITTicketId;
            public int COMMITStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] COMMITPlate;
            public int COMMITImageCount;
            public int COMMITVehicleLength;
        };
        public static COMMIT stCOMMIT = new COMMIT();

        // ROLLBACK structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ROLLBACK
        {
            public int ROLLBACKMessageLength;
            public int ROLLBACKCommandId;
            public int ROLLBACKRequestId;
            public int ROLLBACKSessionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public char[] ROLLBACKEtag;
            public int ROLLBACKStation;
            public int ROLLBACKLane;
            public int ROLLBACKTicketId;
            public int ROLLBACKStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] ROLLBACKPlate;
            public int ROLLBACKImageCount;
        };
        public static ROLLBACK stROLLBACK = new ROLLBACK();

        // CHARGE structure
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CHARGE
        {
            public int CHARGEMessageLength;
            public int CHARGECommandId;
            public int CHARGERequestId;
            public int CHARGESessionId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public char[] CHARGEEtag;
            public int CHARGEStation;
            public int CHARGELane;
        };
        public static CHARGE stCHARGE = new CHARGE();

        // List of command
        public object[] ListCommand = new object[] {
            stCONNECT,
            stSHAKE,
            stTERMINATE,
            stCHECKIN,
            stCOMMIT,
            stROLLBACK,
            stCHARGE
        };

        public Struct()
        {
            // Initialize variable
            // CONNECT
            stCONNECT.CONNECTUsername = new char[10];
            stCONNECT.CONNECTPassword = new char[10];
            ListCommand[0] = stCONNECT;
            // CHECKIN
            stCHECKIN.CHECKINEtag = new char[24];
            stCHECKIN.CHECKINPlate = new char[10];
            stCHECKIN.CHECKINTID = new char[12];
            stCHECKIN.CHECKINHashValue = new char[8];
            ListCommand[3] = stCHECKIN;
            // COMMIT
            stCOMMIT.COMMITEtag = new char[24];
            stCOMMIT.COMMITPlate = new char[10];
            ListCommand[4] = stCOMMIT;
            // ROLLBACK
            stROLLBACK.ROLLBACKEtag = new char[24];
            stROLLBACK.ROLLBACKPlate = new char[10];
            ListCommand[5] = stROLLBACK;
            // CHARGE
            stCHARGE.CHARGEEtag = new char[24];
            ListCommand[6] = stCHARGE;
        }

        // Generate all command
        public int GenerateAllCommand(Dictionary<string, string> dicOptValue)
        {
            for (int i = 0; i < ListCommand.Count(); i++)
            {
                GenerateCommand(dicOptValue, ref ListCommand[i]);
            }

            return DIOConst.RET_OK;
        }

        // Generate 1 command
        public int GenerateCommand(Dictionary<string, string> dicOptValue, ref object objStruct)
        {
            // Set value from dictionary to structre
            ComFunc.SetValueToStruct(dicOptValue, ref objStruct);

            return DIOConst.RET_OK;
        }

        // Generate command byte
        public byte[] GenerateCommandByte(string CommandName, int ReqId, int SecID)
        {
            switch (CommandName)
            {
                case "CONNECT":
                    stCONNECT = (CONNECT)ListCommand[0];
                    stCONNECT.CONNECTRequestId = ReqId;
                    stCONNECT.CONNECTSessionId = SecID;
                    return ComFunc.StructureToByteArray(stCONNECT);
                case "SHAKE":
                    stSHAKE = (SHAKE)ListCommand[1];
                    stSHAKE.SHAKERequestId = ReqId;
                    stSHAKE.SHAKESessionId = SecID;
                    return ComFunc.StructureToByteArray(stSHAKE);
                case "TERMINATE":
                    stTERMINATE = (TERMINATE)ListCommand[2];
                    stTERMINATE.TERMINATERequestId = ReqId;
                    stTERMINATE.TERMINATESessionId = SecID;
                    return ComFunc.StructureToByteArray(stTERMINATE);
                case "CHECKIN":
                    stCHECKIN = (CHECKIN)ListCommand[3];
                    stCHECKIN.CHECKINRequestId = ReqId;
                    stCHECKIN.CHECKINSessionId = SecID;
                    return ComFunc.StructureToByteArray(stCHECKIN);
                case "COMMIT":
                    stCOMMIT = (COMMIT)ListCommand[4];
                    stCOMMIT.COMMITRequestId = ReqId;
                    stCOMMIT.COMMITSessionId = SecID;
                    return ComFunc.StructureToByteArray(stCOMMIT);
                case "ROLLBACK":
                    stROLLBACK = (ROLLBACK)ListCommand[5];
                    stROLLBACK.ROLLBACKRequestId = ReqId;
                    stROLLBACK.ROLLBACKSessionId = SecID;
                    return ComFunc.StructureToByteArray(stROLLBACK);
                case "CHARGE":
                    stCHARGE = (CHARGE)ListCommand[6];
                    stCHARGE.CHARGERequestId = ReqId;
                    stCHARGE.CHARGESessionId = SecID;
                    return ComFunc.StructureToByteArray(stCHARGE);
                default:
                    return null;
            }
        }
    }
}
