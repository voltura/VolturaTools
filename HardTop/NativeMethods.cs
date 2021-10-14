#region Using statements

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#endregion Using statements

namespace HardTop
{
    internal static class NativeMethods
    {
        #region Private constants used in user32 API's interaction

        private const int WS_EX_TOPMOST = 0x00000008;
        private const int GWL_EXSTYLE = -20;

        #endregion Private constants used in user32 API's interaction

        #region Private variables

        private static List<IntPtr> WindowHandles;
        private static List<string> WindowTitles;

        #endregion Private variables

        #region Delegates used in user32 API's interaction

        private delegate bool EnumWindowsProc(IntPtr hwnd, ArrayList lParam);
        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        #endregion Delegates used in user32 API's interaction

        #region Internal functions for Window handling

        internal static void GetDesktopWindowHandlesAndTitles(out List<IntPtr> handles, out List<string> titles)
        {
            WindowHandles = new List<IntPtr>();
            WindowTitles = new List<string>();
            if (!EnumDesktopWindows(IntPtr.Zero, GetDesktopWindowHandlesAndTitlesFilterCallback, IntPtr.Zero))
            {
                handles = null;
                titles = null;
            }
            else
            {
                handles = WindowHandles;
                titles = WindowTitles;
            }
        }

        internal static ArrayList AlwaysOnTopWindows()
        {
            var topmostWindowHandles = new ArrayList();
            EnumWindows(EnumWindowsCallback, topmostWindowHandles);
            return topmostWindowHandles;
        }

        internal static void ToogleWindowAlwaysOnTop(IntPtr hwnd, bool alwaysOnTop)
        {
            SetWindowPos(hwnd, (IntPtr)(alwaysOnTop ? -1 : -2), 0, 0, 0, 0, 67);
        }

        #endregion Internal functions for Window handling

        #region Private Callback methods; window handling

        private static bool EnumWindowsCallback(IntPtr hWnd, ArrayList lParam)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            if ((exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST) lParam.Add(hWnd);
            return true;
        }

        private static bool GetDesktopWindowHandlesAndTitlesFilterCallback(IntPtr hWnd, int lParam)
        {
            StringBuilder sb_title = new StringBuilder(1024);
            GetWindowText(hWnd, sb_title, sb_title.Capacity);
            string title = sb_title.ToString();
            if (IsWindowVisible(hWnd) && string.IsNullOrEmpty(title) == false)
            {
                WindowHandles.Add(hWnd);
                WindowTitles.Add(title);
            }
            return true;
        }

        #endregion Private Callback methods; window handling

        #region user32.dll function imports; window handling

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, ArrayList lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int xy, uint flagsw);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        #endregion user32.dll function imports; window handling

        #region user32.dll function import; used to free GDI+ icon from memory

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr handle);

        #endregion user32.dll function import; used to free GDI+ icon from memory

        #region Unused code snippets
        /*
        const int WS_VISIBLE = 0x10000000;
        const int GWL_STYLE = -16;

            var processesToKill = new HashSet<uint>();
            foreach (IntPtr hWnd in topmostWindowHandles)
            {
                uint processId = 0;
                GetWindowThreadProcessId(hWnd, out processId);
                processesToKill.Add(processId);
            }

            foreach (uint pid in processesToKill)
            {
                Process proc = Process.GetProcessById((int)pid);
                Console.WriteLine("Killing " + proc.ProcessName);
                // kill process, except explorer.exe
            }
                static bool EnumWindowsCallback(IntPtr hWnd, ArrayList lParam)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            int style = GetWindowLong(hWnd, GWL_STYLE);
            if ((exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST
                && (style & WS_VISIBLE) == WS_VISIBLE
                )
            {
                lParam.Add(hWnd);
            }
            return true;
        }

         */
        /*[DllImport("user32.dll")]
internal static extern IntPtr GetForegroundWindow();

[DllImport("user32.dll")]
internal static extern IntPtr GetTopWindow(IntPtr hWnd);
*/
        /*      
      [DllImport("Kernel32.dll")] 
      public static extern IntPtr GetConsoleWindow();

      internal delegate bool WindowEnumCallback(int hwnd, int lparam);

      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool EnumWindows(WindowEnumCallback lpEnumFunc, int lParam);
*/

        #endregion Unused code snippets
    }
}