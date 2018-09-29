#region Using statements

using System;
using System.Runtime.InteropServices;

#endregion

namespace WeekNumber
{
    internal static class NativeMethods
    {
        #region External user32.dll function

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr handle);

        #endregion
    }
}