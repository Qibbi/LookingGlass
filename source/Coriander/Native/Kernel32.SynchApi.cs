using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        public delegate IntPtr CreateEventADelegate(IntPtr lpEventAttributes,
                                                    [MarshalAs(UnmanagedType.Bool)] bool bManualReset,
                                                    [MarshalAs(UnmanagedType.Bool)] bool bInitialState,
                                                    [In, MarshalAs(UnmanagedType.LPStr)] string lpName);
        public delegate IntPtr OpenEventADelegate(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, [In, MarshalAs(UnmanagedType.LPStr)] string lpName);
        public delegate bool SetEventDelegate(IntPtr hEvent);
        public delegate bool ResetEventDelegate(IntPtr hEvent);
        public delegate IntPtr CreateMutexADelegate(IntPtr lpMutexAttributes, [MarshalAs(UnmanagedType.Bool)] bool bInitialOwner, [In, MarshalAs(UnmanagedType.LPStr)] string lpName);
        public delegate IntPtr CreateMutexWDelegate(IntPtr lpMutexAttributes, [MarshalAs(UnmanagedType.Bool)] bool bInitialOwner, [In, MarshalAs(UnmanagedType.LPWStr)] string lpName);
        public delegate int WaitForSingleObjectDelegate(IntPtr hHandle, int milliseconds);

        public static readonly CreateEventADelegate CreateEventA;
        public static readonly OpenEventADelegate OpenEventA;
        public static readonly SetEventDelegate SetEvent;
        public static readonly ResetEventDelegate ResetEvent;
        public static readonly CreateMutexADelegate CreateMutexA;
        public static readonly CreateMutexWDelegate CreateMutexW;
        public static readonly WaitForSingleObjectDelegate WaitForSingleObject;
    }
}
