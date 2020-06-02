using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        public delegate IntPtr LoadLibraryADelegate([In, MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);
        public delegate IntPtr GetProcAddressDelegate(IntPtr hModule, [In, MarshalAs(UnmanagedType.LPStr)] string lpProcName);
        public delegate bool FreeLibraryDelegate(IntPtr hLibModule);
        public delegate int GetModuleFileNameWDelegate(IntPtr hModule, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpFileName, int nSize);

        public static readonly LoadLibraryADelegate LoadLibraryA;
        public static readonly GetProcAddressDelegate GetProcAddress;
        public static readonly FreeLibraryDelegate FreeLibrary;
        public static readonly GetModuleFileNameWDelegate GetModuleFileNameW;
    }
}
