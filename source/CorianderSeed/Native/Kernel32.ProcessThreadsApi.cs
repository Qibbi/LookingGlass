﻿using System;
using System.Runtime.InteropServices;

namespace Coriander.Native
{
    internal static partial class Kernel32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct StartupInfoW
        {
            public int Cb;
            [MarshalAs(UnmanagedType.LPWStr)] public string LpReserved;
            [MarshalAs(UnmanagedType.LPWStr)] public string LpDesktop;
            [MarshalAs(UnmanagedType.LPWStr)] public string LpTitle;
            public int DwX;
            public int DwY;
            public int DwXSize;
            public int DwYSize;
            public int DwXCountChars;
            public int DwYCountChars;
            public int DwFillAttribute;
            public int DwFlags;
            public short WShowWindow;
            public short CbReserved2;
            public IntPtr LpReserved2;
            public IntPtr HStdInput;
            public IntPtr HStdOutput;
            public IntPtr HStdError;

            public StartupInfoW(bool _)
            {
                Cb = 68;
                LpReserved = null;
                LpDesktop = null;
                LpTitle = null;
                DwX = 0;
                DwY = 0;
                DwXSize = 0;
                DwYSize = 0;
                DwXCountChars = 0;
                DwYCountChars = 0;
                DwFillAttribute = 0;
                DwFlags = 0;
                WShowWindow = 0;
                CbReserved2 = 0;
                LpReserved2 = IntPtr.Zero;
                HStdInput = IntPtr.Zero;
                HStdOutput = IntPtr.Zero;
                HStdError = IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessInformation
        {
            public IntPtr HProcess;
            public IntPtr HThread;
            public int DwProcessId;
            public int DwThreadId;
        }

        public delegate IntPtr GetCurrentProcessDelegate();
        public delegate bool CreateProcessADelegate([In, MarshalAs(UnmanagedType.LPStr)] string lpApplicationName,
                                                    [MarshalAs(UnmanagedType.LPStr)] string lpCommandLine,
                                                    IntPtr lpProcessAttributes,
                                                    IntPtr lpThreadAttributes,
                                                    [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles,
                                                    int dwCreationFlags,
                                                    IntPtr lpEnvironment,
                                                    [In, MarshalAs(UnmanagedType.LPStr)] string lpCurrentDirectory,
                                                    ref StartupInfoW lpStartupInfo,
                                                    ref ProcessInformation lpProcessInformation);
        public delegate bool CreateProcessWDelegate([In, MarshalAs(UnmanagedType.LPWStr)] string lpApplicationName,
                                                    [MarshalAs(UnmanagedType.LPWStr)] string lpCommandLine,
                                                    IntPtr lpProcessAttributes,
                                                    IntPtr lpThreadAttributes,
                                                    [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles,
                                                    int dwCreationFlags,
                                                    IntPtr lpEnvironment,
                                                    [In, MarshalAs(UnmanagedType.LPWStr)] string lpCurrentDirectory,
                                                    ref StartupInfoW lpStartupInfo,
                                                    ref ProcessInformation lpProcessInformation);
        public delegate bool TerminateProcessDelegate(IntPtr hProcess, int uExitCode);
        public delegate int GetCurrentThreadIdDelegate();
        public delegate IntPtr CreateThreadDelegate(IntPtr lpThreadAttributes, int dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, int dwCreationFlags, ref int lpThreadId);
        public delegate IntPtr OpenThreadDelegate(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwThreadId);
        public delegate int SuspendThreadDelegate(IntPtr hThread);
        public delegate int ResumeThreadDelegate(IntPtr hThread);

        public static readonly GetCurrentProcessDelegate GetCurrentProcess;
        public static readonly CreateProcessADelegate CreateProcessA;
        public static readonly CreateProcessWDelegate CreateProcessW;
        public static readonly TerminateProcessDelegate TerminateProcess;
        public static readonly GetCurrentThreadIdDelegate GetCurrentThreadId;
        public static readonly CreateThreadDelegate CreateThread;
        public static readonly OpenThreadDelegate OpenThread;
        public static readonly SuspendThreadDelegate SuspendThread;
        public static readonly ResumeThreadDelegate ResumeThread;
    }
}
