using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        [return: MarshalAs(UnmanagedType.LPStr)] public delegate string GetCommandLineADelegate();
        [return: MarshalAs(UnmanagedType.LPWStr)] public delegate string GetCommandLineWDelegate();

        public static readonly GetCommandLineADelegate GetCommandLineA;
        public static readonly GetCommandLineWDelegate GetCommandLineW;
    }
}
