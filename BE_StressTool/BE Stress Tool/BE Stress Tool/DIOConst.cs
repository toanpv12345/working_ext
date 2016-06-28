using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BE_Stress_Tool
{
    public static class DIOConst
    {
        public const int RET_OK = 0;
        public const int RET_NG = 1;
        
        // State of GUI
        public enum GUIState 
        { 
            GUI_STATE_CONNECTED,
            GUI_STATE_DISCONNECTED,
            GUI_STATE_SYNC_DONE
        };

        // Timer state
        public enum TimerState
        {
            TIMER_STATE_STARTED,
            TIMER_STATE_STOPPED
        };
    }
}
