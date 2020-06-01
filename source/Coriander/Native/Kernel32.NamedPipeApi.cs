using System;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        public delegate bool CreatePipeDelegate(out IntPtr hReadPipe, out IntPtr WritePipe, IntPtr lpPipeAttributes, int nSize);

        public static readonly CreatePipeDelegate CreatePipe;
    }
}
