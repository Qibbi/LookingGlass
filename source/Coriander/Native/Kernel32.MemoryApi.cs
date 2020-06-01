using System;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        public delegate bool VirtualProtectDelegate(IntPtr lpAddress, int dwSize, int flNewProtect, ref int lpflOldProtect);

        public static readonly VirtualProtectDelegate VirtualProtect;
    }
}
