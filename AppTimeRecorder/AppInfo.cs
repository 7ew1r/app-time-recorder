using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace AppTimeRecorder
{
    public class AppInfo
    {
        public string ProcessName;
        public string AppName;

        public AppInfo(string processName, string appName)
        {
            ProcessName = processName;
            AppName = appName;
        }

        public string GetAppNameFromProcessName(string processName)
        {
            if (processName == "notepad.exe")
            {
                return "メモ帳";
            }
            else
            {
                return "その他";
            }

        }

    }
}
