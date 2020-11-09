using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;

namespace AppTimeRecorder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel = new MainViewModel
        {
            BindProcessNameText = "",
            BindAppNameText = ""
        };

        private readonly AppInfoList appInfoList = new AppInfoList();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainViewModel;
            Task.Run(() => ActiveAppWatcher());
        }

        private static class NativeMethods

        {
            [DllImport("user32.dll")]
            internal static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();
        }

        public string GetActiveWindowFileName()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            Process p = Process.GetProcessById((int)pid);
            return Path.GetFileName(p.MainModule.FileName);
        }



        private void ActiveAppWatcher()
        {
            Timer timer = new Timer(1000);

            timer.Elapsed += (sender, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var processName = GetActiveWindowFileName();
                    if (processName == null)
                    {
                        mainViewModel.BindProcessNameText = "null";
                    }
                    else
                    {
                        mainViewModel.BindProcessNameText = processName;
                        mainViewModel.BindAppNameText = appInfoList.GetAppNameFromProcessName(processName);
                    }
                    mainViewModel.NotifyPropertyChanged(nameof(MainViewModel.BindProcessNameText));
                });


            };

            timer.Start();
        }


    }
}
