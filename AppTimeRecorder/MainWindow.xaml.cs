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
            BindText = "初期値",
            BindAppNameText = "ああああ"
        };

        private Class1 class1 = new Class1();

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
                    var fileName = GetActiveWindowFileName();
                    if (fileName == null)
                    {
                        mainViewModel.BindText = "null";
                    }
                    else
                    {
                        mainViewModel.BindText = fileName;
                        mainViewModel.BindAppNameText = class1.GetAppNameFromProcessName(fileName);
                    }
                    mainViewModel.NotifyPropertyChanged(nameof(MainViewModel.BindText));
                });


            };

            timer.Start();
        }


    }
}
