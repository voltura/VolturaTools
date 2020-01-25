using System.Runtime.InteropServices;

namespace ScanDevices
{
    internal static class NativeMethods
    {
        public const int CM_LOCATE_DEVNODE_NORMAL = 0x00000000;
        public const int CM_REENUMERATE_NORMAL = 0x00000000;
        public const int CR_SUCCESS = 0x00000000;

        [DllImport("CfgMgr32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int CM_Locate_DevNodeA(ref int pdnDevInst, string pDeviceID, int ulFlags);

        [DllImport("CfgMgr32.dll", SetLastError = true)]
        public static extern int CM_Reenumerate_DevNode(int dnDevInst, int ulFlags);
    }
}