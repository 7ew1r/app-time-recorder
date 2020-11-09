using System;
using System.Collections.Generic;
using System.Text;

namespace AppTimeRecorder
{
    class Class1
    {
		/*
		public Class1()
		{
			var dict = new Dictionary<string, string>()
			{
				{"aaa.exe", "AAA"},
				{"notepad.exe", "メモ帳"}
			};
		}
		*/

		public string GetAppNameFromProcessName(string processName)
        {
			if(processName == "notepad.exe")
            {
				return "メモ帳";
            } else
            {
				return "その他";
            }

		}

	}
}
