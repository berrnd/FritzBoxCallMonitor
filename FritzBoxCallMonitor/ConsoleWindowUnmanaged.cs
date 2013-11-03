using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FritzBoxCallMonitor
{
    public class ConsoleWindowUnmanaged
    {
        private ConsoleWindowUnmanaged()
        { }

        const int SW_MINIMIZE = 6;
        private const int SW_SHOW = 5;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void Minimize()
        {
            IntPtr consoleHandle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(consoleHandle, SW_MINIMIZE);
        }

        public static void Show()
        {
            IntPtr consoleHandle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(consoleHandle, SW_SHOW);
        }
    }
}
