using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AppTimeRecorder
{
    class AppInfoList
    {

        public List<AppInfo> AppList = new List<AppInfo>();

        public AppInfoList()
        {
            string[] csvLines = File.ReadAllLines("AppInfo.csv", Encoding.UTF8);
            // TODO: 例外処理
            foreach (string line in csvLines)
            {

                var splitted = line.Split(',');
                AppInfo appInfo = new AppInfo(splitted[0], splitted[1]);
                AppList.Add(appInfo);
            }
        }

        public string GetAppNameFromProcessName(string processName)
        {
            var app = AppList.Find(x => x.ProcessName == processName);
            return app == null ? processName : app.AppName;
        }

    }
}
